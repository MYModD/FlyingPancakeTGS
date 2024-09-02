using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Utils;
using System.Linq;

public class TestLockOnManager : MonoBehaviour {


    [Header("�J�����̎��E�ɓ����Ă���^�[�Q�b�g�̃��X�g")]
    public List<Transform> _targetsInCamera = new List<Transform>();

    [Header("���̓��ɓ����Ă���^�[�Q�b�g�̃��X�g")]
    public List<Transform> _targetsInCone = new List<Transform>();

    [Header("���˂������Ƃ̃^�[�Q�b�g�̃��X�g")]
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


    //private Dictionary<GameObject,float>
    //private Dictionary<GameObject, Renderer>   ���Ƃ�getcompnent�̍ė��p��float�ɂ��^�C�}�[����������



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



    private void Update() {

        // �L���b�V���p�A��r�p��List
        List<Transform> cashCameraTargets = new List<Transform>();


        // �J����������̔��a�ɂ��āA���C���[��Enemy�̃R���C�_�[���̔z���ۑ�
        // �d��������y����ɂ���
        Collider[] hits = Physics.OverlapSphere(
            _camera.transform.position,
            _searchRadius,
            LayerMask.GetMask(_enemyTag)
        );

        // �J�����̘Z�ʑ�
        _cameraPlanes = GeometryUtility.CalculateFrustumPlanes(_camera);


        foreach (Collider hit in hits) {


            Transform target = default;
            Renderer renderer = default;

            if (hit.CompareTag("Enemy")) {
                target = hit.transform;
                renderer = target.GetComponent<Renderer>();
            } else {
                Debug.Log("�����_�[�R���|�[�l���g�����ĂȂ��\���������");
                continue;
            }

            if (IsInFrustum(renderer, _cameraPlanes) && hit.gameObject.activeSelf) {

                // �J��������^�[�Q�b�g�ւ̕����x�N�g�����v�Z
                Vector3 directionToTarget = (target.position - _camera.transform.position).normalized;

                // Raycast�����s,Gizmo�ŕ`��
                RaycastHit[] hitsALL;
                hitsALL = Physics.RaycastAll(_camera.transform.position, directionToTarget, _searchRadius);

                RaycastHit minDistanceObject = default;


                // �G�ƃr���̃^�O������������brake ���
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


                if (minDistanceObject.collider.CompareTag(_enemyTag)) {
                    print(minDistanceObject.collider.CompareTag(_enemyTag));
                    cashCameraTargets.Add(minDistanceObject.collider.gameObject.transform);
                }

            }
        }

        // �u���b�N���X�g�Ɋ܂܂��G�����O
        cashCameraTargets = cashCameraTargets.Except(_targetsBlackList).ToList();

        _targetsInCamera.Clear(); // �����̃��X�g���N���A
        _targetsInCamera.AddRange(cashCameraTargets); // �V�����^�[�Q�b�g��ǉ�


        foreach (Transform item in _targetsInCone) {

            Vector3 directionToTarget = (item.position - _camera.transform.position).normalized;

            // Raycast�����s,Gizmo�ŕ`��
            RaycastHit[] hitsALL;
            hitsALL = Physics.RaycastAll(_camera.transform.position, directionToTarget, _coneRange);

            RaycastHit minDistanceObject = default;


            // �G�ƃr���̃^�O������������brake ���
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

            Renderer render = minDistanceObject.collider.GetComponent<Renderer>();
            if (!minDistanceObject.collider.CompareTag(_enemyTag) || !IsInFrustum(render, _cameraPlanes) ){
                _targetsInCone.Remove(minDistanceObject.transform);
            }


        }








        // �L���b�V���p�A�G�ɃR�s�[ remove���悤�߂�ǂ���������
        List<Transform> visibleTargetsInCone = new List<Transform>(cashCameraTargets);

        if (_canAdd) {
            // ��ԋ������������I�u�W�F�N�g��������
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

            }
            StartCoroutine(nameof(CanBoolTimer));
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


    private void OnEnable() {
        _canAdd = true;
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
            Debug.LogError("_coneRange��_searchRadius�𒴂��Ă����I���������Ă����� ^^;");
        }
    }









}










