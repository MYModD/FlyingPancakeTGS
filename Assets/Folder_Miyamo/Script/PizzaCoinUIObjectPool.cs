using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

public class PizzaCoinUIObjectPool : PoolManager<PizzaCoinUI> {

    [Header("�J���� Main")]
    private Camera _cameraMain;

    [Header("���b�N�I���^�[�Q�b�g�̈ʒu")]
    [SerializeField]
    private RectTransform _rockOnRectTransform;


    [Header("pizza�e�L�X�g������ꏊ")]
    public RectTransform _targetTransform;

    [Header("�����ʒu�̃����_����")]
    [Range(0, 500f)]
    [SerializeField]
    private float _instancePostionRandom;



    [Header("Width�̃����_����")]
    [SerializeField, Range(0, 50f)]
    private float _minWidthRandom = 0f;

    [SerializeField, Range(0, 50f)]
    private float _maxWidthRandom = 50f;

    [Header("Hight�̃����_����")]
    [SerializeField, Range(0, 50f)]
    private float _minScaleYRandom = 0f;

    [SerializeField, Range(0, 50f)]
    private float _maxScaleYRandom = 50f;

    public PizzaCoinUICounter _pizzaCoinUICounter;



    [Header("�f�o�b�N�p")]
    public Transform _debugPlayer;

    // ObjectPool �̏�����
    protected override IObjectPool<PizzaCoinUI> ObjectPool {
        get; set;
    }



    // �v�[���̃Z�b�g�A�b�v


    protected override PizzaCoinUI Create() {
        PizzaCoinUI instance = Instantiate(_pooledPrefab, transform.position, Quaternion.identity, transform);
        instance.ObjectPool = ObjectPool; // _objectPool ��ݒ�
        instance._pizzaCoinUICounter = _pizzaCoinUICounter;
        instance._targetTransform = _targetTransform;

        // �����_���ȃT�C�Y�̐ݒ�
        float randomWidth = Random.Range(_minWidthRandom, _maxWidthRandom);
        float randomHeight = Random.Range(_minScaleYRandom, _maxScaleYRandom);
        Vector2 uiScale = new Vector2(100 - randomWidth, 100 - randomHeight);

        RectTransform rectTransform = instance.GetComponent<RectTransform>();
        if (rectTransform != null) {
            rectTransform.sizeDelta = uiScale;
        } else {
            Debug.LogWarning("RectTransform component not found on the instantiated PizzaCoinUI.");
        }




        instance.GetComponent<Image>().color = Color.HSVToRGB(Random.Range(0, 0.5f), Random.Range(0, 0.3f), 1f);

        return instance;
    }



    public void CoinStart() {

        PizzaCoinUI instance = ObjectPool.Get();

        Vector2 randmPostion = Random.insideUnitCircle * _instancePostionRandom;
        instance.transform.position = _rockOnRectTransform.position - new Vector3(randmPostion.x, randmPostion.y, 0);
    }


    public void ExplosionPizzaCoin() {





    }
    private void Update() {
        if (Input.GetKey(KeyCode.Space)) {

            CoinStart();
        }


    }

}
