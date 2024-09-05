using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyMissile : MonoBehaviour, IPooledObject<EnemyMissile> {
    #region �ϐ� + �v���p�e�B  


    [SerializeField, Header("�ڕW�^�[�Q�b�g")]
    public Transform _enemyTarget;                // ���Ƃ�set = value get private�ɕς��邩��

    [SerializeField, Header("������₷�� 0.1�f�t�H")]
    [Range(0f, 1f)]
    private float _lerpT = 0.1f;

    [SerializeField, Header("�X�s�[�h")]
    public float _speed;

    [SerializeField, Header("��s����")]
    private float _timer = 10f;

    [SerializeField, Header("Gforce�̍ő�l")]
    private float _maxAcceleration = 10f;


    [Header("�v���C���[�̓��͏�Ԃ��L�^����t���O")]
    [SerializeField, NaughtyAttributes.ReadOnly]
    private bool _isPlayerInputActive = false;


    // ��莞�ԓ��͂��Ȃ���false�ɂȂ郏�[���h�ϐ� (�ÓI�ϐ�)
    public static bool IsPlayerActive { get; private set; } = true;



    [SerializeField]
    private float _inputCheckDuration = 1f; // ���͂��Ȃ��Ɣ��肷�鎞��


    public ExplosionPoolManager _explosionPoolManager {
        set; private get;
    }


    private Rigidbody _rigidbody;
    private float _offtimeValue; //�~�T�C���̎��Ԍv�Z�p
    private float _off_timerandomValue; //�~�T�C���̎��Ԍv�Z�p
    private Vector3 _previousVelocity; //�O�̉����x

    private const float ONEG = 9.81f;  //1G�̉����x
    private const float MINIMUMALLOWEDVALUE = 0.05f;


    private float _inputCheckTimer;   // �^�C�}�[�v�Z�p


    public IObjectPool<EnemyMissile> ObjectPool {
        get; set;
    }



    #endregion

    #region ���\�b�h
    //-------------------------------objectpool�C���^�[�t�F�C�X�̏���--------------------------------
    /// <summary>
    /// ������
    /// </summary>
    public void Initialize() {
        _offtimeValue = _timer;
    }

    /// <summary>
    /// �v�[���ɖ߂�����
    /// </summary>
    public void ReturnToPool() {

        ObjectPool.Release(this);
    }


    //-------------------------------�~�T�C���̏���--------------------------------

    void Awake() {
        _rigidbody = GetComponent<Rigidbody>();

    }

    void FixedUpdate() {
        if (_enemyTarget == null) {
            Debug.LogError("�A�^�b�`����ĂȂ���");
            return;
        }


        // �^�[�Q�b�g�̃A�N�e�B�u��false�̂Ƃ��Ԃ�
        if (_enemyTarget.gameObject.activeSelf == false) {
            ReturnToPool();

        }

        // �^�C�}�[ offtimeValue��0�ɂȂ�����v�[���ɕԂ�
        _offtimeValue = Mathf.Max(0, _offtimeValue - Time.fixedDeltaTime);
        if (_offtimeValue == 0) {

            ReturnToPool();
        }




        PlayerIsMove();


        CalculationFlying();



    }

    private void CalculationFlying() {

        // �O�i����
        _rigidbody.velocity = transform.forward * _speed;

        Vector3 currentVelocity = _rigidbody.velocity;
        //(���̉����x - �O�̉����x)/ ����
        Vector3 acceleration = (currentVelocity - _previousVelocity) / Time.fixedDeltaTime;
        _previousVelocity = currentVelocity;


        // �����x�̑傫��          1G=9.81 m/s2�Ŋ����Ă�
        float gForce = acceleration.magnitude / ONEG;



        Debug.Log($"����G�̒l��{gForce}");
        // Gforce��_maxAcceleration�����Ă���Ƃ�return
        if (gForce > _maxAcceleration) {
            Debug.LogError($"�ő�l�𒴂��܂�������G�l��{gForce}");
            return;
        }

        Vector3 diff = _enemyTarget.position - transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(diff);


        // ���ʐ��`��Ԃ��g���ĉ�]�����X�Ƀ^�[�Q�b�g�Ɍ�����
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _lerpT);


    }


    private void PlayerIsMove() {

        float inputHorizontal = Input.GetAxis("Horizontal");
        float inputVertical = Input.GetAxis("Vertical");
        Debug.Log($"{inputHorizontal}  :{inputVertical}");

        // ���͂���莞�ԂȂ�������bool��ς���X�N���v�g
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


        // �����ɏՓ˂̔��ʂ�����
        if (other.gameObject.CompareTag("Player")) {
            print("�v���C���[�ɏՓ�");
            gameObject.SetActive(false);
        }
    }

    #endregion
}

