// ---------------------------------------------------------
// ControllerSelectButton.cs
//
// 作成日:
// 作成者:
// ---------------------------------------------------------
using UnityEngine;
using System.Collections;
public class ControllerSelectButton : MonoBehaviour {
    #region 変数
    [SerializeField]
    private CanvasManager _canvasManager;
    private string _stateName;
    [SerializeField] private string _title;
    [SerializeField] private string _menu;
    [SerializeField] private string _setting; 
    #endregion
    #region プロパティ
    #endregion
    #region メソッド
    /// <summary>
    /// 初期化処理 使わないなら消す
    /// </summary>
    void Awake() {
    }
    /// <summary>
    /// 更新前処理
    /// </summary>
    void Start() {
    }
    /// <summary>
    /// 更新処理
    /// </summary>
    void Update() {
        GetStateName();
        if (_stateName == _title) {
            SelectButton();
        }
        if (_stateName==_menu) {

        }
        if (_stateName == _setting) {

        }
    }
    private void SelectButton() {
        float inputHorizontal = Input.GetAxis("Horizontal");
        float inputVertical = Input.GetAxis("Vertical");


    }
    private void GetStateName() {
        _stateName = _canvasManager.StateSet();
        print(_stateName);
    }
    #endregion
}