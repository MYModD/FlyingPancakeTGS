using NaughtyAttributes;
using System;
using UnityEngine;
using UnityEngine.Pool;

public class TestMissile : MonoBehaviour, IPooledObject<TestMissile> {
    #region �ϐ� + �v���p�e�B  


    [Header("�~�T�C���X�^�b�N�i��)")]
    public MissileStuck _missileStuck;

    [Header("�ڕW�^�[�Q�b�g")]
    public Transform _enemyTarget;                // ���Ƃ�set = value get private�ɕς��邩��

    [SerializeField, Header("������₷�� 0.1�f�t�H")]
    [Range(0f, 1f)]
    private  float _lerpT = 0.1f;

    [SerializeField, Header("�X�s�[�h")]
    private  float _speed;

    [SerializeField, Header("��s����")]
    private  float _timer = 10f;

    [SerializeField, Header("�����_���͈̔́A��")]
    private float _randomPower = 5f;

    [SerializeField, Header("�����_�����K�p����鎞��")]
    private  float _random_timer = 10f;

    [SerializeField, Header("Gforce�̍ő�l")]
    private  float _maxAcceleration = 10f;


    [SerializeField,Header("�G�̃^�O")]
    [Tag]
    private string _enemyTag;

    [SerializeField,Header("�G�̃^�O")]
    [Tag]
    private string _eliteMissile;


    public ExplosionPoolManager _explosionPoolManager{
        set; private get;
    }


    private Rigidbody _rigidbody;
    private float _offtimeValue; //�~�T�C���̎��Ԍv�Z�p
    private float _off_timerandomValue; //�~�T�C���̎��Ԍv�Z�p
    private Vector3 _previousVelocity; //�O�̉����x

    private const float ONEG = 9.81f;  //1G�̉����x

    public IObjectPool<TestMissile> ObjectPool {
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

        if (_enemyTarget.gameObject.activeSelf == false)  // �^�[�Q�b�g�̃A�N�e�B�u��false�̂Ƃ��Ԃ�
        {
            ReturnToPool();

        }

        // �^�C�}�[ offtimeValue��0�ɂȂ�����v�[���ɕԂ�
        _offtimeValue = Mathf.Max(0, _offtimeValue - Time.fixedDeltaTime);
        if (_offtimeValue == 0) {

            ReturnToPool();
        }


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


        // Gforce��_maxAcceleration�����Ă���Ƃ�return
        if (gForce > _maxAcceleration) {
            return;
        }

        Vector3 diff = _enemyTarget.position - transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(diff);


        // ���ʐ��`��Ԃ��g���ĉ�]�����X�Ƀ^�[�Q�b�g�Ɍ�����
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _lerpT);


    }





    private void OnTriggerEnter(Collider other) {
        print("�Փ�");

        // �G�̃^�O�������ʂ̓G�ŕW�I�̓G�Ɠ����������Ƃ�
        if (other.gameObject.CompareTag(_enemyTag) || other.transform == _enemyTarget) {

            print($"{other.gameObject.name}�ɏՓ�");
            other.gameObject.SetActive(false);                           // �G��setActive��false
            _explosionPoolManager.StartExplosion(other.transform);       // �����J�n
            _missileStuck.Initialize();
            ReturnToPool();                                              // �~�T�C�����v�[���ɕϊ�
        }


        // �G�̃^�O���G���[�g�~�T�C���������Ƃ�
        if (other.gameObject.CompareTag(_eliteMissile)) {

            other.GetComponent<EliteEnemyHP>().DecreaseHP();
            _missileStuck.Initialize();
            Debug.Log("�G���[�g�~�T�C���ɂ���������");
            
        
        }

    }

    #endregion


}
