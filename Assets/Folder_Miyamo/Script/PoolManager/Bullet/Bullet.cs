using NaughtyAttributes;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour, IPooledObject<Bullet>
{

    [SerializeField, Header("消滅時間")]
    private float _timer = default;
    [SerializeField, Header("重力")]
    private float _gravity = default;
    [SerializeField, Tag]
    private string _enemyTag;

    private  float _offtimeValue;        // 時間計算用
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

        print("あたっちょ");
        // ここにエフェクトを入れる
        if (other.CompareTag(_enemyTag)) {

            other.gameObject.SetActive(false); //とりあえずfalse
            ReturnToPool();

        }
        

    }
}
