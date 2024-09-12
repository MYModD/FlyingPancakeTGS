using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MTAppearanceManagement : MonoBehaviour {
    // スポーンポイント
    [SerializeField] private Transform[] _spawnPoint;
    // スポーンするかの判定 (trueでスポーン可能、falseでスポーン済み)
    [SerializeField] private bool[] _spawnJudge;
    // 生成するモンスタートラックのプレハブ（複数のオブジェクト）
    [SerializeField] private GameObject[] _monsterTruck;
    [SerializeField] private ExplosionPoolManager _explosionPoolManager;

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
            // _spawnJudge が false なら
            if (!_spawnJudge[i]) {

                // _spawnJudge を true に変更
                _spawnJudge[i] = true;

                _explosionPoolManager.StartExplosion(_spawnPoint[i].transform);
                // 対応するモンスタートラックを非アクティブ化
                _monsterTruck[i].SetActive(false);

                // リダクションした数を増加
                reducedCount++;

                // 指定された数に達したら終了
                if (reducedCount >= numberOfTimesToReduce) {
                    break;
                }
            }
        }
    }
}
