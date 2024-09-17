using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System;  // UniTaskを使用するためのライブラリを追加

public class RedDamageEffect : MonoBehaviour {
    [SerializeField] private Image _redDamage;  // _redDamage Imageをシリアライズ
    public bool _isDamageActive = false;       // ダメージエフェクトがアクティブかどうか
    public float _duration;
    private void Start() {
        _redDamage.enabled = false;// 最初は非表示に設定
    }

    // ダメージ時に呼び出されるメソッド
    public void PlayerDamage() {
        if (_isDamageActive) {
        
            return;  // 既にエフェクトが表示されている場合は何もしない

        }

        // ダメージエフェクトを表示
        _redDamage.enabled = true;
        _isDamageActive = true;

        // 一定時間後にエフェクトを非表示にする
        HideRedDamageAsync().Forget();
    }

    /// <summary>
    /// 一定時間後にImageを非表示にする非同期処理
    /// </summary>
    /// <returns></returns>
    private async UniTaskVoid HideRedDamageAsync() {
        // 指定した時間（例: 2秒間）待機
        await UniTask.Delay(TimeSpan.FromSeconds(_duration));

        // エフェクトを非表示にし、処理が終わったことを示すフラグをリセット
        _redDamage.enabled = false;
        _isDamageActive = false;
    }
}
