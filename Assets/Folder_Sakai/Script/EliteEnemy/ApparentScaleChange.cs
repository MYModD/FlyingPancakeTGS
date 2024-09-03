using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApparentScaleChange : MonoBehaviour {
    // カメラからの距離が1のときのスケール値
    private Vector3 _baseScale;

    [SerializeField] private GameObject _camera;
    [SerializeField] private float _minScale = 0.5f;  // スケールA
    [SerializeField] private float _maxScale = 2.0f;  // スケールB

    private void OnEnable() {
        // カメラからの距離が1のときのスケール値を算出
        _baseScale = transform.localScale / GetDistance();
    }

    void Update() {
        // 現在のスケールを計算
        float distance = GetDistance();
        Vector3 newScale = _baseScale * distance;

        // スケールが指定範囲に収まるように制限
        float clampedScaleX = Mathf.Clamp(newScale.x, _minScale, _maxScale);
        float clampedScaleY = Mathf.Clamp(newScale.y, _minScale, _maxScale);
        float clampedScaleZ = Mathf.Clamp(newScale.z, _minScale, _maxScale);

        transform.localScale = new Vector3(clampedScaleX, clampedScaleY, clampedScaleZ);
    }

    // カメラからの距離を取得
    float GetDistance() {
        return (transform.position - _camera.transform.position).magnitude;
    }
}
