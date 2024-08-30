using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class TestLockOnManager : MonoBehaviour {

    [Header("�J�����̎��E�ɓ����Ă���^�[�Q�b�g�̃��X�g")]
    public List<Transform> _targetsInCamera = new List<Transform>();

    [Header("���̓��ɓ����Ă���^�[�Q�b�g��debug�p���X�g")]
    public List<Transform> _targetsInCone = new List<Transform>();

    public MissileStuck[] _missileStucks;

    [Header("�v���C���[��Transform���w��")]
    [SerializeField, Header("�v���C���[��Transform")]
    private Transform _player;

    [SerializeField, Header("�J�����w��")]
    private Camera _camera;


    [SerializeField, Header("spherecastの半径")]
    public  float _searchRadius = 95f;

    [SerializeField, Range(0f, 180f)]
    [Header("コーンの角度")]
    public  float _coneAngle = 45f;

    [SerializeField]
    [Header("コーンの長さ、半径")]
    public  float _coneRange;


    public bool _canAdd = true;
    public float _coolTime;

    readonly private Vector3 _drawOrigin = new Vector3(90, 0, 0);

    // Plane[] �^�ɏC��
    private UnityEngine.Plane[] _cameraPlanes;

    void Update() {
        UpdateTargets();

        // ���₷������f�o�b�O�p
        for (int i = 0; i < _missileStucks.Length; i++) {
            if (_missileStucks[i]._enemyTarget != null && _missileStucks[i]._isValueAssignable == false) {
                _targetsInCone.Add(_missileStucks[i]._enemyTarget);
            }
        }
    }

    private void UpdateTargets() {

        // Plane�^�̕ϐ��ɃJ�����̏��������+�J�����̃��X�g���폜����
        _cameraPlanes = GeometryUtility.CalculateFrustumPlanes(_camera);
        _targetsInCamera.Clear();
        _targetsInCone.Clear();

        // �J�����̈ʒu������̔��a�̋���̃R���C�_�[�̔z����擾����
        Collider[] hits = Physics.OverlapSphere(
            _camera.transform.position,
            _searchRadius,
            LayerMask.GetMask("Enemy")
        );

        // ��ԋ߂��G��T�����߂�null��float.MaxValue���g�p
        Transform minDistanceTarget = null;
        float minDistance = float.MaxValue;

        // �R���C�_�[�̔z��Foreach
        foreach (Collider hit in hits) {
            if (!hit.CompareTag("Enemy")) {
                continue;
            }

            //�^�[�Q�b�g��colider��transform,�����_�[���擾
            Transform target = hit.transform;
            Renderer renderer = target.GetComponent<Renderer>();
            if (renderer == null) {
                Debug.LogError("meshrenderer�����Ă��Ȃ���");
                continue;
            }

            // �J�������ɓG������ ���� �G��active��True�̂Ƃ� ����ȊO��return
            if (IsInFrustum(renderer, _cameraPlanes) && hit.gameObject.activeSelf) {
                _targetsInCamera.Add(target);
            } else {
                continue;
            }

            // �R�[�����ɓG������ ���� �G��active��True
            if (IsInCone(target) && target.gameObject.activeSelf && hit.gameObject.activeSelf) {

                // �R�[�����ɕ����̓G������ꍇ��ԋ߂��G��T��
                float distance = Vector3.Distance(target.position, _camera.transform.position);
                if (distance < minDistance) {
                    minDistanceTarget = target;
                }
            }
        }

        // �^�[�Q�b�g��null�ł͂Ȃ� ���� canAdd��true�̂Ƃ�
        if (minDistanceTarget != null && _canAdd) {

            for (int i = 0; i < _missileStucks.Length; i++) {

                // minDistanceTarget��missileStucks�̔z����ɂ���Ƃ�Break
                if (minDistanceTarget == _missileStucks[i]._enemyTarget) {
                    break;
                }

                // 0���珉�߂�_enemyTarget��null�̂Ƃ�������邽�߂�
                // ���\�b�h���Ăяo���N�[���^�C���̃R���[�`�����Ă�
                if (_missileStucks[i]._enemyTarget == null) {
                    _missileStucks[i].TargetLockOn(minDistanceTarget);
                    StartCoroutine(nameof(CanBoolTimer));
                    break;
                }
            }
        }
    }

    /// <summary>
    /// false�ɂ���莞�Ԍ��true�ɂ���
    /// </summary>
    IEnumerator CanBoolTimer() {
        _canAdd = false;
        Debug.Log(_canAdd);
        yield return new WaitForSeconds(_coolTime);
        _canAdd = true;
        Debug.Log(_canAdd);
    }

    /// <summary>
    /// �J������render���������Ă��邩 render�̃T�C�Y�Ōv�����Ă���̂Ŏ኱�̌덷����
    /// </summary>
    private bool IsInFrustum(Renderer renderer, UnityEngine.Plane[] planes) {
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }

    /// <summary>
    /// target���R�[�����ɂ��邩 �x�N�g���𐳋K�����Ċp�x�������Ă��邩����
    /// </summary>  
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

#if UNITY_EDITOR    
    void OnDrawGizmos() {
        if (_camera == null || _player == null) {
            Debug.Log("�J�������v���C���[���ĂȂ���");
            return;
        }
        // ����͈̔͂�`��
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(_camera.transform.position, _searchRadius);

        // �R�[���̕����Ɖ�]���v�Z
        Vector3 coneDirection = (_player.position - _camera.transform.position).normalized;
        Quaternion coneRotation = Quaternion.LookRotation(coneDirection);

        // �R�[����̉~����`��
        Gizmos.color = Color.yellow;
        float coneAngleRad = Mathf.Deg2Rad * _coneAngle / 2;
        Vector3 coneBaseCenter = _camera.transform.position + (coneDirection * _coneRange);

        Vector3 hoge = coneRotation.eulerAngles + _drawOrigin;
        hoge.z = 0;

        GizmosExtensions.DrawWireCircle(coneBaseCenter, _coneRange * Mathf.Tan(coneAngleRad), 20, Quaternion.Euler(hoge));

        // �R�[���͈̔͂�`��
        Gizmos.color = Color.red;
        Vector3 forward = coneDirection * _coneRange;
        Vector3 rightBoundary = coneRotation * Quaternion.Euler(0, _coneAngle / 2, 0) * Vector3.forward * _coneRange;
        Vector3 leftBoundary = coneRotation * Quaternion.Euler(0, -_coneAngle / 2, 0) * Vector3.forward * _coneRange;

        Gizmos.DrawLine(_camera.transform.position, _camera.transform.position + forward);
        Gizmos.DrawLine(_camera.transform.position, _camera.transform.position + rightBoundary);
        Gizmos.DrawLine(_camera.transform.position, _camera.transform.position + leftBoundary);
    }

    private void OnValidate() {
        if (_coneRange > _searchRadius) {
            _coneRange = _searchRadius;
        }
    }
#endif
}
