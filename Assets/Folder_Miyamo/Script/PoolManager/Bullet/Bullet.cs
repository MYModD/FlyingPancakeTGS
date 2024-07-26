using NaughtyAttributes;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour, IPooledObject<Bullet>
{
    #region �ϐ� + �v���p�e�B

    [SerializeField, Header("���Ŏ���")]
    private float _timer = 1;
    [SerializeField, Header("�d�́A�����₷��")]
    private float _gravity = 1000f;
    [SerializeField, Tag]
    private string _enemyTag;

    private  float _offtimeValue;        // ���Ԍv�Z�p
    private Rigidbody _rigidbody;


    public IObjectPool<Bullet> ObjectPool {get;set;
    }

    #endregion

    #region ���\�b�h


    private void Awake() {

        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Initialize() {

        _offtimeValue = _timer;

    }

    public void ReturnToPool() {
        ObjectPool.Release(this);
    }


    private void FixedUpdate() {

        //�d�͂̏���
        _rigidbody.AddForce(new Vector3(0, -1 * _gravity, 0), ForceMode.Acceleration);


        //�^�C�}�[�̏��� MAX�Ŕ�r����0��MAX�̂Ƃ�ReturnPool()
        _offtimeValue = Mathf.Max(0, _offtimeValue - Time.fixedDeltaTime);
        if (_offtimeValue == 0) {

            ReturnToPool();
        }

    }

    private void OnTriggerEnter(Collider other) {

        print("�ʂ��Ȃɂ��ɂ���������");

        // �����ɃG�t�F�N�g������

        //�G�l�~�[�^�O�ɂ��������Ƃ�SetActive��False
        if (other.CompareTag(_enemyTag)) {

            other.gameObject.SetActive(false); //�Ƃ肠����false
            ReturnToPool();

        }
        

    }

    #endregion

}
