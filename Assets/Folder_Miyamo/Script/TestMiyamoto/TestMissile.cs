using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Pool;

public class TestMissile : MonoBehaviour, IPooledObject<TestMissile>
{

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


    private float _delay = 0.02f;

    private new Rigidbody rigidbody;
    private float OFFtimeValue; //�~�T�C���̎��Ԍv�Z�p
    private float OFFtimeRandomValue; //�~�T�C���̎��Ԍv�Z�p
    private Vector3 previousVelocity; //�O�̉����x

    private const float oneG = 9.81f;  //1G�̉����x



    public IObjectPool<TestMissile> ObjectPool { get; set; }


    /// <summary>
    /// ������
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
            Debug.LogError("�A�^�b�`����ĂȂ���");
            return;
        }

        if (_enemyTarget.gameObject.activeSelf == false)  //�^�[�Q�b�g�̃A�N�e�B�u��false�̂Ƃ��Ԃ�
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

        // �O�i����
        rigidbody.velocity = transform.forward * _speed;

        Vector3 currentVelocity = rigidbody.velocity;
        //(���̉����x - �O�̉����x)/ ����
        Vector3 acceleration = (currentVelocity - previousVelocity) / Time.fixedDeltaTime;
        previousVelocity = currentVelocity;


        //�����x�̑傫��          1G=9.81 m/s2�Ŋ����Ă�
        float gForce = acceleration.magnitude / oneG;


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

            StartCoroutine(DeactivateAfterDelay());
            Deactivate();


        }
    }
    private IEnumerator DeactivateAfterDelay()
    {
        yield return new WaitForSeconds(_delay);

    }



}
