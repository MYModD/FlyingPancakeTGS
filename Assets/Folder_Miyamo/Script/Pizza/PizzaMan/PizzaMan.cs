using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks; // UniTaskの名前空間を追加
using System; // Systemの名前空間を追加

public class PizzaMan : MonoBehaviour {
    public TimeLimit _timeLimit;
    public ExplosionPoolManager _explosionPoolManager;
    public float _endToNextGameDuration;
    public float _explosionScale = 100f;
    [SerializeField]
    [Tag]
    private string _missileTag;

    

    private async void OnTriggerEnter(Collider other) {
        if (other.CompareTag(_missileTag)) {
            _explosionPoolManager.StartExplosionScale(this.transform, _explosionScale);

            // UniTaskを使用して遅延処理を実装
            await UniTask.Delay(TimeSpan.FromSeconds(_endToNextGameDuration), cancellationToken: this.GetCancellationTokenOnDestroy());

            _timeLimit.End3rdGame();
        }
    }
}