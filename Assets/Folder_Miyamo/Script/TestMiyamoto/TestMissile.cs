using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Pool;

public class TestMissile : MonoBehaviour, IPooledObject<TestMissile>
{

    [Header("目標ターゲット")]
    public Transform _enemyTarget;                //あとでset = value get privateに変えるかも

    

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
    public float _randomTimer = 10f;

    [Header("Gforceの最大値")]
    public float _maxAcceleration = 10f;


    [Header("敵のタグ"), Tag]
    public string _enemyTag;


    private float _delay = 0.02f;

    private new Rigidbody rigidbody;
    private float OFFtimeValue; //ミサイルの時間計算用
    private float OFFtimeRandomValue; //ミサイルの時間計算用
    private Vector3 previousVelocity; //前の加速度

    private const float oneG = 9.81f;  //1Gの加速度



    public IObjectPool<TestMissile> ObjectPool { get; set; }


    /// <summary>
    /// 初期化
    /// </summary>
    public void Initialize()
    {
        OFFtimeValue = _timer;
        OFFtimeRandomValue = _randomTimer;
    }
    public void Deactivate()
    {
        ObjectPool.Release(this);

    }







    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (_enemyTarget == null)
        {
            Debug.LogError("アタッチされてないよ");
            return;
        }

        if (_enemyTarget.gameObject.activeSelf == false)  //ターゲットのアクティブがfalseのとき返す
        {
            Deactivate();

        }

        OFFtimeValue = Mathf.Max(0, OFFtimeValue - Time.fixedDeltaTime);

        if (OFFtimeValue == 0)
        {
            Deactivate();

        }




        CalculationFlying();

    }



    private void CalculationFlying()
    {

        // 前進する
        rigidbody.velocity = transform.forward * _speed;

        Vector3 currentVelocity = rigidbody.velocity;
        //(今の加速度 - 前の加速度)/ 時間
        Vector3 acceleration = (currentVelocity - previousVelocity) / Time.fixedDeltaTime;
        previousVelocity = currentVelocity;


        //加速度の大きさ          1G=9.81 m/s2で割ってる
        float gForce = acceleration.magnitude / oneG;


        //GforceがmaxAcceleration超えている かつhissatsuがfalseのとき return 処理なくす
        if (gForce > _maxAcceleration ) return;

        Vector3 diff = _enemyTarget.position - transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(diff);


        // 球面線形補間を使って回転を徐々にターゲットに向ける
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _lerpT);


    }





    private void OnTriggerEnter(Collider other)
    {
        print("衝突");
        if (other.gameObject.CompareTag(_enemyTag))
        {
            print("敵と衝突");

            StartCoroutine(DeactivateAfterDelay());
            Deactivate();


        }
    }
    private IEnumerator DeactivateAfterDelay()
    {
        yield return new WaitForSeconds(_delay);

    }



}
