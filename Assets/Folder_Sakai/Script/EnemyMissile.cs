using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyMissile : MonoBehaviour, IPooledObject<EnemyMissile> {
    #region 変数 + プロパティ  

    [SerializeField, Header("�~�T�C���̃^�[�Q�b�g (�v���C���[)")]
    public Transform _playerTarget;


    [SerializeField, Header("目標ターゲット")]
    public Transform _enemyTarget;                // あとでset = value get privateに変えるかも


    [SerializeField, Header("あたりやすさ 0.1デフォ")]
    [Range(0f, 1f)]
    private float _lerpT = 0.1f;


    [SerializeField, Header("スピード")]
    public float _speed;

    [SerializeField, Header("飛行時間")]

    private float _timer = 10f;

    [SerializeField, Header("Gforceの最大値")]
    private float _maxAcceleration = 10f;

    [SerializeField, Header("G�l������𒴂���΂܂������ɂ����i�܂Ȃ��Ȃ�")]
    private float _maxHighAcceleration = 3500f;

    [SerializeField, Header("���������True�ɂȂ�")]
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
    private float _off_timerandomValue; //ミサイルの時間計算用
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

        //PlayerIsMove();
        CalculationFlying();
    }

    private void CalculationFlying() {
        if (_isOverGforce) {
            //return;
        }

        // �v���C���[�ƃ~�T�C���̑��Α��x���l��
        Vector3 playerVelocity = _playerRigidbody.velocity;
        Vector3 missileToPlayer = _playerTarget.position - transform.position;
        float distanceToPlayer = missileToPlayer.magnitude;
        Debug.Log($"�~�T�C���ƃv���C���[�̋��� : {distanceToPlayer}");

        // �v���C���[�Ƃ̋����ɉ����đ��x�𒲐�
        float adjustedMissileSpeed = _missileSpeed;

        if (distanceToPlayer < _proximityThreshold) {
            adjustedMissileSpeed *= _nearMissSlowdown; // �߂Â����猸��
            Debug.Log($"�~�T�C�����x������: {adjustedMissileSpeed}");
        }

        // �~�T�C���̑O�i
        _rigidbody.velocity = transform.forward * adjustedMissileSpeed;

        // 前進する
        _rigidbody.velocity = transform.forward * _speed;


        // G-force�v�Z
        Vector3 currentVelocity = _rigidbody.velocity;

        //(今の加速度 - 前の加速度)/ 時間
        Vector3 acceleration = (currentVelocity - _previousVelocity) / Time.fixedDeltaTime;
        _previousVelocity = currentVelocity;


        // 加速度の大きさ          1G=9.81 m/s2で割ってる

        float gForce = acceleration.magnitude / ONEG;
        _nowGforce = gForce;

        Debug.Log($"����G�̒l�� {gForce}");



        Debug.Log($"今のGの値は{gForce}");
        // Gforceが_maxAcceleration超えているときreturn
        if (gForce > _maxAcceleration) {
            Debug.LogError($"最大値を超えました今のG値は{gForce}");
            return;
        }

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


    }

    public void SetActiveFalse() {

        this.gameObject.SetActive(false);
    }
    #endregion
}
