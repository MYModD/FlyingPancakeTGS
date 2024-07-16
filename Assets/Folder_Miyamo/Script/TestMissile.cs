using System;
using UnityEngine;

using UnityEngine.Pool;

public class TestMissile : MonoBehaviour
{

    [Header("目標ターゲット")]
    public Transform target;                //あとでset = value get privateに変えるかも

    [Header("必中の場合チェック")]
    public bool hissatsu = true;

    [Header("あたりやすさ 0.1デフォ")]
    [Range(0f, 1f)]
    public float lerpT = 0.1f;

    [Header("スピード")]
    public float speed;

    [Header("飛行時間")]
    public float timer = 10f;

    [Header("ランダムの範囲、力")]
    public float randomPower = 5f;

    [Header("ランダムが適用される時間")]
    public float randomTimer = 10f;

    [Header("Gforceの最大値")]
    public float maxAcceleration = 10f;

    //private IObjectPool<Missile> objectPool;
    //public IObjectPool<Missile> ObjectPool { set => objectPool = value; }  //外部から値を変えた場合、上のobjectpoolに代入される




    private new Rigidbody rigidbody;
    private float OFFtimeValue; //ミサイルの時間計算用
    private float OFFtimeRandomValue; //ミサイルの時間計算用
    private Vector3 previousVelocity; //前の加速度

    private const float oneG = 9.81f;  //1Gの加速度
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
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
            //PoolReurn();
            Destroy(this.gameObject);
        }

        OFFtimeValue = Mathf.Max(0, OFFtimeValue - Time.fixedDeltaTime);

        if (OFFtimeValue == 0)
        {
            //PoolReurn();
            Destroy(this.gameObject);

            
        }//時間切れになったら返す


        CalculationFlying();

    }



    private void CalculationFlying()
    {

        // 前進する
        rigidbody.velocity = transform.forward * speed;

        Vector3 currentVelocity = rigidbody.velocity;
        //(今の加速度 - 前の加速度)/ 時間
        Vector3 acceleration = (currentVelocity - previousVelocity) / Time.fixedDeltaTime;
        previousVelocity = currentVelocity;


        //加速度の大きさ          1G=9.81 m/s2で割ってる
        float gForce = acceleration.magnitude / oneG;


        //GforceがmaxAcceleration超えている かつhissatsuがfalseのとき return 処理なくす
        if (gForce > maxAcceleration && !hissatsu) return;

        Vector3 diff = target.position - transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(diff);


        // 球面線形補間を使って回転を徐々にターゲットに向ける
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, lerpT);


    }



    //private void PoolReurn()
    //{

    //    rigidbody.velocity = Vector3.zero;
    //    rigidbody.angularVelocity = Vector3.zero;  //オブジェクトをfalseにする直前までここに付け足すかも
    //    transform.rotation = new Quaternion(0, 0, 0, 0);
    //    objectPool.Release(this);

    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            print("敵と衝突");
            Destroy(this.gameObject);
        }
    }


    private void OnEnable()
    {
        OFFtimeValue = timer;
        OFFtimeRandomValue = randomTimer;   //オンになったらタイマーの値を初期化
    }


}
