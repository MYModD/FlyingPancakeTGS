// ---------------------------------------------------------
// TextCope.cs
//
// 作成日:
// 作成者:
// ---------------------------------------------------------
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
public class TextCope : MonoBehaviour
{
    #region 変数
    private Text _thisText;
    private TextMeshProUGUI _parentTextMeshPro;
    #endregion
    #region プロパティ
    #endregion
    #region メソッド

/// 更新前処理
/// </summary>
void Start ()
{
        _thisText = GetComponent<Text>();
        _parentTextMeshPro = transform.parent.GetComponent<TextMeshProUGUI>();
}
/// <summary>
/// 更新処理
/// </summary>
void Update ()
{
        //テキストメッシュプロの内容をテキストに反映
        _thisText.color = _parentTextMeshPro.color;
        if (_parentTextMeshPro.text==_thisText.text) {
            return;
        }
        _thisText.text = _parentTextMeshPro.text;
        _thisText.color = _parentTextMeshPro.color;
    }
    #endregion
}