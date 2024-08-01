using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class TestLockOnManager : MonoBehaviour {

    [Header("�J�����̎��E�ɓ����Ă���^�[�Q�b�g�̃��X�g")]
    public List<Transform> _targetsInCamera = new List<Transform>();

    [Header("���̓��ɓ����Ă���^�[�Q�b�g�̃��X�g")]
    public List<Transform> _targetsInCone = new List<Transform>();

    public MissileStuck[] _missileStucks;

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

    public bool _canAdd = true;
    public float _coolTime;


    public Vector3 _drawOrigin = new Vector3(90, 0, 0);

    private Plane[] _cameraPlanes;
    private float _updateInterval = 0.1f;
    private float _lastUpdate = 0f;

    void Update() {
        if (Time.time - _lastUpdate > _updateInterval) {
            UpdateTargets();
            _lastUpdate = Time.time;
        }

        Debug.Log(_canAdd);
    }

    private void UpdateTargets() {
        _cameraPlanes = GeometryUtility.CalculateFrustumPlanes(_camera);
        _targetsInCamera.Clear();

        Collider[] hits = Physics.OverlapSphere(

            _camera.transform.position,
            _searchRadius,
            LayerMask.GetMask("Enemy")

        );


        Transform minDistanceTarget = null;
        float minDistance = float.MaxValue;

        foreach (Collider hit in hits) {
            if (!hit.CompareTag("Enemy")) {
                return;
            }

            Transform target = hit.transform;
            Renderer renderer = target.GetComponent<Renderer>();

            if (renderer == null) {
                Debug.LogError("meshrenderer�����Ă��Ȃ���");
                return;
            }


            if (IsInFrustum(renderer, _cameraPlanes) && hit.gameObject.activeSelf) {
                _targetsInCamera.Add(target);
            } else {
                return;
            }

            if (IsInCone(target) && hit.gameObject.activeSelf ) {
                float distance = Vector3.Distance(target.position , _camera.transform.position);
                if (distance < minDistance) {

                    minDistanceTarget = target;
                }

            }
        }
        if (minDistanceTarget != null && _canAdd) {

            for (int i = 0; i < _missileStucks.Length; i++) {

                if (minDistanceTarget == _missileStucks[i]._enemyTarget) {

                    break;          
                }
                
                if (_missileStucks[i]._enemyTarget == null) {

                    _missileStucks[i].TargetLockOn(minDistanceTarget);
                    StartCoroutine(nameof(CanBoolTimer));
                    break;
                }

            }
        }

        // �����܂Ńf�o�b�N�p ������������������܂� itanai
        _targetsInCone.Clear();
        for (int i = 0; i < _missileStucks.Length; i++) {
            if (_missileStucks[i]._enemyTarget != null) {

                _targetsInCone.Add(_missileStucks[i]._enemyTarget);           
            }

        }


    }


    IEnumerator CanBoolTimer() {

        _canAdd = false;
        Debug.Log(_canAdd);
        yield return new WaitForSeconds(_coolTime);
        _canAdd = true;
        Debug.Log(_canAdd);
    }
    /// <summary>
    /// �J������render���������Ă��邩 �኱�̌덷����
    /// </summary>
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



    void OnDrawGizmos() {
        if (_camera != null) {
            // ����͈̔͂�`��
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(_camera.transform.position, _searchRadius);

            // �R�[����̉~����`��
            Gizmos.color = Color.yellow;
            float coneAngleRad = Mathf.Deg2Rad * _coneAngle / 2;

            Vector3 coneBaseCenter = _camera.transform.position + ((_player.position - _camera.transform.position).normalized * _coneRange);

            Vector3 hoge = _drawOrigin + _player.transform.rotation.eulerAngles;
            hoge.z = 0;

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
