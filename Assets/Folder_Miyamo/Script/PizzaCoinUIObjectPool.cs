using UnityEngine;

public class PizzaCoinUIObjectPool : PoolManager<PizzaCoinUI> {
    [SerializeField, Range(0, 50f)]
    private float _minWidthRandom = 0f;

    [SerializeField, Range(0, 50f)]
    private float _maxWidthRandom = 50f;

    [SerializeField, Range(0, 50f)]
    private float _minScaleYRandom = 0f;

    [SerializeField, Range(0, 50f)]
    private float _maxScaleYRandom = 50f;

    private int _instanceCount = 0;

    protected override PizzaCoinUI Create() {
        PizzaCoinUI instance = Instantiate(_pooledPrefab, transform.position, Quaternion.identity, transform);
        instance.ObjectPool = _objectPool;

        _instanceCount++;

        float randomWidth = Random.Range(_minWidthRandom, _maxWidthRandom);
        float randomHeight = Random.Range(_minScaleYRandom, _maxScaleYRandom);
        Vector2 uiScale = new Vector2(100 - randomWidth, 100 - randomHeight);

        Debug.Log($"Instance {_instanceCount}: UI Scale = {uiScale}");

        RectTransform rectTransform = instance.GetComponent<RectTransform>();
        if (rectTransform != null) {
            rectTransform.sizeDelta = uiScale;
        } else {
            Debug.LogWarning("RectTransform component not found on the instantiated PizzaCoinUI.");
        }

        return instance;
    }
}