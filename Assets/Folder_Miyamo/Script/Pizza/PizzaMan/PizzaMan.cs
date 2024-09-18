using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

public class PizzaMan : MonoBehaviour {
    public TimeLimit _timeLimit;
    public ExplosionPoolManager _explosionPoolManager;
    public float _endToNextGameDuration;
    public float _explosionScale = 100f;
    [SerializeField]
    [Tag]
    private string _missileTag;

    [Header("PizzaMan本体左右を見えないようにする")]
    public GameObject[] _childObject;
    private bool _isProcessing = false;
    private float _lastTriggerTime = 0f;
    private const float TRIGGER_COOLDOWN = 0.1f; // 100ミリ秒のクールダウン



    private void OnTriggerEnter(Collider other) {

        Debug.Log($"{other.gameObject.name}にあたったよ！！");
        if (other.CompareTag(_missileTag) && !_isProcessing && Time.time - _lastTriggerTime > TRIGGER_COOLDOWN) {
            _lastTriggerTime = Time.time;
            ProcessCollisionAsync().Forget();
        }
    }

    private async UniTaskVoid ProcessCollisionAsync() {
        if (_isProcessing) {
            return;
        }

        _isProcessing = true;

        _explosionPoolManager.StartExplosionScale(this.transform, _explosionScale);

        foreach (GameObject item in _childObject) {

            item.SetActive(false);
        }


        await UniTask.Delay(TimeSpan.FromSeconds(_endToNextGameDuration), cancellationToken: this.GetCancellationTokenOnDestroy());

        _timeLimit.End3rdGame();
        _isProcessing = false;
    }
}