using UnityEngine;

public class CursorController : MonoBehaviour {
    // 現在のカーソル表示状態を保持するフィールド変数
    private bool _isCursorVisible = false;

    // Startメソッド内でメソッドを呼び出す
    private void Start() {
        HideCursor();
    }

    // Updateメソッドで毎フレームESCキーの入力をチェック
    private void Update() {
        // ESCキーが押されたらカーソルの表示状態をトグル（切り替え）
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (_isCursorVisible) {
                HideCursor();
            } else {
                ShowCursor();
            }
        }
    }

    // マウスカーソルを非表示にするメソッド
    private void HideCursor() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _isCursorVisible = false;
    }

    // マウスカーソルを表示するメソッド
    private void ShowCursor() {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        _isCursorVisible = true;
    }
}
