using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyBullet : MonoBehaviour, IPooledObject<EnemyBullet> {
    [SerializeField, Header("�e��")] private float _speed = 500f;  // �e�̑��x��ݒ肷��t�B�[���h
    [SerializeField, Header("��s����")]
    private float _timer = 10f;

    private float _offtimeValue;
    private Rigidbody _rigidbody;

    public IObjectPool<EnemyBullet> ObjectPool {
        get; set;
    }

    /// <summary>
    /// ������
    /// </summary>
    public void Initialize() {
        _offtimeValue = _timer;
    }

    public void ReturnToPool() {
        ObjectPool.Release(this);
    }

    void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        
        // �^�C�}�[ offtimeValue��0�ɂȂ�����v�[���ɕԂ�
        _offtimeValue = Mathf.Max(0, _offtimeValue - Time.fixedDeltaTime);
        if (_offtimeValue == 0) {

            ReturnToPool();
        }

        MoveForward();
    }

    private void MoveForward() {
        //// �O�����ɐi��
        //transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        //�O�i����
        _rigidbody.velocity = transform.forward * _speed;
    }

    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag("Player")) {
            print("�v���C���[�ɏՓ�");
        }
    }
}
