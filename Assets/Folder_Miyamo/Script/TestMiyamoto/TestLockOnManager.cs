using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

public class TestLockOnManager : MonoBehaviour {

    [Header("�J�����̎��E�ɓ����Ă���^�[�Q�b�g�̃��X�g")]
    public List<Transform> _targetsInCamera = new List<Transform>();

    [Header("���̓��ɓ����Ă���^�[�Q�b�g�̃��X�g")]
    public List<Transform> _targetsInCone = new List<Transform>();

    [Header("���˂������Ƃ̃^�[�Q�b�g�̃u���b�N���X�g")]
    public List<Transform> _targetsBlackList = new List<Transform>();

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

    [SerializeField, Layer]
    [Header("�G��Tag")]
    private string _enemyTag;

    [SerializeField, Tag]
    [Header("�r���̃^�O")]
    private string _buildingTag;


    [Header("Cone�ɑ������Ԃ̎���")]
    [SerializeField]
    private float _coolTime;

    [Header("Cone�����b�N�I���ł���܂ł̎���")]
    [SerializeField]
    private float _lockOnDuration;

    [Header("Cone�ɑ���\��"), ReadOnly]
    [SerializeField]
    private bool _canAdd = true;



    [HideInInspector]
    public Vector3 _circleCenterPostion;
    [HideInInspector]
    public Quaternion _circleRotation;


    readonly private Vector3 _drawOrigin = new Vector3(90, 0, 0); //�Œ�

    private UnityEngine.Plane[] _cameraPlanes;         // �J�����̘Z�ʑ̂�ۑ��������
    

    private Dictionary<Transform, Renderer> _transformKeyGetRender = new Dictionary<Transform, Renderer>();
    private Dictionary<Transform, float> _targetLockOnConeDuration = new Dictionary<Transform, float>();


    private void Update() {

        InConeTimerDegree();


        List<Transform> cashCameraTargets = new List<Transform>();

        Collider[] hits = Physics.OverlapSphere(
            _camera.transform.position,
            _searchRadius,
            LayerMask.GetMask(_enemyTag)
        );

        _cameraPlanes = GeometryUtility.CalculateFrustumPlanes(_camera);

        foreach (Collider hit in hits) {
            Transform target = default;
            Renderer renderer = null;

            if (hit.CompareTag("Enemy")) {
                target = hit.transform;

                if (!_transformKeyGetRender.TryGetValue(target, out renderer)) {
                    renderer = target.GetComponent<Renderer>();
                    _transformKeyGetRender.Add(target, renderer);
                }

            } else {
                Debug.Log($"{hit.gameObject.name} �� �����_�[�R���|�[�l���g�����ĂȂ��\���������");
                continue;
            }

            if (IsInFrustum(renderer, _cameraPlanes) && hit.gameObject.activeSelf) {
                Vector3 directionToTarget = (target.position - _camera.transform.position).normalized;

                RaycastHit[] hitsALL = Physics.RaycastAll(_camera.transform.position, directionToTarget, _searchRadius);
                RaycastHit minDistanceObject = default;

                for (int i = 0; i < hitsALL.Length; i++) {
                    if (hitsALL[i].collider.CompareTag(_enemyTag) || hitsALL[i].collider.CompareTag(_buildingTag)) {
                        minDistanceObject = hitsALL[i];
                        break;
                    }
                }

                foreach (RaycastHit hitOne in hitsALL) {
                    if (hitOne.collider.CompareTag(_enemyTag) || hitOne.collider.CompareTag(_buildingTag)) {
                        if (hitOne.distance < minDistanceObject.distance) {
                            minDistanceObject = hitOne;
                        }
                    }
                }

                Debug.Log($"Ray�����������G�̖��O��  {minDistanceObject.collider.gameObject.name}  �� Tag��{minDistanceObject.collider.tag}");

                if (minDistanceObject.collider.CompareTag(_enemyTag)) {
                    cashCameraTargets.Add(minDistanceObject.collider.gameObject.transform);
                }

                // Ray������
                Debug.DrawRay(_camera.transform.position, directionToTarget * _searchRadius, Color.green);
            }
        }

        cashCameraTargets = cashCameraTargets.Except(_targetsBlackList).ToList();

        _targetsInCamera.Clear();
        _targetsInCamera.AddRange(cashCameraTargets);



        // �R�[�����ɂ���G���r���̌��������ɂ���Ƃ�Remove����
        if (_targetsInCone != null) {
            foreach (Transform item in _targetsInCone) {
                Vector3 directionToTarget = (item.position - _camera.transform.position).normalized;

                // waypoint�^�O�ȊO�̓G�܂��̓r���Ƀq�b�g�����ŏ���RaycastHit���擾
                RaycastHit hit = Physics.RaycastAll(_camera.transform.position, directionToTarget, _searchRadius)
                    .FirstOrDefault(hit => hit.collider.CompareTag(_enemyTag) || (hit.collider.CompareTag(_buildingTag) && !hit.collider.CompareTag("waypoint")));

                // �q�b�g�����I�u�W�F�N�g���G�^�O�ŁA����������ɂ���ꍇ
                if (hit.collider != null && hit.collider.CompareTag(_enemyTag) && IsInFrustum(hit.collider.GetComponent<Renderer>(), _cameraPlanes)) {
                    // �������p�� (��: ���b�N�I���^�[�Q�b�g�Ƃ��ď���)
                } else {
                    // �G��������Ȃ����A��������ɂȂ��ꍇ�A���X�g����폜
                    _targetsInCone.Remove(item);
                }
            }
        }

        List<Transform> visibleTargetsInCone = new List<Transform>(cashCameraTargets);

        if (_canAdd) {
            Transform closestTarget = null;
            float minDistance = float.MaxValue;

            foreach (Transform target in visibleTargetsInCone) {
                float distanceToTarget = Vector3.Distance(_camera.transform.position, target.position);
                if (distanceToTarget < minDistance) {
                    if (!_targetsInCone.Contains(target)) {
                        closestTarget = target;
                        minDistance = distanceToTarget;
                    }
                }
            }

            if (closestTarget != null) {
                _targetsInCone.Add(closestTarget);
                _targetLockOnConeDuration.Add(closestTarget, _lockOnDuration);
                StartCoroutine(nameof(CanBoolTimer));
            }
        }
    }

    IEnumerator CanBoolTimer() {
        _canAdd = false;
        yield return new WaitForSeconds(_coolTime);
        _canAdd = true;
    }

    public void ClearConeTargetAndAddBlackList() {
        _targetsBlackList.AddRange(_targetsInCone);
        _targetsInCone.Clear();
    }

    public void RemoveBlackList(Transform transform) {
        _targetsBlackList.Remove(transform);
    }

    public void InConeTimerDegree() {

        float deltaTime = Time.deltaTime;

        // _targetLockOnConeDuration �̒l���X�V
        foreach (KeyValuePair<Transform, float> entry in _targetLockOnConeDuration) {
            Transform target = entry.Key;
            float duration = entry.Value;

            // �o�ߎ��Ԃ����Z
            duration -= deltaTime;

            // ���Ԃ�0�ȉ��ɂȂ����ꍇ�́A��������폜
            if (duration <= 0f) {
                _targetLockOnConeDuration.Remove(target);
                _targetsInCone.Remove(target);
            } else {
                // �l���X�V
                _targetLockOnConeDuration[target] = duration;
            }
        }

    }
    private bool IsInFrustum(Renderer renderer, UnityEngine.Plane[] planes) {
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

    private void OnEnable() {
        _canAdd = true;
    }

    void OnDrawGizmos() {
        if (_camera != null) {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(_camera.transform.position, _searchRadius);

            Gizmos.color = Color.yellow;
            float coneAngleRad = Mathf.Deg2Rad * _coneAngle / 2;

            Vector3 coneBaseCenter = _camera.transform.position + ((_player.position - _camera.transform.position).normalized * _coneRange);
            _circleCenterPostion = coneBaseCenter;

            Vector3 hoge = _drawOrigin + _player.transform.rotation.eulerAngles;
            hoge.z = 0;

            _circleRotation = Quaternion.Euler(hoge);

            GizmosExtensions.DrawWireCircle(coneBaseCenter, _coneRange * Mathf.Tan(coneAngleRad), 20, Quaternion.Euler(hoge));

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
            Debug.LogError("_coneRange��_searchRadius�𒴂��Ă����I���������Ă����� ^^;");
        }
    }
}
