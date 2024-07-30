using System.Collections.Generic;
using UnityEngine;
using Utils;

public class TestLockOnManager : MonoBehaviour {
    #region �ϐ�

    [Header("�^�[�Q�b�g�̃��X�g")]
    public List<Transform> _targetsInCamera = new List<Transform>();
    public MissileStuck[] _missileStucks;

    [SerializeField, Header("�J�����ݒ�")] private Camera _camera;

    [SerializeField,Header("���b�N�I���͈̔�")] private float _searchRadius = 5000f;
    
    [SerializeField, Header("�R�[���̊p�x")]
    [Range(0f, 180f)]private float _coneAngle = 45f;

    [SerializeField,Header("�R�[���̒���")] private float _coneRange;

    [SerializeField, Header("�v���C���[��transfrom")]
    private  Transform _playerObject;



    private const float UPDATE_INTERVAL = 0.1f;     // �������R�ɕς��Ă���������const����Ȃ��Ă�����

#pragma warning disable IDE1006 // uruasai
    private readonly Vector3 DRAWORIGIN = new(90, 0, 0);
#pragma warning restore IDE1006 // �����X�^�C��



    private Plane[] _cameraPlanes;        // �J�����̘Z�ʑ̂��L���b�V������ϐ�

    private float _lastUpdate = 0f;       // ���Ԋu�ɂ��邽�߂̕ϐ�
    private Collider[] _hitsBuffer = new Collider[100];

    // HashSet���g�p���č����Ȍ����Əd���`�F�b�N������
    private HashSet<Transform> _targetsInCameraSet = new HashSet<Transform>();
    private HashSet<Transform> _targetsInConeSet = new HashSet<Transform>();

    // Renderer �R���|�[�l���g���L���b�V�����邽�߂� Dictionary
    private Dictionary<Transform, Renderer> _rendererCache = new Dictionary<Transform, Renderer>();

    #endregion

    #region ���\�b�h

    private void Start() {
        foreach (var item in _missileStucks) {
            Debug.Log(item.transform);
        }
    }

    private void Update() {
        // ���Ԋu�Ń^�[�Q�b�g���X�V
        if (Time.time - _lastUpdate >= UPDATE_INTERVAL) {
            UpdateTargets();
            _lastUpdate = Time.time;
        }

    }

    #endregion

    #region Target Management

    /// <summary>
    /// �^�[�Q�b�g���X�g���X�V����
    /// </summary>
    private void UpdateTargets() {
        // Plane�ɃJ�����̘Z�ʑ̂̌`���L���b�V������
        _cameraPlanes = GeometryUtility.CalculateFrustumPlanes(_camera);

        HashSet<Transform> newTargetsInCamera = new HashSet<Transform>();
        HashSet<Transform> newTargetsInCone = new HashSet<Transform>();

        // ���̓��̃R���C�_�[�����o
        int hitCount = Physics.OverlapSphereNonAlloc(
            _camera.transform.position,
            _searchRadius,
            _hitsBuffer,
            LayerMask.GetMask("Enemy")
        );

        // ���o���ꂽ�R���C�_�[������
        for (int i = 0; i < hitCount; i++) {
            ProcessHit(_hitsBuffer[i], _cameraPlanes, newTargetsInCamera, newTargetsInCone);
        }

        // �^�[�Q�b�g���X�g���X�V
        UpdateTargetList(_targetsInCamera, _targetsInCameraSet, newTargetsInCamera);
        //UpdateTargetList(_targetsInCone, _targetsInConeSet, newTargetsInCone);
    }

    /// <summary>
    /// ���o���ꂽ�R���C�_�[���������A�K�؂ȃ��X�g�ɒǉ�����
    /// </summary>
    private void ProcessHit(Collider hit, Plane[] planes, HashSet<Transform> newTargetsInCamera, HashSet<Transform> newTargetsInCone) {
        if (hit.CompareTag("Enemy") || hit.CompareTag("EliteMissile")) { //���Ƃŏ�����������
            Transform target = hit.transform;
            Renderer renderer = GetCachedRenderer(target);

            // �J�����̎�������ɂ���A�A�N�e�B�u�ȓG�݂̂�����
            if (renderer != null && GeometryUtility.TestPlanesAABB(planes, renderer.bounds) && hit.gameObject.activeSelf) {
                newTargetsInCamera.Add(target);

                // �R�[�����ɂ���ꍇ�́A�R�[���̃��X�g�ɂ��ǉ�
                if (IsInCone(target)) {
                    newTargetsInCone.Add(target);
                }
            }
        }
    }

    /// <summary>
    /// �^�[�Q�b�g���X�g�������I�ɍX�V����
    /// </summary>
    private void UpdateTargetList(List<Transform> targetList, HashSet<Transform> targetSet, HashSet<Transform> newTargets) {
        // �Â��^�[�Q�b�g���폜
        targetList.RemoveAll(t => {
            if (!newTargets.Contains(t)) {
                targetSet.Remove(t);
                _rendererCache.Remove(t);  // �L���b�V��������폜
                return true;
            }
            return false;
        });

        // �V�����^�[�Q�b�g��ǉ�
        foreach (Transform newTarget in newTargets) {
            if (!targetSet.Contains(newTarget)) {
                targetList.Add(newTarget);
                targetSet.Add(newTarget);
            }
        }
    }

    /// <summary>
    /// Renderer �R���|�[�l���g���L���b�V������擾�A�܂��͐V���Ɏ擾���ăL���b�V������
    /// </summary>
    private Renderer GetCachedRenderer(Transform target) {
        if (!_rendererCache.TryGetValue(target, out Renderer renderer)) {
            renderer = target.GetComponent<Renderer>();
            if (renderer != null) {
                _rendererCache[target] = renderer;
            }
        }
        return renderer;
    }

    #endregion

    #region Helper Methods

    /// <summary>
    /// �^�[�Q�b�g���R�[�����ɂ��邩�ǂ����𔻒肷��
    /// </summary>
    private bool IsInCone(Transform target) {
        Vector3 cameraPosition = _camera.transform.position;
        Vector3 toObject = target.position - cameraPosition;
        float distanceToObject = toObject.magnitude;

        if (distanceToObject <= _coneRange) {
            Vector3 toObjectNormalized = toObject.normalized;
            Vector3 coneDirection = (_playerObject.position - cameraPosition).normalized;
            float angle = Vector3.Angle(coneDirection, toObjectNormalized);
            return angle <= _coneAngle / 2;
        }
        return false;
    }

#if UNITY_EDITOR

    /// <summary>
    /// �f�o�b�O�p�̃M�Y����`�悷��
    /// </summary>
    private void OnDrawGizmos() {
        if (_camera != null) {
            // ����͈̔͂�`��
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(_camera.transform.position, _searchRadius);

            // �R�[����̉~����`��
            Gizmos.color = Color.yellow;
            float coneAngleRad = Mathf.Deg2Rad * _coneAngle / 2;

            Vector3 coneBaseCenter = _camera.transform.position + ((_playerObject.position - _camera.transform.position).normalized * _coneRange);

            Vector3 hoge = DRAWORIGIN + transform.rotation.eulerAngles;
            hoge.z = 0;

            GizmosExtensions.DrawWireCircle(coneBaseCenter, _coneRange * Mathf.Tan(coneAngleRad), 20, Quaternion.Euler(hoge));

            // �R�[���͈̔͂�`��
            Gizmos.color = Color.red;
            Vector3 forward = (_playerObject.position - _camera.transform.position).normalized * _coneRange;
            Vector3 rightBoundary = Quaternion.Euler(0, _coneAngle / 2, 0) * forward;
            Vector3 leftBoundary = Quaternion.Euler(0, -_coneAngle / 2, 0) * forward;

            Gizmos.DrawLine(_camera.transform.position, _camera.transform.position + forward);
            Gizmos.DrawLine(_camera.transform.position, _camera.transform.position + rightBoundary);
            Gizmos.DrawLine(_camera.transform.position, _camera.transform.position + leftBoundary);
        }
    }

    /// <summary>
    /// coneRange��searchRadius�ȉ��ɂ��鐧��X�N���v�g
    /// </summary>
    private void OnValidate() {
        if (_coneRange > _searchRadius) {
            _coneRange = _searchRadius;
        }
    }
#endif
    #endregion
}
