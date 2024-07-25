using NaughtyAttributes;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour, IPooledObject<Bullet>
{

    [SerializeField, Header("���Ŏ���")]
    private float _timer = default;
    [SerializeField, Header("�d��")]
    private float _gravity = default;
    [SerializeField, Tag]
    private string _enemyTag;

    private  float _offtimeValue;        // ���Ԍv�Z�p
    private Rigidbody _rigidbody;


    public IObjectPool<Bullet> ObjectPool {get;set;
    }

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


        _rigidbody.AddForce(new Vector3(0, -1 * _gravity, 0), ForceMode.Acceleration);

        _offtimeValue = Mathf.Max(0, _offtimeValue - Time.fixedDeltaTime);
        if (_offtimeValue == 0) {

            ReturnToPool();
        }

    }

    private void OnTriggerEnter(Collider other) {

        print("����������");
        // �����ɃG�t�F�N�g������
        if (other.CompareTag(_enemyTag)) {

            other.gameObject.SetActive(false); //�Ƃ肠����false
            ReturnToPool();

        }
        

    }
}
