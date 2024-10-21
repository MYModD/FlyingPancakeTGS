// ---------------------------------------------------------
// TestUI.cs
//
// 作成日:
// 作成者:
// ---------------------------------------------------------
using UnityEngine;
using System.Collections;
public class TestUI : MonoBehaviour
{
    #region 変数
    [SerializeField] private GameObject _ui;
#endregion
#region プロパティ
#endregion
#region メソッド

/// <summary>
/// 更新処理
/// </summary>
void Update ()
{
        //動画撮影用コード
        if (Input.GetKeyDown(KeyCode.T)) {
                _ui.SetActive(!_ui.activeSelf);
        }
}
#endregion
}