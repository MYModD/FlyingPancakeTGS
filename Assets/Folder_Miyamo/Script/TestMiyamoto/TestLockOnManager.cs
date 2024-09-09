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
    [ReadOnly]
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
    public float _coneAngle = 45f;

    [SerializeField]
    [Header("コーンの長さ、半径")]
    private float _coneRange;

    [SerializeField, Layer]
    [Header("敵のTag")]
    private string _enemyTag;




    [Header("Coneに代入する間の時間")]
    [SerializeField]
    private float _coolTime;



    [Header("Coneに代入可能か"), ReadOnly]
    [SerializeField]
    private bool _canAdd = true;



    [HideInInspector]
    public Vector3 _circleCenterPostion;


    public float _circleRadius;
    [HideInInspector]
    public Quaternion _circleRotation;


    readonly private Vector3 _drawOrigin = new Vector3(90, 0, 0); //固定

    private UnityEngine.Plane[] _cameraPlanes;         // カメラの六面体を保存するもの


    private Dictionary<Transform, Renderer> _transformKeyGetRender = new Dictionary<Transform, Renderer>();


    private void Update() {

        // 最初にロックオンの時間計算をする



        List<Transform> cashCameraTargets = new List<Transform>();

        Collider[] hits = Physics.OverlapSphere(
            _camera.transform.position,
            _searchRadius,
            LayerMask.GetMask(_enemyTag)
        );


        _cameraPlanes = GeometryUtility.CalculateFrustumPlanes(_camera);


        //--------------------------------カメラにいるかのスクリプト--------------------------------------------
        foreach (Collider hit in hits) {
            Transform target = null;
            Renderer renderer = null;

            if (hit.CompareTag("Enemy")) {
                target = hit.transform;

                // TryGetValueでrenderに入力されているので合った場合の処理書く必要なし
                if (_transformKeyGetRender.TryGetValue(target, out renderer) == false) {
                    renderer = target.GetComponent<Renderer>();
                    _transformKeyGetRender.Add(target, renderer);
                }

            } else {
                // Debug.Log($"{hit.gameObject.name} の レンダーコンポーネントがついてない可能性があるよ");
                // layerがEnemyでtagがenemeyでないときここに通るため EliteEnemyのとき追加の処理書く必要あり
                continue;
            }

            if (IsInFrustum(renderer, _cameraPlanes) && hit.gameObject.activeSelf) {

                cashCameraTargets.Add(target);
            }
        }


        cashCameraTargets = cashCameraTargets.Except(_targetsBlackList).ToList();

        _targetsInCamera.Clear();
        _targetsInCamera.AddRange(cashCameraTargets);




        //-------------------------------------Cone内にいるかのスクリプト---------------------------------------
        List<Transform> visibleTargetsInCone = new List<Transform>();

        foreach (Transform target in _targetsInCamera) {

            // Cone内に入っていたらAddする
            if (IsInCone(target)) {
                visibleTargetsInCone.Add(target);

            }
        }

        // 
        if (_canAdd) {
            Transform closestTarget = null;
            float minDistance = float.MaxValue;

            foreach (Transform target in visibleTargetsInCone) {

                // Vector3.Distanceよりこっちのほうが処理軽いらしい
                float distanceToTarget = (_camera.transform.position - target.position).sqrMagnitude;
                if (distanceToTarget < minDistance) {
                    if (_targetsInCone.Contains(target) == false) {
                        closestTarget = target;
                        minDistance = distanceToTarget;
                    }
                }
            }

            if (closestTarget != null) {
                _targetsInCone.Add(closestTarget);
                StartCoroutine(nameof(CanBoolTimer));
            }
        }

        // コーンに入ったターゲットがカメラから見えなくなったとき除く
        IsConeTargetsInCamera();

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


    private void IsConeTargetsInCamera() {
        if (_targetsInCone == null) {

            return;
        }

        for (int i = _targetsInCone.Count - 1; i >= 0; i--) {
            Transform coneTarget = _targetsInCone[i];
            Renderer render = _transformKeyGetRender[coneTarget];
            if (!IsInFrustum(render, _cameraPlanes)) {
                _targetsInCone.RemoveAt(i);
            }
        }

    }

    /// <summary>
    /// Renderが空だとちょっと遅れてロックオンしている気がするのでロックオンが遅かったら 付ける必要があり
    /// </summary>
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
            _circleRadius = _coneRange * Mathf.Tan(coneAngleRad);

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