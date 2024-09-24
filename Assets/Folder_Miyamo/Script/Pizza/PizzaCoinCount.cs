using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System;
using Unity.Mathematics;

public class PizzaCoinCount : MonoBehaviour {
    [Header("タグ設定")]
    [SerializeField, Tag]
    private string _pizzaTag;

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
    public TextMeshProUGUI _text;

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

    [Header("Player(MovingObj)")]
    public GameObject _playerMovingObj;

    public PizzaCoinUIObjectPool _pizzaUI;


    public RedDamageEffect _redDamage;


    public TestLockOnManager _lockOn;

    public PizzaCoinUICounter _pizzaCoinUICounter;


    public ParticleSystem _pizzaEffect;


    private ControllerBuruBuru _controller;



    void Start() {
        _lastDecreaseTime = Time.time;
    }
    private void OnEnable() {
        _text.text = ""+0;
    }
    private void OnTriggerEnter(Collider other) {
        Debug.Log($"ぶつかったやつ : {other.gameObject.name}");


        
        if (other.CompareTag(_pizzaTag)) {


            // ピザコイン取得時の処理

            _pizzaUI.CoinStart();

            
            

            float pitchRandom = UnityEngine.Random.Range(-0.05f, 0.05f);
            Debug.Log($"ランダム値 : {pitchRandom}");

            _audioPizza.pitch = 1f -pitchRandom;
            _audioPizza.Play();
            other.gameObject.SetActive(false);


           
            
        }
        if (other.CompareTag(_enemyTag)) {
            // ここにミサイルが当たったとき減らす

            //_pizzaCount = Mathf.Max(0, _pizzaCount - _decreaseAmount);

            _pizzaCoinUICounter.DegreePizzaCoin();


            // ここ要注意
            if (_controller == null) {
                _controller = ControllerBuruBuru.Instance;
            }

            _pizzaEffect.Play();
            _controller.StartVibration();
            _redDamage.PlayerDamage();
            _lockOn.AddBlackList(other.transform);
            other.gameObject.SetActive(false);

        }
    }


   

    private void OnTriggerStay(Collider other) {

        /*
        if (other.CompareTag(_pizzaLeftArmTag)) {
            if (Time.time - _lastDecreaseTime >= _decreaseInterval) {





            }
        }*/
    }

    public void DegreePizzaCoinLeftArm() {


        //_pizzaCount = Mathf.Max(0, _pizzaCount - _decreaseAmount);
        //UpdatePizzaCountText();

        if (_controller == null) {
            _controller = ControllerBuruBuru.Instance;
        }
        _controller.StartVibration();
        _pizzaEffect.Play();

        _pizzaCoinUICounter.DegreePizzaCoin();
        _redDamage.PlayerDamage();

    }







    private void UpdatePizzaCountText() {
        _text.text = _pizzaCount.ToString();
    }
}