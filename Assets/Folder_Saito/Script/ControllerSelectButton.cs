// ---------------------------------------------------------
// ControllerSelectButton.cs
//
// 作成日:
// 作成者:
// ---------------------------------------------------------
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ControllerSelectButton : MonoBehaviour {
    #region 変数
    [SerializeField] private CanvasManager _canvasManager;
    private string _stateName;
    [SerializeField, Header("選ばれているときのスプライト")] private Sprite _nowSprites;
    [SerializeField, Header("選ばれてないときのスプライト")] private Sprite _notSprites;

    [Header("タイトル")]
    [SerializeField] private string _title;
    [SerializeField] private GameObject[] _titleButtons;
    private GameObject _titleNowSelect;
    private int _titleIndex = 0;

    [Header("ポーズ")]
    [SerializeField] private string _menu;
    [SerializeField] private GameObject[] _menuButtons;
    private GameObject _menuNowSelect;
    private int _menuIndex = 0;

    [Header("設定")]
    [SerializeField] private string _setting;
    [SerializeField] private GameObject[] _settingButtons;
    private GameObject _settingNowSelect;
    private int _settingIndex = 0;

    private bool _selected = true;
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
        _titleNowSelect = _titleButtons[0];
        _menuNowSelect = _menuButtons[0];
        _settingNowSelect = _settingButtons[0];
    }
    /// <summary>
    /// 更新処理
    /// </summary>
    void Update() {
        GetStateName();
        if (_stateName == _title) {
            SelectTitleButton();
        }
        if (_stateName == _menu) {
            SelectMenuButton();
        }
        if (_stateName == _setting) {

        }
    }
    private void SelectTitleButton() {
        float inputVertical = Input.GetAxis("Vertical");

        print(_titleNowSelect.name);
        if (inputVertical < 0.5f && inputVertical > -0.5f) {
            _selected = true;
        }
        if (inputVertical >= 0.5f && _selected) {
            if (_titleNowSelect == _titleButtons[0]) {
                return;
            }
            _titleIndex--;
            _titleNowSelect = _titleButtons[_titleIndex];
            _titleNowSelect.GetComponent<Image>().sprite = _nowSprites;
            _titleButtons[_titleIndex + 1].GetComponent<Image>().sprite = _notSprites;
            _selected = false;
        }
        if (inputVertical <= -0.5f && _selected) {
            if (_titleNowSelect == _titleButtons[_titleButtons.Length - 1]) {
                return;
            }
            _titleIndex++;
            _titleNowSelect = _titleButtons[_titleIndex];
            _titleNowSelect.GetComponent<Image>().sprite = _nowSprites;
            _titleButtons[_titleIndex - 1].GetComponent<Image>().sprite = _notSprites;
            _selected = false;
        }
        if (Input.GetButton("Submit")) {
            _titleNowSelect.GetComponent<TestButton>().OnClickSw();
            _selected = true;
        }
    }
    private void SelectMenuButton() {
        float inputVertical = Input.GetAxis("Vertical");

        print(_menuNowSelect.name);
        if (inputVertical < 0.5f && inputVertical > -0.5f) {
            _selected = true;
        }
        if (inputVertical >= 0.5f && _selected) {
            if (_menuNowSelect == _menuButtons[0]) {
                return;
            }
            _menuIndex--;
            _menuNowSelect = _menuButtons[_menuIndex];
            _menuNowSelect.GetComponent<Image>().sprite = _nowSprites;
            _menuButtons[_menuIndex + 1].GetComponent<Image>().sprite = _notSprites;
            _selected = false;
        }
        if (inputVertical <= -0.5f && _selected) {
            if (_menuNowSelect == _menuButtons[_menuButtons.Length - 1]) {
                return;
            }
            _menuIndex++;
            _menuNowSelect = _menuButtons[_menuIndex];
            _menuNowSelect.GetComponent<Image>().sprite = _nowSprites;
            _menuButtons[_menuIndex - 1].GetComponent<Image>().sprite = _notSprites;
            _selected = false;
        }
        if (Input.GetButton("Submit")) {
            _titleNowSelect.GetComponent<TestButton>().OnClickSw();
            _selected = true;
        }

    }
    private void SelectSettingButton() {

    }
    private void GetStateName() {
        _stateName = _canvasManager.StateSet();
        print(_stateName);
    }
    #endregion
}