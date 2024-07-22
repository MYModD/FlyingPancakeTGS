using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApparentScaleChange : MonoBehaviour {
    // カメラからの距離が1のときのスケール値
    Vector3 _baseScale;

    // スクリプトの動作を有効/無効にするフラグ
    [SerializeField] private bool _isActive = true;

    void Start() {
        // カメラからの距離が1のときのスケール値を算出
        this._baseScale = this.transform.localScale / this.GetDistance();
    }

    void Update() {
        // スクリプトが有効な場合のみスケールを更新
        if (_isActive) {
            this.transform.localScale = this._baseScale * this.GetDistance();
        }
    }

    // カメラからの距離を取得
    float GetDistance() {
        return (this.transform.position - Camera.main.transform.position).magnitude;
    }

    // スクリプトの動作を切り替えるメソッド
    public void ToggleActive() {
        _isActive = !_isActive;
    }
}
