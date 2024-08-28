using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;

public class ControllerSelectButton : MonoBehaviour {
    #region 変数
    [SerializeField, Header("CountdownUI")] private CountDownUI _countDown;
    [SerializeField, Header("OPのカメラ")] private GameObject _opCamera;
    [SerializeField, Header("キャンバスマネージャー")] private CanvasManager _canvasManager;
    private string _stateName;
    [SerializeField, Header("選ばれているときのスプライト")] private Sprite _nowSprites;
    [SerializeField, Header("選ばれてないときのスプライト")] private Sprite _notSprites;

    [SerializeField, Header("BGMスピーカー")] private AudioSource _audioBGM;
    [SerializeField, Header("BGMスライダー")] private Slider _sliderBGM;
    [SerializeField, Header("BGMの丸")] private Image _imageBGM;
    [SerializeField, Header("ゲームプレイBGM")] private AudioClip _audioGamePlay;
    [SerializeField, Header("エンディングBGM")]private AudioClip _audioEnding;

    [SerializeField, Header("SEスピーカー")] private AudioSource _audioSE;
    [SerializeField, Header("SEスライダー")] private Slider _sliderSE;
    [SerializeField, Header("SEの丸")] private Image _imageSE;

    [Header("タイトル")]
    [SerializeField, Header("titleと入れてね")] private string _title;
    [SerializeField, Header("上から順番に入れてね")] private GameObject[] _titleButtons;
    private Image[] _titleImages;
    private GameObject _titleNowSelect;
    private int _titleIndex = 0;

    [Header("ポーズ")]
    [SerializeField, Header("menuと入れてね")] private string _menu;
    [SerializeField, Header("上から順番に入れてね")] private GameObject[] _menuButtons;
    private Image[] _menuImages;
    private GameObject _menuNowSelect;
    private int _menuIndex = 0;

    [Header("設定")]
    [SerializeField, Header("settingと入れてね")] private string _setting;
    [SerializeField, Header("上から順番に入れてね")] private GameObject[] _settingButtons;
    [SerializeField, Header("オンオフの表示")] private TextMeshProUGUI _textBGM;
    [SerializeField, Header("オンオフの表示")] private TextMeshProUGUI _textSE;
    [SerializeField, Header("オンオフの表示")] private TextMeshProUGUI _textLanguage;

    [Header("OP")]
    [SerializeField, Header("OPと入れてね")] private string _op;
    [SerializeField] private TextMeshProUGUI[] _textOp;
    [SerializeField] private AudioClip[] _opClip;
    private int _indexOP = 0;
    private int _checkIndexOp = -1;
    private bool _checkedOP = true;

    [Header("ED")]
    [SerializeField, Header("EDと入れてね")] private string _ed;
    [SerializeField] private TextMeshProUGUI[] _textEd;
    [SerializeField] private AudioClip[] _edClip;
    private int _indexED = 0;
    private int _checkIndexEd = -1;

    [Header("リザルト")]
    [SerializeField, Header("resultと入れてね")] private string _result;
    [SerializeField] private TextMeshProUGUI[] _textResult;
    private int _indexResult = 0;
    private int _checkIndexResult = -1;

    private string _settingTextEnglish = "English";
    private string _settingTextJapanece = "Japanese";
    private Image[] _settingImages;
    private GameObject _settingNowSelect;
    private int _settingIndex = 0;

    private string _englishTextTrue = "Flip On";
    private string _englishTextFalse = "Flip Off";

    [SerializeField] private TMP_FontAsset _englishFont;
    [SerializeField] private TMP_FontAsset _japaneceFont;


    private bool _isLanguageEnglish = false;
    private bool _horizontalInversion = true;
    private bool _verticalInversion = true;

    private bool _isChangeLanguage = false;

    private bool _selected = true;
    /// <summary>
    /// タイトルのボタンに対応するもの
    /// </summary>
    private enum TitleState {
        goGame = 0,
        setting,
        finish,
    }
    /// <summary>
    /// メニューの対応するもの
    /// </summary>
    private enum MenuState {
        goTitie = 0,
        setting,
    }
    /// <summary>
    /// 設定画面の対応するもの
    /// </summary>
    private enum SettingState {
        mainVolume = 0,
        seVolume,
        verticalInversion,
        horizontalInversion,
        language,
    }
    #endregion

    #region メソッド
    void Start() {
        //最初に選択しておきたいオブジェクト
        _titleNowSelect = _titleButtons[(int)TitleState.goGame];
        _menuNowSelect = _menuButtons[(int)MenuState.goTitie];
        _settingNowSelect = _settingButtons[(int)SettingState.mainVolume];
        //Image配列を使うう大きさにする
        _titleImages = new Image[_titleButtons.Length];
        _menuImages = new Image[_menuButtons.Length];
        _settingImages = new Image[_settingButtons.Length];
        //ゲットコンポーネントして取得
        _titleImages = GetImage(_titleImages, _titleButtons);
        _menuImages = GetImage(_menuImages, _menuButtons);
        _settingImages = GetImage(_settingImages, _settingButtons);
    }

    /// <summary>
    /// ImageをGetComponentして配列に入れ取得する
    /// </summary>
    /// <param name="images">コンポーネントを入れたい配列</param>
    /// <param name="objects">GetComponentしたいゲームオブジェクト配列</param>
    /// <returns>入れた配列返す</returns>
    private Image[] GetImage(Image[] images, GameObject[] objects) {
        for (int index = 0; index < objects.Length; index++) {
            images[index] = objects[index].GetComponent<Image>();
        }
        return images;
    }

    void Update() {
        //ステータスの取得
        GetStateName();
        //ステータスに応じて動きを変える
        if (_stateName == _title) {
            SelectTitleButton();
        }
        if (_stateName == _menu) {
            SelectMenuButton();
        }
        if (_stateName == _setting) {
            SelectSettingButton();
        }
        if (_stateName == _op) {
            OPStartProcess();
        }
        if (_stateName == _ed) {
            EDStartProcess();
        }
        if(_stateName == _result) {
            ResultProcess();
        }
        //EnglishSwitchJapanece();
    }
    #region タイトル
    /// <summary>
    /// Titleでの処理
    /// </summary>
    private void SelectTitleButton() {
        //縦方向の入力取得
        float inputVertical = Input.GetAxis("Vertical");
        //取得下限値
        float sensitivity = 0.5f;
        //選択中のボタンの見た目を変える
        _titleImages[_titleIndex].sprite = _nowSprites;
        //決定押された時
        if (Input.GetButtonDown("Submit")) {
            print("こんにちわ");
            //選択しているボタンの押された時の処理を行う
            _titleNowSelect.GetComponent<TestButton>().OnClickSw();
        }
        //入力の無いとき
        if (Mathf.Abs(inputVertical) < sensitivity) {
            _selected = true;
            return;
        }
        //上への入力
        if (inputVertical >= sensitivity && _selected) {
            //カーソルを上にする
            if (_titleIndex > (int)TitleState.goGame) {
                _titleIndex--;
                _selected = false;
            }
        }
        //下への入力
        else if (inputVertical <= -sensitivity && _selected) {
            //カーソルを下にする
            if (_titleIndex < _titleButtons.Length - 1) {
                _titleIndex++;
                _selected = false;
            }
        }
        //現在選択中のボタンを更新
        _titleNowSelect = _titleButtons[_titleIndex];
        //リセット
        SetNotImage(_titleImages);
    }
    #endregion
    #region メニュー
    /// <summary>
    /// Menuでの処理
    /// </summary>
    private void SelectMenuButton() {
        //縦方向の入力を取得
        float inputVertical = Input.GetAxis("Vertical");
        //取得下限値
        float sensitivity = 0.5f;
        //選択中の見た目へ
        _menuImages[_menuIndex].sprite = _nowSprites;
        //決定が押されたら
        if (Input.GetButtonDown("Submit")) {
            //選択しているボタンの押された時の処理を行う
            _menuNowSelect.GetComponent<TestButton>().OnClickSw();
        }
        //入力がないときにはなにもしない
        if (Mathf.Abs(inputVertical) < sensitivity) {
            _selected = true;
            return;
        }
        //上への入力
        if (inputVertical >= sensitivity && _selected) {
            //カーソルを上にする
            if (_menuIndex > (int)MenuState.goTitie) {
                _menuIndex--;
                _selected = false;
            }
        }
        //下への入力
        else if (inputVertical <= -sensitivity && _selected) {
            //カーソルを下にする
            if (_menuIndex < _menuButtons.Length - 1) {
                _menuIndex++;
                _selected = false;
            }
        }
        //現在の選択しているボタンの更新
        _menuNowSelect = _menuButtons[_menuIndex];
        //リセット処理
        SetNotImage(_menuImages);
    }
    #endregion
    #region 設定画面
    /// <summary>
    /// 設定画面での処理
    /// </summary>
    private void SelectSettingButton() {
        //縦の入力
        float inputVertical = Input.GetAxis("Vertical");
        //BGMAudioSourceの音量
        float volumeBGM = _audioBGM.volume;
        //SEAudioSourceの音量
        float volumeSE = _audioSE.volume;
        //入力下限値
        float sensitivity = 0.5f;
        //決定処理
        if (Input.GetButtonDown("Submit")) {
            //上下反転
            if (_settingNowSelect == _settingButtons[2]) {
                _verticalInversion = TrueFalseInversion(_verticalInversion);
                SetTextOnOff(_textBGM, _verticalInversion);
            }
            //左右反転
            else if (_settingNowSelect == _settingButtons[3]) {
                _horizontalInversion = TrueFalseInversion(_horizontalInversion);
                SetTextOnOff(_textSE, _horizontalInversion);
            }
            //言語
            else if (_settingNowSelect == _settingButtons[4]) {
                _isLanguageEnglish = TrueFalseInversion(_isLanguageEnglish);
                _isChangeLanguage = true;
                SetTextEnglish(_textLanguage, _isLanguageEnglish);

                //EnglishSwitchJapanece();
            }
            // _settingImages[_settingIndex].sprite = _nowSprites;
            //_selected = true;
        }
        //それぞれに応じた動き
        //BGM
        if (_settingNowSelect == _settingButtons[(int)SettingState.mainVolume]) {
            HandleMainVolumeSetting(volumeBGM);
        }
        //SE
        else if (_settingNowSelect == _settingButtons[(int)SettingState.seVolume]) {
            HandleSEVolumeSetting(volumeSE);
        }
        //上下反転
        else if (_settingNowSelect == _settingButtons[(int)SettingState.verticalInversion]) {
            VerticalInversionSetting();
        }
        //左右反転
        else if (_settingNowSelect == _settingButtons[(int)SettingState.horizontalInversion]) {
            HorizontalInversionSetting();
        }
        //言語
        else if (_settingNowSelect == _settingButtons[(int)SettingState.language]) {
            LanguageSetting();
        }
        //入力ないとき
        if (Mathf.Abs(inputVertical) < sensitivity) {
            _selected = true;
            return;
        }
        //上への入力
        if (inputVertical >= sensitivity && _selected) {
            if (_settingIndex > (int)SettingState.mainVolume) {
                _settingIndex--;
                _settingNowSelect = _settingButtons[_settingIndex];
                _selected = false;
            }
        }
        //下への入力
        else if (inputVertical <= -sensitivity && _selected) {
            if (_settingIndex < _settingButtons.Length - 1) {
                _settingIndex++;
                _settingNowSelect = _settingButtons[_settingIndex];
                _selected = false;
            }
        }
        //リセット
        ResetSettings();

    }
    #endregion
    #region 設定画面の動き働き
    /// <summary>
    /// 設定でのリセット処理
    /// </summary>
    private void ResetSettings() {
        _settingImages[(int)SettingState.mainVolume].color = Color.white;
        _settingImages[(int)SettingState.seVolume].color = Color.white;
        _settingImages[(int)SettingState.verticalInversion].sprite = _notSprites;
        _settingImages[(int)SettingState.horizontalInversion].sprite = _notSprites;
        _settingImages[(int)SettingState.language].sprite = _notSprites;
    }
    /// <summary>
    /// BGMの大きさ調整
    /// </summary>
    /// <param name="volumeBGM">BGMAudioSourceの現在の大きさ</param>
    private void HandleMainVolumeSetting(float volumeBGM) {
        //黄色にして選択中を表現
        _settingImages[(int)SettingState.mainVolume].color = Color.yellow;
        //音量を足し引き
        volumeBGM += Time.deltaTime * Input.GetAxisRaw("RStickH");
        //ボリュームを設定
        _audioBGM.volume = volumeBGM;
        //ボリュームを設定
        _sliderBGM.value = volumeBGM;
    }
    /// <summary>
    /// SEの大きさ調整
    /// </summary>
    /// <param name="volumeSE">SEAudioSourceの現在の大きさ</param>
    private void HandleSEVolumeSetting(float volumeSE) {
        //黄色にして選択中を表現
        _settingImages[(int)SettingState.seVolume].color = Color.yellow;
        //音量を足し引き
        volumeSE += Time.deltaTime * Input.GetAxisRaw("RStickH");
        //ボリュームを設定
        _audioSE.volume = volumeSE;
        //ボリュームを設定
        _sliderSE.value = volumeSE;
    }
    /// <summary>
    /// 上下反転スイッチにカーソルがある時の画像
    /// </summary>
    private void VerticalInversionSetting() {
        //選択中を表現
        _settingImages[(int)SettingState.verticalInversion].sprite = _nowSprites;
    }
    /// <summary>
    /// 左右反転スイッチにカーソルがある時の画像
    /// </summary>
    private void HorizontalInversionSetting() {
        //選択中を表現
        _settingImages[(int)SettingState.horizontalInversion].sprite = _nowSprites;
    }
    /// <summary>
    /// 言語スイッチにカーソルがある時の画像
    /// </summary>
    private void LanguageSetting() {
        //選択中を表現
        _settingImages[(int)SettingState.language].sprite = _nowSprites;
    }
    #endregion
    #region OPED
    private void OPStartProcess() {
        if (!_checkedOP) {
            return;
        }
        if (_checkIndexOp != _indexOP) {
            if (_checkIndexOp >= 0) {
                _textOp[_indexOP - 1].enabled = false;
            }
            _audioBGM.Stop();
            _textOp[_indexOP].enabled = true;
            _audioSE.Stop();
            _audioSE.PlayOneShot(_opClip[_indexOP]);
            _checkIndexOp = _indexOP;
        }
        if (Input.GetButtonDown("Submit")) {
            _indexOP++;
            if (_indexOP >= _textOp.Length) {
                _countDown.PublicStart();
                _opCamera.SetActive(false);
                _canvasManager.OPtoCount();
                _audioBGM.clip = _audioGamePlay;
                _audioBGM.Play();
                _indexOP = 0;
                _checkedOP = false;
            }
        }
    }
    private void EDStartProcess() {
        print(_audioSE.volume);
        if (_checkIndexEd != _indexED) {
            if (_checkIndexEd >= 0) {
                _textEd[_indexED - 1].enabled = false;
            }
            _textEd[_indexED].enabled = true;
            _audioSE.Stop();
            _audioSE.PlayOneShot(_edClip[_indexED]);
            _checkIndexEd = _indexED;
        }
        if (Input.GetButtonDown("Submit")) {
            _indexED++;
            if (_indexED >= _textEd.Length) {
                _canvasManager.EDToResult();
            }
        }
    }
    #endregion
    private void ResultProcess() {
        if (_checkIndexResult != _indexResult) {
            _textResult[_indexResult].enabled = true;
            _checkIndexResult = _indexResult;
        }
        if (Input.GetButtonDown("Submit")) {
            _indexResult++;
            if (_indexResult >= _textResult.Length) {
                _canvasManager.MenuOrResultToStart();
            }
        }
    }
    /// <summary>
    /// すべてを選んでいない画像にする
    /// </summary>
    /// <param name="imgs">処理を行わせたい配列</param>
    private void SetNotImage(Image[] imgs) {
        foreach (Image img in imgs) {
            img.sprite = _notSprites;
        }
    }
    /// <summary>
    /// boolがtrueはオン、falseはオフ
    /// </summary>
    /// <param name="text">セットしたいテキストコンポーネント</param>
    /// <param name="check">判断材料にしたいbool</param>
    private void SetTextOnOff(TextMeshProUGUI text, bool check) {
        //if (_isLanguageEnglish) {
        text.text = check ? _englishTextTrue : _englishTextFalse;
        //} else {
        //    text.text = check ? _japaneceTextTrue : _japaneceTextFalse;
        //}

    }

    /// <summary>
    /// boolがtrueはEnglish、falseは日本語
    /// </summary>
    /// <param name="text">セットしたいテキストコンポーネント</param>
    /// <param name="check">判断材料にしたいbool</param>
    private void SetTextEnglish(TextMeshProUGUI text, bool check) {
        text.text = check ? _settingTextEnglish : _settingTextJapanece;
    }
    /// <summary>
    /// trueとfalseを入れ替える
    /// </summary>
    /// <param name="switchBool">入れ替えたいbool</param>
    /// <returns></returns>
    private bool TrueFalseInversion(bool switchBool) {
        return !switchBool;
    }
    /// <summary>
    /// 現在のステータスを取得しString型で持つ
    /// </summary>
    private void GetStateName() {
        _stateName = _canvasManager.StateSet();
    }

    /// <summary>
    /// 上下反転スイッチ渡し
    /// </summary>
    /// <returns>上下反転スイッチ</returns>
    public bool VerticalInversionCheak() {
        return _verticalInversion;
    }
    /// <summary>
    /// 左右反転スイッチ渡し
    /// </summary>
    /// <returns>左右反転スイッチ</returns>
    public bool HorizontalInversionCheak() {
        return _horizontalInversion;
    }
    /// <summary>
    /// 言語スイッチ渡し
    /// </summary>
    /// <returns>言語スイッチ</returns>
    public bool LanguageInversionCheak() {
        return _isLanguageEnglish;
    }
    public void StartBGM() {
        _audioBGM.clip = _audioEnding;
        _audioBGM.Play();
    }
    #endregion
}
