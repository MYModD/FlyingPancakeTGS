using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaCoinInstance : MonoBehaviour {
    // 配置位置の配列
    public Transform _instantiateLeftPosition;
    public Transform _instantiateRightPosition;
    // コインオブジェクトの配列
    [Header("短いやつと長いやつある")]
    public GameObject[] _leftCoins;
    [Header("短いやつしかないよ")]
    public GameObject _rightCoin;


    public Transform _instancePearent;
    // コイン生成間隔
    public float _leftCoinDuration;
    public float _rightCoinDuration;
    [Header("ラミエルから見て左が出る確率  だいたい70")]
    public float _leftProbability = 70;
    // タイマー
    private float _leftTimer;
    private float _rightTimer;

    private void OnEnable() {
        _leftTimer = _leftCoinDuration;
        _rightTimer = _rightCoinDuration;
    }

    void Update() {
        _leftTimer -= Time.deltaTime;
        _rightTimer -= Time.deltaTime;

        // 左のコイン生成
        if (_leftTimer <= 0) {
            if (Random.Range(0, 100) < _leftProbability) {
                GameObject obj = Instantiate(_leftCoins[Random.Range(0, _leftCoins.Length)], _instancePearent);
                obj.transform.position = _instantiateLeftPosition.position;
            }
            _leftTimer = _leftCoinDuration;
        }

        // 右のコイン生成
        if (_rightTimer <= 0) {
            GameObject obj = Instantiate(_rightCoin, _instancePearent);
            obj.transform.position = _instantiateRightPosition.position;
            _rightTimer = _rightCoinDuration;
        }
    }
}