using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Splines;

public class PizzaCoinCount : MonoBehaviour {
    [Header("タグ設定")]
    [SerializeField, Tag]
    private string _pizzaTag;
    [SerializeField, Tag]
    private string _pizzaLeftArmTag;
    [SerializeField, Tag]
    private string _enemyTag;
    [SerializeField, Tag]
    public string _pizzaManTagEnemy;

    [Header("ピザコイン設定")]
    [SerializeField]
    private int _pizzaCount = 0;
    [SerializeField]
    [Header("次のステージに進むために必要なコイン数")]
    private float _maxPizzaCoin;

    [Header("UI設定")]
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private TextMeshProUGUI _title;

    [SerializeField]
    private SplineAnimate _anime;


    [Header("一定時間ごとに減少するその時間")]
    [SerializeField, Range(0, 3f)]
    public float _decreaseInterval = 1f; // 減少する間隔（秒）
    [SerializeField]
    [Header("一度に減少する量")]
    public int _decreaseAmount = 1;

    public AudioSource _audioPizza;

    private float _lastDecreaseTime;

    [Header("プレイヤー参照")]
    public PizzaMan _pizzaMan;

    void Start() {
        _lastDecreaseTime = Time.time;
    }
    private void Update() {
        if (_anime.enabled == true) {

        }
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log($"ぶつかったやつ : {other.gameObject.name}");
        if (other.CompareTag(_pizzaTag)) {
            _pizzaCount++;
            _audioPizza.Play();
            other.gameObject.SetActive(false);
            UpdatePizzaCountText();
            //一定数達したらピザマンのタグが敵に変わるスクリプト
            if (_pizzaCount >= _maxPizzaCoin) {
                _pizzaMan.tag = _pizzaManTagEnemy;
            }
        }
        if (other.CompareTag(_enemyTag)) {
            // ここにミサイルが当たったとき減らす
            _pizzaCount = Mathf.Max(0, _pizzaCount - _decreaseAmount);
            other.gameObject.SetActive(false);
        }
    }


    private void OnTriggerStay(Collider other) {
        if (other.CompareTag(_pizzaLeftArmTag)) {
            if (Time.time - _lastDecreaseTime >= _decreaseInterval) {
                _pizzaCount = Mathf.Max(0, _pizzaCount - _decreaseAmount);
                _lastDecreaseTime = Time.time;
                UpdatePizzaCountText();

            }
        }
    }

    private void UpdatePizzaCountText() {
        _text.text = _pizzaCount.ToString();
        _title.text = "PizzaCoin";
    }
}