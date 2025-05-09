using NaughtyAttributes;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour, IPooledObject<Bullet>
{
    #region 変数 + プロパティ

    [SerializeField, Header("消滅するまでの時間")]
    private float _timer = 1;

    [SerializeField, Header("重力つける？")]
    private bool _isGravityON = false;

    [SerializeField, Header("重力、おちやすさ"),EnableIf(nameof(_isGravityON))]
    private float _gravity = 1000f;
    [SerializeField, Tag]
    private string _playerTag;

    private  float _offtimeValue;        // 時間計算用
    private Rigidbody _rigidbody;


    public IObjectPool<Bullet> ObjectPool {get;set;
    }

    #endregion

    #region メソッド


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

        //重力の処理
        if (_isGravityON) {

            _rigidbody.AddForce(new Vector3(0, -1 * _gravity, 0), ForceMode.Acceleration);
        }



        //タイマーの処理 MAXで比較して0がMAXのときReturnPool()
        _offtimeValue = Mathf.Max(0, _offtimeValue - Time.fixedDeltaTime);
        if (_offtimeValue == 0) {

            ReturnToPool();
        }

    }

    private void OnTriggerEnter(Collider other) {



        //エネミータグにあたったときSetActiveをFalse
        if (other.CompareTag(_playerTag)) {

            Debug.Log($"{other.name}あたったよぉ");
            ReturnToPool();

            // ここにBulletにあたったらのエフェクトを入れる
        }

    }

    #endregion

}
