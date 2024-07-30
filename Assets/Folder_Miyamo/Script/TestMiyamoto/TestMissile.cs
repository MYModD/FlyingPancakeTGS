using NaughtyAttributes;
using System;
using UnityEngine;
using UnityEngine.Pool;

public class TestMissile : MonoBehaviour, IPooledObject<TestMissile> {
    #region フィールド + プロパティ

    [SerializeField, Header("ターゲット")]
    public Transform _enemyTarget; // 敵のターゲット

    [SerializeField, Header("回転補間 0.1デフォ")]
    [Range(0f, 1f)]
    private float _lerpT = 0.1f;

    [SerializeField, Header("スピード")]
    private float _speed;

    [SerializeField, Header("発射時間")]
    private float _timer = 10f;

    [SerializeField, Header("ランダムな力の範囲")]
    private float _randomPower = 5f;

    [SerializeField, Header("ランダムな力の適用時間")]
    private float _random_timer = 10f;

    [SerializeField, Header("Gforceの最大値")]
    private float _maxAcceleration = 10f;

    [SerializeField, Header("敵のタグ")]
    [Tag]
    private string _enemyTag;

    [SerializeField, Header("エリートミサイルのタグ")]
    [Tag]
    private string _eliteMissile;

    public ExplosionPoolManager _explosionPoolManager {
        set; private get;
    }

    private Rigidbody _rigidbody;
    private float _offtimeValue; // ミサイルの残り時間
    private float _off_timerandomValue; // ランダム力の適用時間の残り
    private Vector3 _previousVelocity; // 前フレームの速度

    private const float ONEG = 9.81f; // 1Gの加速度

    public IObjectPool<TestMissile> ObjectPool {
        get; set;
    }

    #endregion

    #region メソッド

    // -------------------------------オブジェクトプールインターフェイスの実装--------------------------------
    /// <summary>
    /// 初期化
    /// </summary>
    public void Initialize() {
        _offtimeValue = _timer;
    }

    /// <summary>
    /// プールに戻す
    /// </summary>
    public void ReturnToPool() {
        ObjectPool.Release(this);
    }

    // -------------------------------ミサイルの処理--------------------------------

    void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        if (_enemyTarget == null) {
            Debug.LogError("ターゲットが設定されていません");
            return;
        }

        if (!_enemyTarget.gameObject.activeSelf) // ターゲットが非アクティブの場合
        {
            ReturnToPool();
            return;
        }

        // タイマーが0になったらプールに戻す
        _offtimeValue = Mathf.Max(0, _offtimeValue - Time.fixedDeltaTime);
        if (_offtimeValue == 0) {
            ReturnToPool();
            return;
        }

        CalculationFlying();
    }

    private void CalculationFlying() {
        // 前進する
        _rigidbody.velocity = transform.forward * _speed;

        Vector3 currentVelocity = _rigidbody.velocity;
        // (現在の速度 - 前フレームの速度) / 時間
        Vector3 acceleration = (currentVelocity - _previousVelocity) / Time.fixedDeltaTime;
        _previousVelocity = currentVelocity;

        // 加速度の大きさを1Gで割る
        float gForce = acceleration.magnitude / ONEG;

        // Gforceが_maxAccelerationを超えたら終了
        if (gForce > _maxAcceleration) {
            return;
        }

        Vector3 diff = _enemyTarget.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(diff);

        // 徐々にターゲットに向けて回転する
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _lerpT);
    }

    private void OnTriggerEnter(Collider other) {
        // 敵のタグを持つオブジェクトに衝突した場合
        if (other.gameObject.CompareTag(_enemyTag)) {
            other.gameObject.SetActive(false); // 敵を非アクティブにする
            _explosionPoolManager.StartExplosion(other.transform); // 爆発を開始する
            ReturnToPool(); // ミサイルをプールに戻す
        }

        // エリートミサイルのタグを持つオブジェクトに衝突した場合
        if (other.gameObject.CompareTag(_eliteMissile)) {
            other.GetComponent<EliteEnemyHP>().DecreaseHP();
            Debug.Log("エリートミサイルに命中しました");
        }
    }

    #endregion
}
