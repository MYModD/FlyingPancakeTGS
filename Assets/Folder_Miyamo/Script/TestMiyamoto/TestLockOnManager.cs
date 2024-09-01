using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class TestLockOnManager : MonoBehaviour {

    [Header("�J�����̎��E�ɓ����Ă���^�[�Q�b�g�̃��X�g")]
    public List<Transform> _targetsInCamera = new List<Transform>();

    [Header("���̓��ɓ����Ă���^�[�Q�b�g�̃��X�g")]
    public List<Transform> _targetsInCone = new List<Transform>();

    [Header("�v���C���[��Transform���w��")]
    [SerializeField, Header("�v���C���[��Transform")]
    private Transform _player;

    [SerializeField, Header("�J�����w��")]
    private Camera _camera;

    [SerializeField, Header("spherecast�̔��a")]
    private float _searchRadius = 95f;

    [SerializeField, Range(0f, 180f)]
    [Header("�R�[���̊p�x")]
    private float _coneAngle = 45f;

    [SerializeField]
    [Header("�R�[���̒����A���a")]
    private float _coneRange;


    [HideInInspector]
    public Vector3 _circleCenterPostion;
    [HideInInspector]
    public Quaternion _circleRotation;          //�Ƃ肠�������Ƃŏ���




    public bool _canAdd = true;
    public float _coolTime;


    public Vector3 _drawOrigin = new Vector3(90, 0, 0);


    private Plane[] _cameraPlanes;
    private float _updateInterval = 0.1f;
    private float _lastUpdate = 0f;


    /// <summary>
    /// Update���J�����̏������I��������Ƃ̕��������񂶂�Ȃ�
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
            // ����͈̔͂�`��
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(_camera.transform.position, _searchRadius);

            // �R�[����̉~����`��
            Gizmos.color = Color.yellow;
            float coneAngleRad = Mathf.Deg2Rad * _coneAngle / 2;

            Vector3 coneBaseCenter = _camera.transform.position + ((_player.position - _camera.transform.position).normalized * _coneRange);
            // UI�p�ɃL���b�V��
            _circleCenterPostion = coneBaseCenter;



            Vector3 hoge = _drawOrigin + _player.transform.rotation.eulerAngles;
            hoge.z = 0;

            //UI�p�ɃL���b�V��
            _circleRotation = Quaternion.Euler(hoge);


            GizmosExtensions.DrawWireCircle(coneBaseCenter, _coneRange * Mathf.Tan(coneAngleRad), 20, Quaternion.Euler(hoge));

            // �R�[���͈̔͂�`��
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
