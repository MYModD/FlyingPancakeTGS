using UnityEngine;
using UnityEngine.UI;

public class ObjectImagePositioner : MonoBehaviour {
    // オブジェクトに関連付けるImage
    [SerializeField, Header("配置するImage")]
    private Image _targetImage;

    // 画像を表示したいオブジェクトのTransform
    [SerializeField, Header("表示対象オブジェクトのTransform")]
    private Transform _objectTransform;

    // CanvasのRectTransform
    [SerializeField, Header("CanvasのRectTransform")]
    private RectTransform _canvasRectTransform;

    // カメラを取得（主にメインカメラを想定）
    [SerializeField, Header("対象カメラ")]
    private Camera _mainCamera;

    void Start() {
        SetImageToObjectPosition();
    }

    void Update() {
        // オブジェクトの位置に合わせてImageを常に更新
        SetImageToObjectPosition();
    }

    // オブジェクトの位置にImageを配置するメソッド
    private void SetImageToObjectPosition() {
        if (_targetImage == null || _objectTransform == null || _canvasRectTransform == null || _mainCamera == null) {
            Debug.LogWarning("設定が正しくありません！");
            return;
        }

        // オブジェクトのワールド座標をスクリーン座標に変換
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(_mainCamera, _objectTransform.position);

        // スクリーン座標をキャンバスのローカル座標に変換
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvasRectTransform, screenPoint, _mainCamera, out Vector2 localPoint);

        // Imageの位置を設定（キャンバス上の位置）
        _targetImage.rectTransform.localPosition = localPoint;
    }
}
