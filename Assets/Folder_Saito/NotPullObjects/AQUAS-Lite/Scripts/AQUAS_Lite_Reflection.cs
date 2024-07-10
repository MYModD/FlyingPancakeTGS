using UnityEngine;
using System.Collections;

namespace AQUAS_Lite {
    [AddComponentMenu("AQUAS Lite/Reflection")]
    [ExecuteInEditMode] // Make mirror live-update even when not in play mode
    public class AQUAS_Lite_Reflection : MonoBehaviour {
        #region Variables
        public bool _mDisablePixelLights = true;
        public int _mTextureSize = 256;
        public float _mClipPlaneOffset = 0.07f;

        public LayerMask _mReflectLayers = -1;

        private Hashtable _mReflectionCameras = new Hashtable(); // Camera -> Camera table

        private RenderTexture _mReflectionTexture = null;
        private int _mOldReflectionTextureSize = 0;

        private static bool _sInsideRendering = false;

        public bool _ignoreOcclusionCulling;

#if UNITY_5_3 || UNITY_5_4 || UNITY_5_5
    public bool disableInEditMode;
#endif

        public void OnWillRenderObject() {

#if UNITY_5_3 || UNITY_5_4 || UNITY_5_5
        if (disableInEditMode && !Application.isPlaying)
        {
            OnDisable();
            return;
        }
#endif

            if (!enabled || !GetComponent<Renderer>() || !GetComponent<Renderer>().sharedMaterial || !GetComponent<Renderer>().enabled)
                return;

            Camera cam = Camera.current;
            if (!cam)
                return;

            // カメラがオブジェクトを正しく見ることができるかチェック
            if (!GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(cam), GetComponent<Renderer>().bounds))
                return;

            // Safeguard from recursive reflections.
            if (_sInsideRendering)
                return;
            _sInsideRendering = true;

            Camera reflectionCamera;
            CreateMirrorObjects(cam, out reflectionCamera);

            // find out the reflection plane: position and normal in world space
            Vector3 pos = transform.position;
            Vector3 normal = transform.up;

            // Optionally disable pixel lights for reflection
            int oldPixelLightCount = QualitySettings.pixelLightCount;
            if (_mDisablePixelLights)
                QualitySettings.pixelLightCount = 0;

            UpdateCameraModes(cam, reflectionCamera);

            // Render reflection
            // Reflect camera around reflection plane
            float d = -Vector3.Dot(normal, pos) - _mClipPlaneOffset;
            Vector4 reflectionPlane = new Vector4(normal.x, normal.y, normal.z, d);

            if (_ignoreOcclusionCulling) {
                reflectionCamera.useOcclusionCulling = false;
            } else {
                reflectionCamera.useOcclusionCulling = true;
            }

            Matrix4x4 reflection = Matrix4x4.zero;
            CalculateReflectionMatrix(ref reflection, reflectionPlane);
            Vector3 oldpos = cam.transform.position;
            Vector3 newpos = reflection.MultiplyPoint(oldpos);
            reflectionCamera.worldToCameraMatrix = cam.worldToCameraMatrix * reflection;

            // Setup oblique projection matrix so that near plane is our reflection
            // plane. This way we clip everything below/above it for free.
            Vector4 clipPlane = CameraSpacePlane(reflectionCamera, pos, normal, 1.0f);
            Matrix4x4 projection = cam.projectionMatrix;
            CalculateObliqueMatrix(ref projection, clipPlane);
            reflectionCamera.projectionMatrix = projection;

            reflectionCamera.cullingMask = ~(1 << 4) & _mReflectLayers.value; // never render water layer
            reflectionCamera.targetTexture = _mReflectionTexture;
            GL.invertCulling = true;
            reflectionCamera.transform.position = newpos;
            Vector3 euler = cam.transform.eulerAngles;
            reflectionCamera.transform.eulerAngles = new Vector3(0, euler.y, euler.z);

            // Check if the reflectionCamera is inside the view frustum
            if (GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(cam), new Bounds(newpos, Vector3.one * 0.1f))) {
                reflectionCamera.Render();
            } else {
                Debug.LogWarning("Reflection camera is out of view frustum.");
            }

            reflectionCamera.transform.position = oldpos;
            GL.invertCulling = false;

            Material[] materials = GetComponent<Renderer>().sharedMaterials;
            foreach (Material mat in materials) {
                if (mat.HasProperty("_ReflectionTex"))
                    mat.SetTexture("_ReflectionTex", _mReflectionTexture);
            }

            // Set matrix on the shader that transforms UVs from object space into screen
            // space. We want to just project reflection texture on screen.
            Matrix4x4 scaleOffset = Matrix4x4.TRS(
                new Vector3(0.5f, 0.5f, 0.5f), Quaternion.identity, new Vector3(0.5f, 0.5f, 0.5f));
            Vector3 scale = transform.lossyScale;
            Matrix4x4 mtx = transform.localToWorldMatrix * Matrix4x4.Scale(new Vector3(1.0f / scale.x, 1.0f / scale.y, 1.0f / scale.z));
            mtx = scaleOffset * cam.projectionMatrix * cam.worldToCameraMatrix * mtx;
            foreach (Material mat in materials) {
                mat.SetMatrix("_ProjMatrix", mtx);
            }

            // Restore pixel light count
            if (_mDisablePixelLights)
                QualitySettings.pixelLightCount = oldPixelLightCount;

            _sInsideRendering = false;
        }
        #endregion

        //<summary>
        // Cleans up all the objects that were possibly created
        //</summary>
        void OnDisable() {
            if (_mReflectionTexture) {
                DestroyImmediate(_mReflectionTexture);
                _mReflectionTexture = null;
            }
            foreach (DictionaryEntry kvp in _mReflectionCameras)
                DestroyImmediate(((Camera)kvp.Value).gameObject);
            _mReflectionCameras.Clear();
        }

        private void UpdateCameraModes(Camera src, Camera dest) {
            if (dest == null)
                return;

            //sets camera to clear the same way as current camera
            dest.clearFlags = src.clearFlags;
            dest.backgroundColor = src.backgroundColor;

            if (src.clearFlags == CameraClearFlags.Skybox) {
                Skybox sky = src.GetComponent(typeof(Skybox)) as Skybox;
                Skybox mysky = dest.GetComponent(typeof(Skybox)) as Skybox;
                if (!sky || !sky.material) {
                    mysky.enabled = false;
                } else {
                    mysky.enabled = true;
                    mysky.material = sky.material;
                }
            }

            // Updates other values to match current camera.
            // Even if camera & projection matrices are supplied, some of values are used elsewhere (e.g. skybox uses far plane)
            dest.farClipPlane = src.farClipPlane;
            dest.nearClipPlane = src.nearClipPlane;
            dest.orthographic = src.orthographic;
            dest.fieldOfView = src.fieldOfView;
            dest.aspect = src.aspect;
            dest.orthographicSize = src.orthographicSize;
        }

        //<summary>
        // Creates any objects needed on demand
        //</summary>
        private void CreateMirrorObjects(Camera currentCamera, out Camera reflectionCamera) {
            reflectionCamera = null;

            // Reflection render texture
            if (!_mReflectionTexture || _mOldReflectionTextureSize != _mTextureSize) {
                if (_mReflectionTexture)
                    DestroyImmediate(_mReflectionTexture);
                _mReflectionTexture = new RenderTexture(_mTextureSize, _mTextureSize, 16);
                _mReflectionTexture.name = "__MirrorReflection" + GetInstanceID();
                _mReflectionTexture.isPowerOfTwo = true;
                _mReflectionTexture.hideFlags = HideFlags.DontSave;
                _mOldReflectionTextureSize = _mTextureSize;
            }

            // Camera for reflection
            reflectionCamera = _mReflectionCameras[currentCamera] as Camera;
            if (!reflectionCamera) // catch both not-in-dictionary and in-dictionary-but-deleted-GO
            {
                GameObject go = new GameObject("Mirror Refl Camera id" + GetInstanceID() + " for " + currentCamera.GetInstanceID(), typeof(Camera), typeof(Skybox));
                reflectionCamera = go.GetComponent<Camera>();
                reflectionCamera.enabled = false;
                reflectionCamera.transform.position = transform.position;
                reflectionCamera.transform.rotation = transform.rotation;
                reflectionCamera.gameObject.AddComponent<FlareLayer>();
                go.hideFlags = HideFlags.HideAndDontSave;
                _mReflectionCameras[currentCamera] = reflectionCamera;
            }
        }

        //<summary>
        // Extended sign: returns -1, 0 or 1 based on sign of a
        //</summary>
        private static float Sgn(float a) {
            if (a > 0.0f)
                return 1.0f;
            if (a < 0.0f)
                return -1.0f;
            return 0.0f;
        }

        //<summary>
        // Given position/normal of the plane, calculates plane in camera space.
        //</summary>
        private Vector4 CameraSpacePlane(Camera cam, Vector3 pos, Vector3 normal, float sideSign) {
            Vector3 offsetPos = pos + normal * _mClipPlaneOffset;
            Matrix4x4 m = cam.worldToCameraMatrix;
            Vector3 cpos = m.MultiplyPoint(offsetPos);
            Vector3 cnormal = m.MultiplyVector(normal).normalized * sideSign;
            return new Vector4(cnormal.x, cnormal.y, cnormal.z, -Vector3.Dot(cpos, cnormal));
        }

        //<summary>
        // Adjusts the given projection matrix so that near plane is the given clipPlane
        // clipPlane is given in camera space
        //</summary>
        private static void CalculateObliqueMatrix(ref Matrix4x4 projection, Vector4 clipPlane) {
            Vector4 q = projection.inverse * new Vector4(
                Sgn(clipPlane.x),
                Sgn(clipPlane.y),
                1.0f,
                1.0f
                );
            Vector4 c = clipPlane * (2.0F / (Vector4.Dot(clipPlane, q)));
            // third row = clip plane - fourth row
            projection[2] = c.x - projection[3];
            projection[6] = c.y - projection[7];
            projection[10] = c.z - projection[11];
            projection[14] = c.w - projection[15];
        }

        //<summary>
        // Calculates reflection matrix around the given plane
        //</summary>
        private static void CalculateReflectionMatrix(ref Matrix4x4 reflectionMat, Vector4 plane) {
            reflectionMat.m00 = (1F - 2F * plane[0] * plane[0]);
            reflectionMat.m01 = (-2F * plane[0] * plane[1]);
            reflectionMat.m02 = (-2F * plane[0] * plane[2]);
            reflectionMat.m03 = (-2F * plane[3] * plane[0]);

            reflectionMat.m10 = (-2F * plane[1] * plane[0]);
            reflectionMat.m11 = (1F - 2F * plane[1] * plane[1]);
            reflectionMat.m12 = (-2F * plane[1] * plane[2]);
            reflectionMat.m13 = (-2F * plane[3] * plane[1]);

            reflectionMat.m20 = (-2F * plane[2] * plane[0]);
            reflectionMat.m21 = (-2F * plane[2] * plane[1]);
            reflectionMat.m22 = (1F - 2F * plane[2] * plane[2]);
            reflectionMat.m23 = (-2F * plane[3] * plane[2]);

            reflectionMat.m30 = 0F;
            reflectionMat.m31 = 0F;
            reflectionMat.m32 = 0F;
            reflectionMat.m33 = 1F;
        }
    }
}
