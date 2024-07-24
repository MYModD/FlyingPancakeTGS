using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApparentScaleChange : MonoBehaviour
{
    // カメラからの距離が1のときのスケール値
    Vector3 _baseScale;

    void Start() {
        // カメラからの距離が1のときのスケール値を算出
        this._baseScale = this.transform.localScale / this.GetDistance();
    }

    void Update() {
        this.transform.localScale = this._baseScale * this.GetDistance();
    }

    // カメラからの距離を取得
    float GetDistance() {
        return (this.transform.position - Camera.main.transform.position).magnitude;
    }
}
