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

    public PizzaCoinUICounter _pizzaCoinUICounter;

    public RectTransform _debugTransform;

    private int _instanceCount = 0;

    protected override PizzaCoinUI Create() {
        PizzaCoinUI instance = Instantiate(_pooledPrefab, transform.position, Quaternion.identity, transform);
        instance.ObjectPool = _objectPool;
        instance._pizzaCoinUICounter = _pizzaCoinUICounter;

        _instanceCount++;

        float randomWidth = Random.Range(_minWidthRandom, _maxWidthRandom);
        float randomHeight = Random.Range(_minScaleYRandom, _maxScaleYRandom);
        Vector2 uiScale = new Vector2(100 - randomWidth, 100 - randomHeight);


        RectTransform rectTransform = instance.GetComponent<RectTransform>();
        if (rectTransform != null) {
            rectTransform.sizeDelta = uiScale;
        } else {
            Debug.LogWarning("RectTransform component not found on the instantiated PizzaCoinUI.");
        }

        return instance;
    }

    public void CoinStart(RectTransform rectTransform) {

        PizzaCoinUI hoge = _objectPool.Get();
        hoge._targetTransform = _debugTransform;




    }


    private void Update() {


        if (Input.GetKey(KeyCode.Space)){

            CoinStart(_debugTransform);
        }
    }
}