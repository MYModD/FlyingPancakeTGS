using NaughtyAttributes;
using System;
using UnityEngine;
using UnityEngine.Pool;

public class TestMissile : MonoBehaviour, IPooledObject<TestMissile> {
    #region 変数


    [Header("目標ターゲット")]
    public Transform _enemyTarget;                // あとでset = value get privateに変えるかも

    [Header("あたりやすさ 0.1デフォ")]
    [Range(0f, 1f)]
    public float _lerpT = 0.1f;

    [Header("スピード")]
    public float _speed;

    [Header("飛行時間")]
    public float _timer = 10f;

    [Header("ランダムの範囲、力")]
    public float _randomPower = 5f;

    [Header("ランダムが適用される時間")]
    public float _random_timer = 10f;

    [Header("Gforceの最大値")]
    public float _maxAcceleration = 10f;


    [Header("敵のタグ"), Tag]
    [SerializeField]
    private  string _enemyTag;

    [Header("敵のタグ"), Tag]
    [SerializeField]
    private string _eliteMissile;



    public ExplosionPoolManager _explosionPoolManager{
        set; private get;
    }


    private float _delay = 0.02f;
    private Rigidbody _rigidbody;
    private float _offtimeValue; //ミサイルの時間計算用
    private float _off_timerandomValue; //ミサイルの時間計算用
    private Vector3 _previousVelocity; //前の加速度

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

        // 敵のタグがが普通の敵だったとき
        if (other.gameObject.CompareTag(_enemyTag)) {

            print($"{other.gameObject.name}に衝突");
            other.gameObject.SetActive(false);                           // 敵のsetActiveをfalse
            _explosionPoolManager.StartExplosion(other.transform);       // 爆発開始
            ReturnToPool();                                              // ミサイルをプールに変換
        }


        // 敵のタグがエリートミサイルだったとき
        if (other.gameObject.CompareTag(_eliteMissile)) {
            

            
        
        }

    }

    #endregion


}
