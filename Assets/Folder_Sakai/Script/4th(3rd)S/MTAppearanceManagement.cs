using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MTAppearanceManagement : MonoBehaviour {
    // スポーンポイント
    [SerializeField] private Transform[] _spawnPoint;
    // スポーンするかの判定 (trueでスポーン可能、falseでスポーン済み)
    [SerializeField] private bool[] _spawnJudge;
    // 生成するモンスタートラックのプレハブ（複数のオブジェクト）
    [SerializeField] private GameObject[] _monsterTruck;
    [SerializeField] private ExplosionPoolManager _explosionPoolManager;

    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField]
    private AudienceGaugeManager _audienceGaugeManager;

    [SerializeField]
    private TextMeshProUGUI _text;
    [SerializeField]
    private TextMeshProUGUI _title;
    private void Update() {
        NumberOfUnitsCounted();
    }
    // スポーンを管理するメソッド
    public void MTSpawn(int numberOfGeneration) {
        print(numberOfGeneration);
        for (int i = 0; i < numberOfGeneration; i++) {
            for (int j = 0; j < _spawnJudge.Length; j++) {
                if (_spawnJudge[j]) {
                    _monsterTruck[j].transform.position = _spawnPoint[j].position;
                    _monsterTruck[j].transform.rotation = _spawnPoint[j].rotation;
                    _monsterTruck[j].SetActive(true);
                    _spawnJudge[j] = false;
                    break;
                }
            }
        }
    }

    // 引数を使用したモンスタートラック生成管理
    public void MultiplicationMTSpawn(int numberOfGeneration) {
        int activeCount = 0;

        // _spawnJudge の中で false（アクティブなオブジェクト）の数を数える
        foreach (bool judge in _spawnJudge) {
            if (!judge) {
                activeCount++;
            }
        }

        // 引数とアクティブオブジェクトの数を掛けたもの
        int multiplicationResult = numberOfGeneration * activeCount;

        // 掛けた結果から、再度アクティブなオブジェクトの数を引いたもの
        int finalResult = multiplicationResult - activeCount;

        Debug.Log("掛けた結果: " + multiplicationResult + ", 引いた後の最終結果: " + finalResult);

        MTSpawn(finalResult);
    }

    // DivisionMTReduce メソッド
    public void DivisionMTReduce(int numberOfReduce) {
        // _spawnJudge の中で false（アクティブなオブジェクト）の数を数える
        int activeCount = 0;
        foreach (bool judge in _spawnJudge) {
            if (!judge) {
                activeCount++;
            }
        }

        // アクティブなオブジェクトの数を引数 numberOfReduce で割る (変数A)
        if (numberOfReduce != 0) { // 0で割ることを防ぐためのチェック
            int variableA = (int)activeCount / numberOfReduce;
            Debug.Log("変数A: " + variableA);
          
            int finalResult = activeCount - variableA;

            MTReduce(finalResult);

            Debug.Log("最終結果: " + finalResult);
        } else {
            Debug.LogWarning("numberOfReduce が 0 です。0 で割ることはできません。");
        }
    }

    // 障害物に接触したMTの場所を生成可にするメソッド
    public void MTTriggerObstacles(int index) {
        _spawnJudge[index] = true;
    }

    // 下から順に _spawnJudge の false を true にして、対応するモンスタートラックを非アクティブ化
    public void MTReduce(int numberOfReduce) {
        int numberOfTimesToReduce = Mathf.Abs(numberOfReduce);
        int reducedCount = 0;

        // 配列の下（大きいインデックス）からループする
        for (int i = _spawnJudge.Length - 1; i >= 0; i--) {
            if (!_spawnJudge[i]) {
                _spawnJudge[i] = true;
                _explosionPoolManager.StartExplosion(_spawnPoint[i].transform);
                _monsterTruck[i].SetActive(false);
                reducedCount++;

                if (reducedCount >= numberOfTimesToReduce) {
                    break;
                }
            }
        }
    }

    private void NumberOfUnitsCounted() {

        int activeCount = 0;

        // _spawnJudge の中で false（アクティブなオブジェクト）の数を数える
        foreach (bool judge in _spawnJudge) {
            if (!judge) {
                activeCount++;
                print(activeCount);
            }
        }
        _scoreManager.InputGetStarScore(activeCount, 30);
        _text.text = activeCount.ToString();
        _title.text = "MonstarCount";
        _audienceGaugeManager.SetScoreValue(activeCount, 30, "MonsterCount");
    }
}
