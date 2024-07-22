using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Pool;

public class TestMissile : MonoBehaviour, IPooledObject<TestMissile>
{
    #region �ϐ�

    
    [Header("�ڕW�^�[�Q�b�g")]
    public Transform _enemyTarget;                //���Ƃ�set = value get private�ɕς��邩��

    [Header("������₷�� 0.1�f�t�H")]
    [Range(0f, 1f)]
    public float _lerpT = 0.1f;

    [Header("�X�s�[�h")]
    public float _speed;

    [Header("��s����")]
    public float _timer = 10f;

    [Header("�����_���͈̔́A��")]
    public float _randomPower = 5f;

    [Header("�����_�����K�p����鎞��")]
    public float _randomTimer = 10f;

    [Header("Gforce�̍ő�l")]
    public float _maxAcceleration = 10f;


    [Header("�G�̃^�O"), Tag]
    public string _enemyTag;

    [Header("effect�̃v�[��"), Tag]
    public ExplosionPoolManger _explosionPoolManger;

    private float _delay = 0.02f;
    private  Rigidbody _rigidbody;
    private float _offtimeValue; //�~�T�C���̎��Ԍv�Z�p
    private float _offtimeRandomValue; //�~�T�C���̎��Ԍv�Z�p
    private Vector3 _previousVelocity; //�O�̉����x

    private const float ONEG = 9.81f;  //1G�̉����x

    public IObjectPool<TestMissile> ObjectPool { get; set; }

    #endregion

    #region ���\�b�h
    //-------------------------------objectpool�C���^�[�t�F�C�X�̏���--------------------------------
    /// <summary>
    /// ������
    /// </summary>
    public void Initialize()
    {
        _offtimeValue = _timer;
        _offtimeRandomValue = _randomTimer;
    }

    /// <summary>
    /// �v�[���ɖ߂�����
    /// </summary>
    public void  ReturnToPool()

    {
        ObjectPool.Release(this);

    }


    //-------------------------------�~�T�C���̏���--------------------------------

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (_enemyTarget == null)
        {
            Debug.LogError("�A�^�b�`����ĂȂ���");
            return;
        }

        if (_enemyTarget.gameObject.activeSelf == false)  //�^�[�Q�b�g�̃A�N�e�B�u��false�̂Ƃ��Ԃ�
        {
            ReturnToPool();

        }

        _offtimeValue = Mathf.Max(0, _offtimeValue - Time.fixedDeltaTime);

        if (_offtimeValue == 0)
        {
            ReturnToPool();

        }

        CalculationFlying();

    }



    private void CalculationFlying()
    {

        // �O�i����
        _rigidbody.velocity = transform.forward * _speed;

        Vector3 currentVelocity = _rigidbody.velocity;
        //(���̉����x - �O�̉����x)/ ����
        Vector3 acceleration = (currentVelocity - _previousVelocity) / Time.fixedDeltaTime;
        _previousVelocity = currentVelocity;


        //�����x�̑傫��          1G=9.81 m/s2�Ŋ����Ă�
        float gForce = acceleration.magnitude / ONEG;


        //Gforce��maxAcceleration�����Ă��� ����hissatsu��false�̂Ƃ� return �����Ȃ���
        if (gForce > _maxAcceleration ) return;

        Vector3 diff = _enemyTarget.position - transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(diff);


        // ���ʐ��`��Ԃ��g���ĉ�]�����X�Ƀ^�[�Q�b�g�Ɍ�����
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _lerpT);


    }





    private void OnTriggerEnter(Collider other)
    {
        print("�Փ�");
        if (other.gameObject.CompareTag(_enemyTag))
        {
            print("�G�ƏՓ�");
            other.gameObject.SetActive(false);
            _explosionPoolManger.StartExplosion(other.transform);
            ReturnToPool();
        }
    }
    
    #endregion


}
