using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyBullet : MonoBehaviour, IPooledObject<EnemyBullet> {
    [SerializeField, Header("弾速")] private float _speed = 500f;  // 弾の速度を設定するフィールド
    [SerializeField, Header("飛行時間")]
    private float _timer = 10f;

    private float _offtimeValue;
    private Rigidbody _rigidbody;

    public IObjectPool<EnemyBullet> ObjectPool {
        get; set;
    }

    /// <summary>
    /// 初期化
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
        
        // タイマー offtimeValueが0になったらプールに返す
        _offtimeValue = Mathf.Max(0, _offtimeValue - Time.fixedDeltaTime);
        if (_offtimeValue == 0) {

            ReturnToPool();
        }

        MoveForward();
    }

    private void MoveForward() {
        //// 前方向に進む
        //transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        //前進する
        _rigidbody.velocity = transform.forward * _speed;
    }

    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag("Player")) {
            print("プレイヤーに衝突");
        }
    }
}
