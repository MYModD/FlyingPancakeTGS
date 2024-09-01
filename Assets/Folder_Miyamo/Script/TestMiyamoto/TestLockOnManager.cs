using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class TestLockOnManager : MonoBehaviour {

    [Header("カメラの視界に入っているターゲットのリスト")]
    public List<Transform> _targetsInCamera = new List<Transform>();

    [Header("錐体内に入っているターゲットのリスト")]
    public List<Transform> _targetsInCone = new List<Transform>();

    [Header("プレイヤーのTransformを指定")]
    [SerializeField, Header("プレイヤーのTransform")]
    private Transform _player;

    [SerializeField, Header("カメラ指定")]
    private Camera _camera;

    [SerializeField, Header("spherecastの半径")]
    private float _searchRadius = 95f;

    [SerializeField, Range(0f, 180f)]
    [Header("コーンの角度")]
    private float _coneAngle = 45f;

    [SerializeField]
    [Header("コーンの長さ、半径")]
    private float _coneRange;


    [HideInInspector]
    public Vector3 _circleCenterPostion;
    [HideInInspector]
    public Quaternion _circleRotation;          //とりあえずあとで書く




    public bool _canAdd = true;
    public float _coolTime;


    public Vector3 _drawOrigin = new Vector3(90, 0, 0);


    private Plane[] _cameraPlanes;
    private float _updateInterval = 0.1f;
    private float _lastUpdate = 0f;


    /// <summary>
    /// Updateよりカメラの処理が終わったあとの方がいいんじゃない
    /// </summary>
    private void LateUpdate() {

        
    }



    void Update() {
       


            UpdateTargets();
            RemoveTargetInCone();
          
    }

    private void UpdateTargets() {
        _cameraPlanes = GeometryUtility.CalculateFrustumPlanes(_camera);
        _targetsInCamera.Clear();
        _targetsInCone.Clear();

        Collider[] hits = GetSphereOverlapHits();

        foreach (Collider hit in hits) {
            ProcessHit(hit, _cameraPlanes);
        }

    }

    private Collider[] GetSphereOverlapHits() {
        return Physics.OverlapSphere(
            _camera.transform.position,
            _searchRadius,
            LayerMask.GetMask("Enemy")
        );
    }

    private void ProcessHit(Collider hit, Plane[] planes) {
        if (hit.CompareTag("Enemy")) {
            Transform target = hit.transform;
            Renderer renderer = target.GetComponent<Renderer>();

            if (renderer != null && IsInFrustum(renderer, planes) && hit.gameObject.activeSelf) {
                _targetsInCamera.Add(target);

                if (IsInCone(target) && hit.gameObject.activeSelf && _canAdd) {

                    if (!_targetsInCone.Contains(target)) {

                        _targetsInCone.Add(target);

                        StartCoroutine(nameof(CanBoolTimer));

                    }
                }
            }
        }
    }

    IEnumerator CanBoolTimer() {

        _canAdd = false;
        yield return new WaitForSeconds(_coolTime);
        _canAdd = true;

    }

    private bool IsInFrustum(Renderer renderer, Plane[] planes) {
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }

    private bool IsInCone(Transform target) {
        Vector3 cameraPosition = _camera.transform.position;
        Vector3 toObject = target.position - cameraPosition;
        float distanceToObject = toObject.magnitude;

        if (distanceToObject <= _coneRange) {
            Vector3 toObjectNormalized = toObject.normalized;
            Vector3 coneDirection = (_player.position - cameraPosition).normalized;
            float angle = Vector3.Angle(coneDirection, toObjectNormalized);
            return angle <= _coneAngle / 2;
        }
        return false;
    }

    private void RemoveTargetInCone() {

        List<Transform> targetsToRemove = new List<Transform>();
        foreach (Transform target in _targetsInCone) {
            if (!GeometryUtility.TestPlanesAABB(_cameraPlanes, target.GetComponent<Collider>().bounds)) {
                targetsToRemove.Add(target);
            }
        }
        foreach (Transform target in targetsToRemove) {
            _targetsInCone.Remove(target);
        }
    }

    void OnDrawGizmos() {
        if (_camera != null) {
            // 球状の範囲を描画
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(_camera.transform.position, _searchRadius);

            // コーン上の円周を描画
            Gizmos.color = Color.yellow;
            float coneAngleRad = Mathf.Deg2Rad * _coneAngle / 2;

            Vector3 coneBaseCenter = _camera.transform.position + ((_player.position - _camera.transform.position).normalized * _coneRange);
            // UI用にキャッシュ
            _circleCenterPostion = coneBaseCenter;



            Vector3 hoge = _drawOrigin + _player.transform.rotation.eulerAngles;
            hoge.z = 0;

            //UI用にキャッシュ
            _circleRotation = Quaternion.Euler(hoge);


            GizmosExtensions.DrawWireCircle(coneBaseCenter, _coneRange * Mathf.Tan(coneAngleRad), 20, Quaternion.Euler(hoge));

            // コーンの範囲を描画
            Gizmos.color = Color.red;
            Vector3 forward = (_player.position - _camera.transform.position).normalized * _coneRange;
            Vector3 rightBoundary = Quaternion.Euler(0, _coneAngle / 2, 0) * forward;
            Vector3 leftBoundary = Quaternion.Euler(0, -_coneAngle / 2, 0) * forward;

            Gizmos.DrawLine(_camera.transform.position, _camera.transform.position + forward);
            Gizmos.DrawLine(_camera.transform.position, _camera.transform.position + rightBoundary);
            Gizmos.DrawLine(_camera.transform.position, _camera.transform.position + leftBoundary);
        }
    }


    private void OnValidate() {
        if (_coneRange > _searchRadius) {
            _coneRange = _searchRadius;
        }
    }
}
