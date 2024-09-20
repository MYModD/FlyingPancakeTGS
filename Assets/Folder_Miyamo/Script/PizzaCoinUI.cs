using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PizzaCoinUI : MonoBehaviour, IPooledObject<PizzaCoinUI> {

    [Header("UIに接続するスクリプト")]
    public PizzaCoinUICounter _pizzaCoinUICounter;

    [Header("UIの位置")]
    public RectTransform _targetTransform; // UIのターゲット位置

    [Header("移動スピード")]
    public float _attractionSpeed = 1000f;



    [Tag]
    public string _pizzaCoinUI;
    public IObjectPool<PizzaCoinUI> ObjectPool {
        get; set;
    }

    public RectTransform _rectTransform;



    public void Hoge() {

        Debug.Log(ObjectPool);
    }
    private void Awake() {
        _rectTransform = GetComponent<RectTransform>();
      
    }
    public void Initialize() {
        if (_rectTransform == null) {
            _rectTransform = GetComponent<RectTransform>();
        }

        if (_targetTransform == null) {
            Debug.LogError("TargetTransformが設定されていません");
        }


    }

    public void ReturnToPool() {
        ObjectPool.Release(this);
    }

    // Update is called once per frame
    void FixedUpdate() {


        if (_rectTransform == null) {
            Debug.LogError("入ってないよ！！");
            return;
        }

        // UIの位置をターゲットに向かって移動
        Vector2 direction = (_targetTransform.position - _rectTransform.position).normalized;
        _rectTransform.position += (Vector3)direction * _attractionSpeed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag(_pizzaCoinUI)) {
            _pizzaCoinUICounter.TouchMyField();
            ReturnToPool();
        }
    }
}
