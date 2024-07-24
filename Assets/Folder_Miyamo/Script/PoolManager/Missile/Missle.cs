using System;
using UnityEngine;

using UnityEngine.Pool;

public class Missile : MonoBehaviour
{

    [Header("�ڕW�^�[�Q�b�g")]
    public Transform target;                //���Ƃ�set = value get private�ɕς��邩��

    [Header("�K���̏ꍇ�`�F�b�N")]
    public bool _hissatsu = true;

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
    public float _random_timer = 10f;

    [Header("Gforce�̍ő�l")]
    public float _maxAcceleration = 10f;

    private IObjectPool<Missile> _objectPool;
    public IObjectPool<Missile> ObjectPool { set => _objectPool = value; }  //�O������l��ς����ꍇ�A���objectpool�ɑ�������




    private  Rigidbody _rigidbody;
    private float _offTimeValue; //�~�T�C���̎��Ԍv�Z�p
    private float _offTimerandomValue; //�~�T�C���̎��Ԍv�Z�p
    private Vector3 _previousVelocity; //�O�̉����x

    private const float ONEG = 9.81f;  //1G�̉����x
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (target == null)
        {
            Debug.LogError("�A�^�b�`����ĂȂ���"); 
            return;
        }


        if (target.gameObject.activeSelf == false)  //�^�[�Q�b�g�̃A�N�e�B�u��false�̂Ƃ��Ԃ�
        {
            PoolReurn();
        }

        _offTimeValue = Mathf.Max(0, _offTimeValue - Time.fixedDeltaTime);

        if (_offTimeValue == 0)
        {
            PoolReurn();
        }//���Ԑ؂�ɂȂ�����Ԃ�


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


        //Gforce��_maxAcceleration�����Ă��� ����_hissatsu��false�̂Ƃ� return �����Ȃ���
        if (gForce > _maxAcceleration && !_hissatsu) {
            return;
        }

        Vector3 diff = target.position - transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(diff);


        // ���ʐ��`��Ԃ��g���ĉ�]�����X�Ƀ^�[�Q�b�g�Ɍ�����
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _lerpT);


    }



    private void PoolReurn()
    {

        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;  //�I�u�W�F�N�g��false�ɂ��钼�O�܂ł����ɕt����������
        transform.rotation = new Quaternion(0, 0, 0, 0);
        _objectPool.Release(this);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            print("�G�ƏՓ�");
            PoolReurn();
        }
    }


    private void OnEnable()
    {
        _offTimeValue = _timer;
        _offTimerandomValue = _random_timer;   //�I���ɂȂ�����^�C�}�[�̒l��������
    }

    
}
