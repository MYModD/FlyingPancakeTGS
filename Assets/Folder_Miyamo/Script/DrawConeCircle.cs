using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawConeCircle : MonoBehaviour {
    public Camera _mainCamera;
    [SerializeField, Header("CameraManger")]
    private TestLockOnManager _testLockOnManager;
    public bool _debugIsON = false;
    private RectTransform _rectTransform;

    [SerializeField]
    private Text _circleCenterPositionText;

    void Start() {
        _rectTransform = GetComponent<RectTransform>();
        if (_rectTransform == null) {
            Debug.LogError("RectTransformが見つかりません。");
        }

        if (_circleCenterPositionText == null) {
            Debug.LogError("Circle Center Position Textが割り当てられていません。");
        }
    }

    private void LateUpdate() {
        UpdateCirclePosition();
    }

    private void UpdateCirclePosition() {
        // ワールド座標からスクリーン座標への変換
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(_mainCamera, _testLockOnManager._circleCenterPostion);

        // スクリーン座標からUI座標への変換
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _rectTransform.parent as RectTransform,
            screenPoint,
            null,
            out Vector2 localPoint
        );

        // UI要素の位置を設定
        _rectTransform.anchoredPosition = localPoint;

        // UI要素のサイズを設定
        float scale = _testLockOnManager._coneAngle * 0.1339f;
        _rectTransform.sizeDelta = new Vector2(100 * scale, 100 * scale); // 基本サイズを100とした場合
    }

    

    private void OnDrawGizmos() {
        if (!_debugIsON) {
            UpdateCirclePosition();
        }
    }
}