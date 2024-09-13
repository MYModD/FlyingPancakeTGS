using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePizzaMissileToPlayer : MonoBehaviour {
    [Header("ミサイル設定")]
    [SerializeField]
    private PizzaMissile _pizzaMissileToPlayer;

    [Header("ターゲット")]
    [SerializeField]
    private Transform _player;

    [Header("発射位置")]
    [SerializeField]
    private Transform[] _firePostion;
    [SerializeField]
    private Transform _pearentPostion;

    [Header("エフェクト")]
    [SerializeField]
    private ExplosionPoolManager _explosionPoolManager;

    [Header("発射間隔")]
    [SerializeField]
    private float _fireInterval = 1.0f; // 発射間隔 (秒)

    private float _timer = 0.0f;

    void Update() {
        _timer += Time.deltaTime;
        if (_timer >= _fireInterval) {
            _timer = 0.0f;

            // 発射するミサイルの数
            int numToFire = Random.Range(1, _firePostion.Length + 1);

            // 選択済みのインデックスを管理するリスト
            List<int> usedIndexes = new List<int>();

            for (int i = 0; i < numToFire; i++) {
                // 未選択のインデックスをランダムに選択
                int randomIndex;
                do {
                    randomIndex = Random.Range(0, _firePostion.Length);
                } while (usedIndexes.Contains(randomIndex));

                usedIndexes.Add(randomIndex);

                // ミサイルを生成
                PizzaMissile missile = Instantiate(_pizzaMissileToPlayer, _pearentPostion);
                missile._player = _player;
                missile._explosionPool = _explosionPoolManager;
                missile.gameObject.transform.SetPositionAndRotation(_firePostion[randomIndex].position, _firePostion[randomIndex].rotation);
                Debug.Log($"発射位置は : {_firePostion[randomIndex]}");
            }

            // 次のループのためにリストをクリア
            usedIndexes.Clear();
        }
    }
}
