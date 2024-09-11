using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyMissile : MonoBehaviour, IPooledObject<EnemyMissile> {
    #region 変数 + プロパティ  

    [SerializeField, Header("ミサイルのターゲット (プレイヤー)")]
    public Transform _playerTarget;

    [SerializeField, Header("ミサイルの速度")]
    private float _missileSpeed = 100f;

    [SerializeField, Header("ミサイルの最大追尾速度 角度")]
    [Range(0, 180f)]
    private float _maxTurnSpeed = 5f;

    [SerializeField, Header("プレイヤーの速度")]
    public Rigidbody _playerRigidbody;

    [SerializeField, Header("距離が近づいたら減速する範囲")]
    private float _proximityThreshold = 50f;

    [SerializeField, Header("ギリギリ感を演出するための減速係数")]
    [Range(0, 1f)]
    private float _nearMissSlowdown = 0.5f;

    [SerializeField, Header("あたりやすさ 0.1デフォ")]
    [Range(0f, 1f)]
    private float _lerpT = 0.1f;

    [SerializeField, Header("飛行時間")]
    private float _timer = 10f;

    [SerializeField, Header("Gforceの最大値")]
    private float _maxAcceleration = 10f;

    [SerializeField, Header("G値がこれを超えればまっすぐにしか進まなくなる")]
    private float _maxHighAcceleration = 3500f;

    [SerializeField, Header("↑超えればTrueになる")]
    private bool _isOverGforce = false;

    [Header("プレイヤーの入力状態を記録するフラグ")]
    [SerializeField, NaughtyAttributes.ReadOnly]
    private bool _isPlayerInputActive = false;

    public List<string> _debug;

    // 一定時間入力がないとfalseになるワールド変数 (静的変数)
    public static bool IsPlayerActive { get; private set; } = true;

    [SerializeField]
    private float _inputCheckDuration = 1f; // 入力がないと判定する時間

    [SerializeField, NaughtyAttributes.ReadOnly]
    private float _nowGforce;

    public ExplosionPoolManager _explosionPoolManager {
        set; private get;
    }

    private Rigidbody _rigidbody;
    private float _offtimeValue; //ミサイルの時間計算用
    private Vector3 _previousVelocity; //前の加速度

    private const float ONEG = 9.81f;  //1Gの加速度
    private const float MINIMUMALLOWEDVALUE = 0.05f;

    private float _inputCheckTimer;   // タイマー計算用

    public IObjectPool<EnemyMissile> ObjectPool {
        get; set;
    }

    #endregion

    #region メソッド
    //-------------------------------objectpoolインターフェイスの処理--------------------------------
    /// <summary>
    /// 初期化
    /// </summary>
    public void Initialize() {
        _offtimeValue = _timer;
        _isOverGforce = false;
    }

    /// <summary>
    /// プールに戻す処理
    /// </summary>
    public void ReturnToPool() {
        ObjectPool.Release(this);
    }

    //-------------------------------ミサイルの処理--------------------------------

    void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        if (_playerTarget == null) {
            Debug.LogError("プレイヤーターゲットがアタッチされていません");
            return;
        }

        // ターゲットのアクティブがfalseのとき返す
        if (_playerTarget.gameObject.activeSelf == false) {
            ReturnToPool();
        }

        // タイマーが0になったらプールに返す
        _offtimeValue = Mathf.Max(0, _offtimeValue - Time.fixedDeltaTime);
        if (_offtimeValue == 0) {
            ReturnToPool();
        }

        //PlayerIsMove();
        CalculationFlying();
    }

    private void CalculationFlying() {
        if (_isOverGforce) {
            //return;
        }

        // プレイヤーとミサイルの相対速度を考慮
        Vector3 playerVelocity = _playerRigidbody.velocity;
        Vector3 missileToPlayer = _playerTarget.position - transform.position;
        float distanceToPlayer = missileToPlayer.magnitude;
        Debug.Log($"ミサイルとプレイヤーの距離 : {distanceToPlayer}");

        // プレイヤーとの距離に応じて速度を調整
        float adjustedMissileSpeed = _missileSpeed;

        if (distanceToPlayer < _proximityThreshold) {
            adjustedMissileSpeed *= _nearMissSlowdown; // 近づいたら減速
            Debug.Log($"ミサイル速度が減速: {adjustedMissileSpeed}");
        }

        // ミサイルの前進
        _rigidbody.velocity = transform.forward * adjustedMissileSpeed;

        // ミサイルの回転速度制限
        Vector3 directionToTarget = _playerTarget.position + (playerVelocity * Time.fixedDeltaTime) - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _maxTurnSpeed * Time.fixedDeltaTime);

        // G-force計算
        Vector3 currentVelocity = _rigidbody.velocity;
        Vector3 acceleration = (currentVelocity - _previousVelocity) / Time.fixedDeltaTime;
        _previousVelocity = currentVelocity;
        float gForce = acceleration.magnitude / ONEG;
        _nowGforce = gForce;

        Debug.Log($"今のGの値は {gForce}");

        // G-forceが制限を超えた場合
        if (gForce > _maxHighAcceleration) {
            Debug.Log("高G値により追尾終了");
            _isOverGforce = true;
            return;
        }
        if (gForce > _maxAcceleration) {
            Debug.LogError($"最大G値を超えました。現在のG値は {gForce}");
            return;
        }
    }

    private void PlayerIsMove() {
        float inputHorizontal = Input.GetAxis("Horizontal");
        float inputVertical = Input.GetAxis("Vertical");
        Debug.Log($"{inputHorizontal}  :{inputVertical}");

        // 入力が一定時間なかったらboolを変えるスクリプト
        if (Mathf.Abs(inputHorizontal) > MINIMUMALLOWEDVALUE || Mathf.Abs(inputVertical) > MINIMUMALLOWEDVALUE) {
            _isPlayerInputActive = true;
            _inputCheckTimer = 0f;
            IsPlayerActive = true;
        } else {
            _inputCheckTimer += Time.deltaTime;

            if (_inputCheckTimer >= _inputCheckDuration) {
                _isPlayerInputActive = false;
                IsPlayerActive = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        _debug.Add($"{other.gameObject.name}   :    {other.tag}  ");

        // 衝突判別ロジック (必要に応じて追加)
        // if (other.gameObject.CompareTag("Player")) {
        //     print("プレイヤーに衝突");
        //     gameObject.SetActive(false);
        // }
    }

    #endregion
}