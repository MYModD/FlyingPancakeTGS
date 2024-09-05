using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

public class TestLockOnManager : MonoBehaviour {

    [Header("カメラの視界に入っているターゲットのリスト")]
    public List<Transform> _targetsInCamera = new List<Transform>();

    [Header("錐体内に入っているターゲットのリスト")]
    public List<Transform> _targetsInCone = new List<Transform>();

    [Header("発射したあとのターゲットのブラックリスト")]
    public List<Transform> _targetsBlackList = new List<Transform>();

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

    [SerializeField, Layer]
    [Header("敵のTag")]
    private string _enemyTag;

    [SerializeField, Tag]
    [Header("ビルのタグ")]
    private string _buildingTag;


    [Header("Coneに代入する間の時間")]
    [SerializeField]
    private float _coolTime;

    [Header("Coneがロックオンできるまでの時間")]
    [SerializeField]
    private float _lockOnDuration;

    [Header("Coneに代入可能か"), ReadOnly]
    [SerializeField]
    private bool _canAdd = true;



    [HideInInspector]
    public Vector3 _circleCenterPostion;
    [HideInInspector]
    public Quaternion _circleRotation;


    readonly private Vector3 _drawOrigin = new Vector3(90, 0, 0); //固定

    private UnityEngine.Plane[] _cameraPlanes;         // カメラの六面体を保存するもの


    private Dictionary<Transform, Renderer> _transformKeyGetRender = new Dictionary<Transform, Renderer>();
    private Dictionary<Transform, float> _targetLockOnConeDuration = new Dictionary<Transform, float>();


    private void Update() {
        // System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
        // stopwatch.Start();
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
                //Debug.Log($"{hit.gameObject.name} の レンダーコンポーネントがついてない可能性があるよ");
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

                if (minDistanceObject.collider != default){

                    if (minDistanceObject.collider.CompareTag(_enemyTag)) {
                        cashCameraTargets.Add(minDistanceObject.collider.gameObject.transform);
                    }
                }
                

                // Rayを可視化
                Debug.DrawRay(_camera.transform.position, directionToTarget * _searchRadius, Color.green);
            }
        }

        cashCameraTargets = cashCameraTargets.Except(_targetsBlackList).ToList();

        _targetsInCamera.Clear();
        _targetsInCamera.AddRange(cashCameraTargets);


        //Hige();


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

        // stopwatch.Stop();
        // Debug.Log($"かかった時間  {stopwatch.Elapsed.TotalSeconds}");

    }

    public void Hige() {

        // コーン内にいる敵がビルの向こう側にあるときRemoveする
        if (_targetsInCone != null) {

            return;
        }
        foreach (Transform item in _targetsInCone) {
            

            // ヒットしたオブジェクトが敵タグで、かつ視錐台内にある場合
            if (IsInFrustum(_transformKeyGetRender[item], _cameraPlanes)) {
                // 処理を継続 (例: ロックオンターゲットとして処理)
            } else {
                // 敵が見つからないか、視錐台内にない場合、リストから削除
                _targetsInCone.Remove(item);
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

        if (_targetLockOnConeDuration == null || _targetLockOnConeDuration.Count == 0) {
            return;
        }

        // 削除対象のキーを一時リストに格納
        List<Transform> keysToRemove = new List<Transform>();

        // _targetLockOnConeDuration の値を更新
        foreach (var entry in _targetLockOnConeDuration.ToList()) {
            Transform target = entry.Key;
            float duration = entry.Value;

            // 経過時間を減算
            duration -= deltaTime;

            // 時間が0以下になった場合は、削除対象としてマーク
            if (duration <= 0f) {
                keysToRemove.Add(target);
            } else {
                // 値を更新
                _targetLockOnConeDuration[target] = duration;
            }
        }

        // 削除対象を辞書から削除
        foreach (Transform key in keysToRemove) {
           
            _targetLockOnConeDuration.Remove(key);
            _targetsInCone.Remove(key);
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
            Debug.LogError("_coneRangeが_searchRadiusを超えているよ！速く直してあげて ^^;");
        }
    }
}
