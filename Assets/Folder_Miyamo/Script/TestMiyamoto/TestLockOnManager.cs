using System.Collections.Generic;
using UnityEngine;
using Utils;

public class TestLockOnManager : MonoBehaviour {
    #region 変数

    [Header("ターゲットのリスト")]
    public List<Transform> _targetsInCamera = new List<Transform>();
    public MissileStuck[] _missileStucks;

    [SerializeField, Header("カメラ設定")] private Camera _camera;

    [SerializeField,Header("ロックオンの範囲")] private float _searchRadius = 5000f;
    
    [SerializeField, Header("コーンの角度")]
    [Range(0f, 180f)]private float _coneAngle = 45f;

    [SerializeField,Header("コーンの長さ")] private float _coneRange;

    [SerializeField, Header("プレイヤーのtransfrom")]
    private  Transform _playerObject;



    private const float UPDATE_INTERVAL = 0.1f;     // ここ自由に変えてもいいからconstじゃなくてもいい

#pragma warning disable IDE1006 // uruasai
    private readonly Vector3 DRAWORIGIN = new(90, 0, 0);
#pragma warning restore IDE1006 // 命名スタイル



    private Plane[] _cameraPlanes;        // カメラの六面体をキャッシュする変数

    private float _lastUpdate = 0f;       // 一定間隔にするための変数
    private Collider[] _hitsBuffer = new Collider[100];

    // HashSetを使用して高速な検索と重複チェックを実現
    private HashSet<Transform> _targetsInCameraSet = new HashSet<Transform>();
    private HashSet<Transform> _targetsInConeSet = new HashSet<Transform>();

    // Renderer コンポーネントをキャッシュするための Dictionary
    private Dictionary<Transform, Renderer> _rendererCache = new Dictionary<Transform, Renderer>();

    #endregion

    #region メソッド

    private void Start() {
        foreach (var item in _missileStucks) {
            Debug.Log(item.transform);
        }
    }

    private void Update() {
        // 一定間隔でターゲットを更新
        if (Time.time - _lastUpdate >= UPDATE_INTERVAL) {
            UpdateTargets();
            _lastUpdate = Time.time;
        }

    }

    #endregion

    #region Target Management

    /// <summary>
    /// ターゲットリストを更新する
    /// </summary>
    private void UpdateTargets() {
        // Planeにカメラの六面体の形をキャッシュする
        _cameraPlanes = GeometryUtility.CalculateFrustumPlanes(_camera);

        HashSet<Transform> newTargetsInCamera = new HashSet<Transform>();
        HashSet<Transform> newTargetsInCone = new HashSet<Transform>();

        // 球体内のコライダーを検出
        int hitCount = Physics.OverlapSphereNonAlloc(
            _camera.transform.position,
            _searchRadius,
            _hitsBuffer,
            LayerMask.GetMask("Enemy")
        );

        // 検出されたコライダーを処理
        for (int i = 0; i < hitCount; i++) {
            ProcessHit(_hitsBuffer[i], _cameraPlanes, newTargetsInCamera, newTargetsInCone);
        }

        // ターゲットリストを更新
        UpdateTargetList(_targetsInCamera, _targetsInCameraSet, newTargetsInCamera);
        //UpdateTargetList(_targetsInCone, _targetsInConeSet, newTargetsInCone);
    }

    /// <summary>
    /// 検出されたコライダーを処理し、適切なリストに追加する
    /// </summary>
    private void ProcessHit(Collider hit, Plane[] planes, HashSet<Transform> newTargetsInCamera, HashSet<Transform> newTargetsInCone) {
        if (hit.CompareTag("Enemy") || hit.CompareTag("EliteMissile")) { //あとで書き直したい
            Transform target = hit.transform;
            Renderer renderer = GetCachedRenderer(target);

            // カメラの視錐台内にあり、アクティブな敵のみを処理
            if (renderer != null && GeometryUtility.TestPlanesAABB(planes, renderer.bounds) && hit.gameObject.activeSelf) {
                newTargetsInCamera.Add(target);

                // コーン内にある場合は、コーンのリストにも追加
                if (IsInCone(target)) {
                    newTargetsInCone.Add(target);
                }
            }
        }
    }

    /// <summary>
    /// ターゲットリストを効率的に更新する
    /// </summary>
    private void UpdateTargetList(List<Transform> targetList, HashSet<Transform> targetSet, HashSet<Transform> newTargets) {
        // 古いターゲットを削除
        targetList.RemoveAll(t => {
            if (!newTargets.Contains(t)) {
                targetSet.Remove(t);
                _rendererCache.Remove(t);  // キャッシュからも削除
                return true;
            }
            return false;
        });

        // 新しいターゲットを追加
        foreach (Transform newTarget in newTargets) {
            if (!targetSet.Contains(newTarget)) {
                targetList.Add(newTarget);
                targetSet.Add(newTarget);
            }
        }
    }

    /// <summary>
    /// Renderer コンポーネントをキャッシュから取得、または新たに取得してキャッシュする
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
    /// ターゲットがコーン内にあるかどうかを判定する
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
    /// デバッグ用のギズモを描画する
    /// </summary>
    private void OnDrawGizmos() {
        if (_camera != null) {
            // 球状の範囲を描画
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(_camera.transform.position, _searchRadius);

            // コーン上の円周を描画
            Gizmos.color = Color.yellow;
            float coneAngleRad = Mathf.Deg2Rad * _coneAngle / 2;

            Vector3 coneBaseCenter = _camera.transform.position + ((_playerObject.position - _camera.transform.position).normalized * _coneRange);

            Vector3 hoge = DRAWORIGIN + transform.rotation.eulerAngles;
            hoge.z = 0;

            GizmosExtensions.DrawWireCircle(coneBaseCenter, _coneRange * Mathf.Tan(coneAngleRad), 20, Quaternion.Euler(hoge));

            // コーンの範囲を描画
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
    /// coneRangeをsearchRadius以下にする制御スクリプト
    /// </summary>
    private void OnValidate() {
        if (_coneRange > _searchRadius) {
            _coneRange = _searchRadius;
        }
    }
#endif
    #endregion
}
