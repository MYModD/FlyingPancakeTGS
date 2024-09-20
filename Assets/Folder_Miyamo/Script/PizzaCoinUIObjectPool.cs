using UnityEngine;
using UnityEngine.Pool;

public class PizzaCoinUIObjectPool : PoolManager<PizzaCoinUI> {

    [Header("カメラ Main")]
    private Camera _cameraMain;

    [Header("ロックオンターゲットの位置")]
    [SerializeField]
    private RectTransform _rockOnRectTransform;


    [Header("pizzaテキストがある場所")]
    public RectTransform _targetTransform;

    [Header("生成位置のランダムさ")]
    [Range(0,500f)]
    [SerializeField]
    private float _instancePostionRandom;



    [Header("Widthのランダムさ")]
    [SerializeField, Range(0, 50f)]
    private float _minWidthRandom = 0f;

    [SerializeField, Range(0, 50f)]
    private float _maxWidthRandom = 50f;

    [Header("Hightのランダムさ")]
    [SerializeField, Range(0, 50f)]
    private float _minScaleYRandom = 0f;

    [SerializeField, Range(0, 50f)]
    private float _maxScaleYRandom = 50f;

    public PizzaCoinUICounter _pizzaCoinUICounter;



    [Header("デバック用")]
    public Transform _debugPlayer;

    // ObjectPool の初期化
    protected override IObjectPool<PizzaCoinUI> ObjectPool {
        get; set;
    }

    

    // プールのセットアップ
    

    protected override PizzaCoinUI Create() {
        PizzaCoinUI instance = Instantiate(_pooledPrefab, transform.position, Quaternion.identity, transform);
        instance.ObjectPool = ObjectPool; // _objectPool を設定
        instance._pizzaCoinUICounter = _pizzaCoinUICounter;
        instance._targetTransform = _targetTransform;

        // ランダムなサイズの設定
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

   

    public void CoinStart(Transform playerTransform) {

        //Vector2 playerPostion = RectTransformUtility.WorldToScreenPoint(_cameraMain, playerTransform.position);

        //Debug.Log(playerPostion);

        PizzaCoinUI instance = ObjectPool.Get();

        Vector2 randmPostion = Random.insideUnitCircle * _instancePostionRandom;
        instance.transform.position = _rockOnRectTransform.position - new Vector3( randmPostion.x, randmPostion.y, 0);
    }

    private void Update() {
        if (Input.GetKey(KeyCode.Space)) {
            CoinStart(_debugPlayer);
        }
    }
}
