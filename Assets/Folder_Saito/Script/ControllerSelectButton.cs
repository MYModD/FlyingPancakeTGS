using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControllerSelectButton : MonoBehaviour {
    #region 変数
    [SerializeField] private CanvasManager _canvasManager;
    private string _stateName;
    [SerializeField, Header("選ばれているときのスプライト")] private Sprite _nowSprites;
    [SerializeField, Header("選ばれてないときのスプライト")] private Sprite _notSprites;
    [SerializeField, Header("BGMスピーカー")] private AudioSource _audioBGM;
    [SerializeField, Header("BGMスライダー")] private Slider _sliderBGM;
    [SerializeField, Header("BGMの丸")] private Image _imageBGM;
    [SerializeField, Header("SEスピーカー")] private AudioSource _audioSE;
    [SerializeField, Header("SEスライダー")] private Slider _sliderSE;
    [SerializeField, Header("SEの丸")] private Image _imageSE;

    [Header("タイトル")]
    [SerializeField] private string _title;
    [SerializeField] private GameObject[] _titleButtons;
    private Image[] _titleImages;
    private GameObject _titleNowSelect;
    private int _titleIndex = 0;

    [Header("ポーズ")]
    [SerializeField] private string _menu;
    [SerializeField] private GameObject[] _menuButtons;
    private Image[] _menuImages;
    private GameObject _menuNowSelect;
    private int _menuIndex = 0;

    [Header("設定")]
    [SerializeField] private string _setting;
    [SerializeField] private GameObject[] _settingButtons;
    [SerializeField] private TextMeshProUGUI _textBGM;
    [SerializeField] private TextMeshProUGUI _textSE;
    private string _settingTextTrue = "オン";
    private string _settingTextFalse = "オフ";
    private Image[] _settingImages;
    private GameObject _settingNowSelect;
    private int _settingIndex = 0;
    private bool _horizontalInversion = true;
    private bool _verticalInversion = true;

    private bool _selected = true;
    #endregion

    #region メソッド
    void Start() {
        _titleNowSelect = _titleButtons[0];
        _menuNowSelect = _menuButtons[0];
        _settingNowSelect = _settingButtons[0];

        _titleImages = new Image[_titleButtons.Length];
        _menuImages = new Image[_menuButtons.Length];
        _settingImages = new Image[_settingButtons.Length];

        _titleImages = GetImage(_titleImages, _titleButtons);
        _menuImages = GetImage(_menuImages, _menuButtons);
        _settingImages = GetImage(_settingImages, _settingButtons);
    }

    private Image[] GetImage(Image[] images, GameObject[] objects) {
        for (int index = 0; index < objects.Length; index++) {
            images[index] = objects[index].GetComponent<Image>();
        }
        return images;
    }

    void Update() {
        GetStateName();
        if (_stateName == _title) {
            SelectTitleButton();
        }
        if (_stateName == _menu) {
            SelectMenuButton();
        }
        if (_stateName == _setting) {
            SelectSettingButton();
        }
    }

    private void SetNotImage(Image[] imgs) {
        foreach (Image img in imgs) {
            img.sprite = _notSprites;
        }
    }

    private void SelectTitleButton() {
        float inputVertical = Input.GetAxis("Vertical");
        float sensitivity = 0.5f;
        if (Mathf.Abs(inputVertical) < sensitivity) {
            _selected = true;
        }

        if (inputVertical >= sensitivity && _selected) {
            if (_titleIndex > 0) {
                _titleIndex--;
                _selected = false;
            }
        } else if (inputVertical <= -sensitivity && _selected) {
            if (_titleIndex < _titleButtons.Length - 1) {
                _titleIndex++;
                _selected = false;
            }
        }

        _titleNowSelect = _titleButtons[_titleIndex];
        SetNotImage(_titleImages);
        _titleImages[_titleIndex].sprite = _nowSprites;

        if (Input.GetButton("Submit")) {
            _titleNowSelect.GetComponent<TestButton>().OnClickSw();
            _selected = true;
        }
    }

    private void SelectMenuButton() {
        float inputVertical = Input.GetAxis("Vertical");
        float sensitivity = 0.5f;
        if (Mathf.Abs(inputVertical) < sensitivity) {
            _selected = true;
        }

        if (inputVertical >= sensitivity && _selected) {
            if (_menuIndex > 0) {
                _menuIndex--;
                _selected = false;
            }
        } else if (inputVertical <= -sensitivity && _selected) {
            if (_menuIndex < _menuButtons.Length - 1) {
                _menuIndex++;
                _selected = false;
            }
        }

        _menuNowSelect = _menuButtons[_menuIndex];
        SetNotImage(_menuImages);
        _menuImages[_menuIndex].sprite = _nowSprites;

        if (Input.GetButton("Submit")) {
            _menuNowSelect.GetComponent<TestButton>().OnClickSw();
            _selected = true;
        }
    }

    private void SelectSettingButton() {
        float inputVertical = Input.GetAxis("Vertical");
        float volumeBGM = _audioBGM.volume;
        float volumeSE = _audioSE.volume;
        float sensitivity = 0.5f;
        if (Mathf.Abs(inputVertical) < sensitivity) {
            _selected = true;
        }

        if (inputVertical >= sensitivity && _selected) {
            if (_settingIndex > 0) {
                _settingIndex--;
                _settingNowSelect = _settingButtons[_settingIndex];
                _selected = false;
            }
        } else if (inputVertical <= -sensitivity && _selected) {
            if (_settingIndex < _settingButtons.Length - 1) {
                _settingIndex++;
                _settingNowSelect = _settingButtons[_settingIndex];
                _selected = false;
            }
        }



        if (_settingNowSelect == _settingButtons[0]) {
            _settingImages[0].color = Color.yellow;
            _settingImages[1].color = Color.white;
            volumeBGM += Time.deltaTime * Input.GetAxisRaw("RStickH");
            _audioBGM.volume = volumeBGM;
            _sliderBGM.value = volumeBGM;
            _settingImages[2].sprite = _notSprites;
            _settingImages[3].sprite = _notSprites;
        } else if (_settingNowSelect == _settingButtons[1]) {
            _settingImages[0].color = Color.white;
            _settingImages[1].color = Color.yellow;
            volumeSE += Time.deltaTime * Input.GetAxisRaw("RStickH");
            _audioSE.volume = volumeSE;
            _sliderSE.value = volumeSE;
            _settingImages[2].sprite = _notSprites;
            _settingImages[3].sprite = _notSprites;
        } else if (_settingNowSelect == _settingButtons[2]) {
            _settingImages[0].color = Color.white;
            _settingImages[1].color = Color.white;
            _settingImages[2].sprite = _nowSprites;
            _settingImages[3].sprite = _notSprites;
        } else if (_settingNowSelect == _settingButtons[3]) {
            _settingImages[0].color = Color.white;
            _settingImages[1].color = Color.white;
            _settingImages[2].sprite = _notSprites;
            _settingImages[3].sprite = _nowSprites;
        }
        if (Input.GetButton("Submit")) {
            if (_settingNowSelect == _settingButtons[2]) {
                _verticalInversion = TrueFalseInversion(_verticalInversion);
                SetTextOnOff(_textBGM, _verticalInversion);
            } else if (_settingNowSelect == _settingButtons[3]) {
                _horizontalInversion = TrueFalseInversion(_horizontalInversion);
                SetTextOnOff(_textSE, _horizontalInversion);
            }
            // _settingImages[_settingIndex].sprite = _nowSprites;
            _selected = true;
        }


    }

    private void SetTextOnOff(TextMeshProUGUI text, bool check) {
        text.text = check ? _settingTextTrue : _settingTextFalse;
    }

    private bool TrueFalseInversion(bool switchBool) {
        return !switchBool;
    }

    private void GetStateName() {
        _stateName = _canvasManager.StateSet();
    }

    public bool VerticalInversionCheak() {
        return _verticalInversion;
    }

    public bool HorizontalInversionCheak() {
        return _horizontalInversion;
    }
    #endregion
}
