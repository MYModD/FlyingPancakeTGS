// ---------------------------------------------------------
// TestButton.cs
//
// 作成日:
// 作成者:
// ---------------------------------------------------------
using UnityEngine;
using System.Collections;
public class TestButton : MonoBehaviour {
    #region 変数
    [SerializeField] private CanvasManager _canvas;
    #endregion
    #region プロパティ
    #endregion
    #region メソッド
    public void OnClickSw() {
        //押されたボタンのタグを渡す
        _canvas.OnClickSw(this.gameObject.tag);
    }
    #endregion
}