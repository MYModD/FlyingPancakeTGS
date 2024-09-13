using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeR : MonoBehaviour {
    // 回転させたい特定のオブジェクト
    [SerializeField] private GameObject _targetObject;

    // Y軸の回転角度（度数）
    [SerializeField] private float _rNumber;

    private void OnEnable() {
        if (_targetObject != null) {
            // 特定のオブジェクトの現在の回転角度を取得
            Vector3 currentRotation = _targetObject.transform.eulerAngles;

            // Y軸の回転角度を設定
            currentRotation.y = _rNumber;

            // 新しい回転をオブジェクトに適用
            _targetObject.transform.eulerAngles = currentRotation;
        } else {
            Debug.LogWarning("ターゲットオブジェクトが指定されていません。");
        }
    }
}
