using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyMissile : MonoBehaviour, IPooledObject<EnemyMissile> {
    #region 変数 + プロパティ  



    [SerializeField, Header("ミサイルの最大追尾速度 角度")]
    [Range(0, 180f)]
    private float _maxTurnSpeed = 5f;


    [SerializeField, Header("あたりやすさ 0.1デフォ")]
    [Range(0f, 1f)]
    private float _lerpT = 0.1f;


    [SerializeField, Header("飛行時間")]
    private float _timer = 10f;

    [SerializeField, Header("Gforceの最大値")]
    private float _maxAcceleration = 10f;



    [Header("プレイヤーの入力状態を記録するフラグ")]
    [SerializeField, NaughtyAttributes.ReadOnly]
    private bool _isPlayerInputActive = false;


    public List<string> _debug;

    // 一定時間入力がないとfalseになるワールド変数 (静的変数)
    public static bool IsPlayerActive { get; private set; } = true;



    [SerializeField]
    private float _inputCheckDuration = 1f; // 入力がないと判定する時間


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

        // ターゲットのアクティブがfalseのとき返す

        if (_enemyTarget.gameObject.activeSelf == false) {

            ReturnToPool();


        }

        // タイマー offtimeValueが0になったらプールに返す

        _offtimeValue = Mathf.Max(0, _offtimeValue - Time.fixedDeltaTime);
        if (_offtimeValue == 0) {

            ReturnToPool();
        }





        PlayerIsMove();


        CalculationFlying();



    }

    private void CalculationFlying() {


        // ミサイルの回転速度制限
        Vector3 directionToTarget = _playerTarget.position + (playerVelocity * Time.fixedDeltaTime) - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _maxTurnSpeed * Time.fixedDeltaTime);

        Vector3 currentVelocity = _rigidbody.velocity;
        //(今の加速度 - 前の加速度)/ 時間
        Vector3 acceleration = (currentVelocity - _previousVelocity) / Time.fixedDeltaTime;
        _previousVelocity = currentVelocity;


        // 加速度の大きさ          1G=9.81 m/s2で割ってる
        float gForce = acceleration.magnitude / ONEG;




        // G-forceが制限を超えた場合
        if (gForce > _maxHighAcceleration) {
            Debug.Log("高G値により追尾終了");
            _isOverGforce = true;
            return;
        }
        if (gForce > _maxAcceleration) {
            Debug.LogError($"最大G値を超えました。現在のG値は {gForce}");
            return;


        Vector3 diff = _enemyTarget.position - transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(diff);


        // 球面線形補間を使って回転を徐々にターゲットに向ける
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _lerpT);



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

        // ここに衝突の判別を書く

        //if (other.gameObject.CompareTag("Player")) {
        //    print("プレイヤーに衝突");
        //    gameObject.SetActive(false);
        //}



    #endregion
}