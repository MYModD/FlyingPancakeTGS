// ---------------------------------------------------------
// TestButton.cs
//
// 作成日:
// 作成者:
// ---------------------------------------------------------
using UnityEngine;
using System.Collections;
public class TestButton : MonoBehaviour
{
    #region 変数
    [SerializeField] private CanvasManager _canvas;
#endregion
#region プロパティ
#endregion
#region メソッド
/// <summary>
/// 初期化処理 使わないなら消す
/// </summary>
void Awake()
{
}
/// <summary>
/// 更新前処理
/// </summary>
void Start ()
{
}
/// <summary>
/// 更新処理
/// </summary>
void Update ()
{
}
    public void OnClickSw() {
        _canvas.OnClickSw(this.gameObject.tag);
    }
    #endregion
}