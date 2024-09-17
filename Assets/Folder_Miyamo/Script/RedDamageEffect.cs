using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System;

public class RedDamageEffect : MonoBehaviour {
    [SerializeField] private Image _redDamage;
    public bool _isDamageActive = false;
    public float _duration = 2f;
    public float _fadeInDuration = 0.5f;
    public float _fadeOutDuration = 0.5f;

    private void Start() {
        SetAlpha(0);
    }

    public void PlayerDamage() {
        if (_isDamageActive) {
            return;
        }

        _isDamageActive = true;
        FadeInOutAsync().Forget();
    }

    private async UniTaskVoid FadeInOutAsync() {
        // フェードイン
        await FadeAsync(0, 1, _fadeInDuration);

        // 最大アルファ値で待機
        await UniTask.Delay(TimeSpan.FromSeconds(_duration - _fadeInDuration - _fadeOutDuration));

        // フェードアウト
        await FadeAsync(1, 0, _fadeOutDuration);

        _isDamageActive = false;
    }

    private async UniTask FadeAsync(float startAlpha, float endAlpha, float fadeDuration) {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration) {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / fadeDuration);
            float currentAlpha = Mathf.Lerp(startAlpha, endAlpha, t);
            SetAlpha(currentAlpha);
            await UniTask.Yield();
        }

        SetAlpha(endAlpha);
    }

    private void SetAlpha(float alpha) {
        Color color = _redDamage.color;
        color.a = alpha;
        _redDamage.color = color;
    }
}