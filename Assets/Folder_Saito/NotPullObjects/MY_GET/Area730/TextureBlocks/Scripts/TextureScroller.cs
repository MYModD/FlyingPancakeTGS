using UnityEngine;

namespace Area730.TextureBlock
{

    public class TextureScroller : MonoBehaviour
    {
        public bool     _stopped         = false;
        public float    _offsetSpeed     = 0.5f;
        public bool     _reverse         = false;

        private float   _timePassed      = 0;
        private Mesh    _mesh;

        void Awake()
        {
            _mesh = GetComponent<MeshFilter>().mesh;
            if (_mesh == null)
            {
                Debug.LogError("TextureScroller: Mesh is null");
            }
        }

        void Update()
        {
            if (_stopped || _mesh == null)
            {
                return;
            }

            float offset = Time.time * _offsetSpeed % 1;
            if (_reverse)
            {
                offset = -offset;
            }
            GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(-offset, 0));
        }

    }

}
