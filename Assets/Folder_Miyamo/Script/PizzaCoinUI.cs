using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PizzaCoinUI : MonoBehaviour, IPooledObject<PizzaCoinUI> {
    [Tag]
    public string _pizzaCoinUI;

    public PizzaCoinUICounter _pizzaCoinUICounter;

    public RectTransform _targetTransform; // UIのターゲット位置
    public float _attractionSpeed = 5f;
    public IObjectPool<PizzaCoinUI> ObjectPool {
        get; set;
    }

    public RectTransform _rectTransform;


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
