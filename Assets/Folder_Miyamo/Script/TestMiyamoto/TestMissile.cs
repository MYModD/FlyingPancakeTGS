using NaughtyAttributes;
using System;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Splines;

public class TestMissile : MonoBehaviour, IPooledObject<TestMissile> {
    #region 変数 + プロパティ  

    //あとで書き直す
    public TestLockOnManager _testLockOnManager;

    [SerializeField, Header("目標ターゲット")]

    public Transform _enemyTarget;                // あとでset = value get privateに変えるかも

    [SerializeField, Header("あたりやすさ 0.1デフォ")]
    [Range(0f, 1f)]
    private float _lerpT = 0.1f;

    [SerializeField, Header("スピード")]
    private float _speed;

    [SerializeField, Header("飛行時間")]
    private float _timer = 10f;

    [SerializeField, Header("ランダムの範囲、力")]
    private float _randomPower = 5f;

    [SerializeField, Header("ランダムが適用される時間")]
    private float _random_timer = 10f;

    [SerializeField, Header("Gforceの最大値")]
    private float _maxAcceleration = 10f;

    [SerializeField, Header("敵のタグ")]
    [Tag]
    private string _enemyTag;

    [SerializeField, Header("エリートミサイルのタグ")]
    [Tag]
    private string _eliteMissile;

    [SerializeField, Header("ビルのタグ")]
    [Tag]
    private string _buildingTag;







    public ExplosionPoolManager _explosionPoolManager {
        set; private get;
    }

    private Rigidbody _rigidbody;
    private float _offtimeValue; //ミサイルの時間計算用
    private float _off_timerandomValue; //ミサイルの時間計算用
    private Vector3 _previousVelocity; //前の加速度
    private bool _hasCollided = false; // 衝突フラグ

    private const float ONEG = 9.81f;  //1Gの加速度

    public IObjectPool<TestMissile> ObjectPool {
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
        _hasCollided = false; // フラグをリセット


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
        print(_hasCollided);

        if (_enemyTarget == null) {
            Debug.LogError("アタッチされてないよ");
            return;
        }

        if (_enemyTarget.gameObject.activeSelf == false)  // ターゲットのアクティブがfalseのとき返す
        {
            ReturnToPool();
        }

        // タイマー offtimeValueが0になったらプールに返す
        _offtimeValue = Mathf.Max(0, _offtimeValue - Time.fixedDeltaTime);
        if (_offtimeValue == 0) {
            ReturnToPool();
        }

        CalculationFlying();


    }


    /// <summary>
    /// 対象物に飛翔するメソッド
    /// </summary>  

    private void CalculationFlying() {
        // 前進する
        _rigidbody.velocity = transform.forward * _speed;

        Vector3 currentVelocity = _rigidbody.velocity;
        //(今の加速度 - 前の加速度)/ 時間
        Vector3 acceleration = (currentVelocity - _previousVelocity) / Time.fixedDeltaTime;
        _previousVelocity = currentVelocity;

        // 加速度の大きさ          1G=9.81 m/s2で割ってる
        float gForce = acceleration.magnitude / ONEG;

        // Gforceが_maxAcceleration超えているときreturn
        if (gForce > _maxAcceleration) {
            return;
        }

        Vector3 diff = _enemyTarget.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(diff);


        // 球面線形補間を使って回転を徐々にターゲットに向ける
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _lerpT);
    }




    private void OnTriggerEnter(Collider other) {
        print("衝突");

        // 敵のタグがが普通の敵で標的の敵と同じだったとき
        if (other.transform == _enemyTarget) {


            // 敵のタグが普通の敵だったとき
            if (other.gameObject.CompareTag(_enemyTag)) {
                _hasCollided = true; // 衝突フラグをセット
                print($"{other.gameObject.name}に衝突");
                ReturnToPool();                             // ミサイルをプールに変換
            }

        } 
        #endregion

    }
}
