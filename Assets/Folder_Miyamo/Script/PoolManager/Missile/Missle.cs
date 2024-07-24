using System;
using UnityEngine;

using UnityEngine.Pool;

public class Missile : MonoBehaviour
{

    [Header("目標ターゲット")]
    public Transform target;                //あとでset = value get privateに変えるかも

    [Header("必中の場合チェック")]
    public bool _hissatsu = true;

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

    private IObjectPool<Missile> _objectPool;
    public IObjectPool<Missile> ObjectPool { set => _objectPool = value; }  //外部から値を変えた場合、上のobjectpoolに代入される




    private  Rigidbody _rigidbody;
    private float _offTimeValue; //ミサイルの時間計算用
    private float _offTimerandomValue; //ミサイルの時間計算用
    private Vector3 _previousVelocity; //前の加速度

    private const float ONEG = 9.81f;  //1Gの加速度
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (target == null)
        {
            Debug.LogError("アタッチされてないよ"); 
            return;
        }


        if (target.gameObject.activeSelf == false)  //ターゲットのアクティブがfalseのとき返す
        {
            PoolReurn();
        }

        _offTimeValue = Mathf.Max(0, _offTimeValue - Time.fixedDeltaTime);

        if (_offTimeValue == 0)
        {
            PoolReurn();
        }//時間切れになったら返す


        CalculationFlying();

    }



    private void CalculationFlying()
    {

        // 前進する
        _rigidbody.velocity = transform.forward * _speed;

        Vector3 currentVelocity = _rigidbody.velocity;
        //(今の加速度 - 前の加速度)/ 時間
        Vector3 acceleration = (currentVelocity - _previousVelocity) / Time.fixedDeltaTime;
        _previousVelocity = currentVelocity;


        //加速度の大きさ          1G=9.81 m/s2で割ってる
        float gForce = acceleration.magnitude / ONEG;


        //Gforceが_maxAcceleration超えている かつ_hissatsuがfalseのとき return 処理なくす
        if (gForce > _maxAcceleration && !_hissatsu) {
            return;
        }

        Vector3 diff = target.position - transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(diff);


        // 球面線形補間を使って回転を徐々にターゲットに向ける
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _lerpT);


    }



    private void PoolReurn()
    {

        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;  //オブジェクトをfalseにする直前までここに付け足すかも
        transform.rotation = new Quaternion(0, 0, 0, 0);
        _objectPool.Release(this);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            print("敵と衝突");
            PoolReurn();
        }
    }


    private void OnEnable()
    {
        _offTimeValue = _timer;
        _offTimerandomValue = _random_timer;   //オンになったらタイマーの値を初期化
    }

    
}
