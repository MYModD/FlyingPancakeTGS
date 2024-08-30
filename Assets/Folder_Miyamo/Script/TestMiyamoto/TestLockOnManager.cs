using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class TestLockOnManager : MonoBehaviour {

    [Header("カメラの視界に入っているターゲットのリスト")]
    public List<Transform> _targetsInCamera = new List<Transform>();

    [Header("錐体内に入っているターゲットのdebug用リスト")]
    public List<Transform> _targetsInCone = new List<Transform>();

    public MissileStuck[] _missileStucks;


    [Header("プレイヤーのTransformを指定")]
    [SerializeField, Header("プレイヤーのTransform")]
    private Transform _player;

    [SerializeField, Header("カメラ指定")]
    private Camera _camera;

    [SerializeField, Header("spherecastの半径")]
    public float _searchRadius = 95f;

    [SerializeField, Range(0f, 180f)]
    [Header("コーンの角度")]
    public  float _coneAngle = 45f;

    [SerializeField]
    [Header("コーンの長さ、半径")]
    public  float _coneRange;


    [HideInInspector]
    public Vector3 _circleCenterPostion;
    [HideInInspector]
    public Quaternion  _circleRotation;

    public bool _canAdd = true;
    public float _coolTime;
    public RectTransform _rectTransform;


    
    readonly private Vector3 _drawOrigin = new Vector3(90, 0, 0);

    private Plane[] _cameraPlanes;

    private void Start() {

        // UI用
        _rectTransform.sizeDelta = new Vector2(_coneAngle * 13.75f, _coneAngle * 13.75f);


    }





    void Update() {
        UpdateTargets();



        // 見やすくするデバッグ用
        for (int i = 0; i < _missileStucks.Length; i++) {
            if (_missileStucks[i]._enemyTarget != null && _missileStucks[i]._isValueAssignable == false) {

                _targetsInCone.Add(_missileStucks[i]._enemyTarget);
            }
        }
    }

    private void UpdateTargets() {

        // Plane型の変数にカメラの情報をいれる+カメラのリストを削除する
        _cameraPlanes = GeometryUtility.CalculateFrustumPlanes(_camera);
        _targetsInCamera.Clear();
        _targetsInCone.Clear();



        // カメラの位置から一定の半径の球状のコライダーの配列を取得する
        Collider[] hits = Physics.OverlapSphere(

            _camera.transform.position,
            _searchRadius,
            LayerMask.GetMask("Enemy")

        );

        // 一番近い敵を探すためにnullとfloat.MaxValueを使用
        Transform minDistanceTarget = null;
        float minDistance = float.MaxValue;


        // コライダーの配列Foreach
        foreach (Collider hit in hits) {
            if (!hit.CompareTag("Enemy")) {
                continue;
            }

            //ターゲットをcoliderのtransform,レンダーを取得
            Transform target = hit.transform;
            Renderer renderer = target.GetComponent<Renderer>();
            if (renderer == null) {
                Debug.LogError("meshrendererがついていないよ");
                continue;
            }

            // カメラ内に敵がいる かつ 敵のactiveがTrueのとき それ以外はreturn
            if (IsInFrustum(renderer, _cameraPlanes) && hit.gameObject.activeSelf) {
                _targetsInCamera.Add(target);
            } else {
                continue;
            }


            // コーン内に敵がいる かつ 敵のactiveがTrue
            if (IsInCone(target) && target.gameObject.activeSelf &&hit.gameObject.activeSelf) {

                // コーン内に複数の敵がいる場合一番近い敵を探す
                float distance = Vector3.Distance(target.position, _camera.transform.position);
                if (distance < minDistance) {

                    minDistanceTarget = target;
                }

            }
        }


        // ターゲットがnullではなく かつ canAddがtrueのとき
        if (minDistanceTarget != null && _canAdd) {

            for (int i = 0; i < _missileStucks.Length; i++) {

                // minDistanceTargetがmissileStucksの配列内にあるときBreak
                if (minDistanceTarget == _missileStucks[i]._enemyTarget) {

                    break;
                }

                // 0から初めて_enemyTargetがnullのとき代入するための
                // メソッドを呼び出しクールタイムのコルーチンを呼ぶ
                if (_missileStucks[i]._enemyTarget == null) {

                    _missileStucks[i].TargetLockOn(minDistanceTarget);
                    StartCoroutine(nameof(CanBoolTimer));
                    break;
                }

            }
        }


    }

    /// <summary>
    /// falseにし一定時間後にtrueにする
    /// </summary>
    IEnumerator CanBoolTimer() {

        _canAdd = false;
        Debug.Log(_canAdd);
        yield return new WaitForSeconds(_coolTime);
        _canAdd = true;
        Debug.Log(_canAdd);
    }


    /// <summary>
    /// カメラとrenderが交差しているか renderのサイズで計測しているので若干の誤差あり
    /// </summary>
    private bool IsInFrustum(Renderer renderer, Plane[] planes) {
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }


    /// <summary>
    /// targetがコーン内にいるか ベクトルを正規化して角度が合っているか判別
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
            Debug.Log("カメラかプレイヤーつけてないよ");
            return;
        }
        // 球状の範囲を描画
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(_camera.transform.position, _searchRadius);

        // コーンの方向と回転を計算
        Vector3 coneDirection = (_player.position - _camera.transform.position).normalized;
        Quaternion coneRotation = Quaternion.LookRotation(coneDirection);


        // コーン上の円周を描画
        Gizmos.color = Color.yellow;
        float coneAngleRad = Mathf.Deg2Rad * _coneAngle / 2;
        Vector3 coneBaseCenter = _camera.transform.position + (coneDirection * _coneRange);

        //UI用にキャッシュ
        _circleCenterPostion = coneBaseCenter;

        Vector3 hoge = coneRotation.eulerAngles + _drawOrigin;
        hoge.z = 0;

        //UI用にキャッシュ
        _circleRotation = Quaternion.Euler(hoge);

        GizmosExtensions.DrawWireCircle(coneBaseCenter, _coneRange * Mathf.Tan(coneAngleRad), 20, Quaternion.Euler(hoge));

        // コーンの範囲を描画
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
