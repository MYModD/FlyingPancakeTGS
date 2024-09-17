// ---------------------------------------------------------
// GetCoolTime.cs
//
// 作成日:
// 作成者:
// ---------------------------------------------------------
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GetCooltime : MonoBehaviour
{
    #region 変数
    [SerializeField] private Slider _coolTimeSlider;
    [SerializeField]
    private GameController _gameController;
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
        _coolTimeSlider.value = _coolTimeSlider.maxValue - _gameController.CoolTime();
}
#endregion
}