using System;
using UnityEngine;

using UnityEngine.Pool;

public class TestMissile : MonoBehaviour
{

    [Header("�ڕW�^�[�Q�b�g")]
    public Transform target;                //���Ƃ�set = value get private�ɕς��邩��

    [Header("�K���̏ꍇ�`�F�b�N")]
    public bool hissatsu = true;

    [Header("������₷�� 0.1�f�t�H")]
    [Range(0f, 1f)]
    public float lerpT = 0.1f;

    [Header("�X�s�[�h")]
    public float speed;

    [Header("��s����")]
    public float timer = 10f;

    [Header("�����_���͈̔́A��")]
    public float randomPower = 5f;

    [Header("�����_�����K�p����鎞��")]
    public float randomTimer = 10f;

    [Header("Gforce�̍ő�l")]
    public float maxAcceleration = 10f;

    //private IObjectPool<Missile> objectPool;
    //public IObjectPool<Missile> ObjectPool { set => objectPool = value; }  //�O������l��ς����ꍇ�A���objectpool�ɑ�������




    private new Rigidbody rigidbody;
    private float OFFtimeValue; //�~�T�C���̎��Ԍv�Z�p
    private float OFFtimeRandomValue; //�~�T�C���̎��Ԍv�Z�p
    private Vector3 previousVelocity; //�O�̉����x

    private const float oneG = 9.81f;  //1G�̉����x
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
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
            //PoolReurn();
            Destroy(this.gameObject);
        }

        OFFtimeValue = Mathf.Max(0, OFFtimeValue - Time.fixedDeltaTime);

        if (OFFtimeValue == 0)
        {
            //PoolReurn();
            Destroy(this.gameObject);

            
        }//���Ԑ؂�ɂȂ�����Ԃ�


        CalculationFlying();

    }



    private void CalculationFlying()
    {

        // �O�i����
        rigidbody.velocity = transform.forward * speed;

        Vector3 currentVelocity = rigidbody.velocity;
        //(���̉����x - �O�̉����x)/ ����
        Vector3 acceleration = (currentVelocity - previousVelocity) / Time.fixedDeltaTime;
        previousVelocity = currentVelocity;


        //�����x�̑傫��          1G=9.81 m/s2�Ŋ����Ă�
        float gForce = acceleration.magnitude / oneG;


        //Gforce��maxAcceleration�����Ă��� ����hissatsu��false�̂Ƃ� return �����Ȃ���
        if (gForce > maxAcceleration && !hissatsu) return;

        Vector3 diff = target.position - transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(diff);


        // ���ʐ��`��Ԃ��g���ĉ�]�����X�Ƀ^�[�Q�b�g�Ɍ�����
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, lerpT);


    }



    //private void PoolReurn()
    //{

    //    rigidbody.velocity = Vector3.zero;
    //    rigidbody.angularVelocity = Vector3.zero;  //�I�u�W�F�N�g��false�ɂ��钼�O�܂ł����ɕt����������
    //    transform.rotation = new Quaternion(0, 0, 0, 0);
    //    objectPool.Release(this);

    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            print("�G�ƏՓ�");
            Destroy(this.gameObject);
        }
    }


    private void OnEnable()
    {
        OFFtimeValue = timer;
        OFFtimeRandomValue = randomTimer;   //�I���ɂȂ�����^�C�}�[�̒l��������
    }


}
