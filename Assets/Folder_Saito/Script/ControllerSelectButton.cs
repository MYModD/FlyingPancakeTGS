using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;

public class ControllerSelectButton : MonoBehaviour {
    #region 変数
    [SerializeField, Header("キャンバスマネージャー")] private CanvasManager _canvasManager;
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
    [SerializeField] private string[] _opText;
    [SerializeField] private AudioClip[] _opClip;
    private int _indexOP = 0;
    private int _checkIndexOp = -1;

    [Header("ED")]
    [SerializeField, Header("EDと入れてね")] private string _ed;
    [SerializeField] private TextMeshProUGUI[] _textEd;
    [SerializeField] private string[] _edText;
    [SerializeField] private AudioClip[] _edClip;
    private int _indexED = 0;

    private string _settingTextEnglish = "English";
    private string _settingTextJapanece = "日本語";
    private Image[] _settingImages;
    private GameObject _settingNowSelect;
    private int _settingIndex = 0;

    #region ゲーム内のテキスト
    [SerializeField, Header("スタートボタンの中のテキスト")] private TextMeshProUGUI _startButton;
    [SerializeField, Header("ゲーム終了ボタンの中のテキスト")] private TextMeshProUGUI _finishButton;
    [SerializeField, Header("タイトルに戻るボタンの中のテキスト")] private TextMeshProUGUI _backTitleButton;
    [SerializeField, Header("BGM項目の表示")] private TextMeshProUGUI _mainVomuleText;
    [SerializeField, Header("SE項目の表示")] private TextMeshProUGUI _subVolumeText;
    [SerializeField, Header("上下反転の項目の表示")] private TextMeshProUGUI _verticalInversionText;
    [SerializeField, Header("左右反転の項目の表示")] private TextMeshProUGUI _horizontalInversionText;
    [SerializeField, Header("言語項目の表示")] private TextMeshProUGUI _languageText;
    [SerializeField, Header("設定画面")] private TextMeshProUGUI _settingText;
    [SerializeField, Header("一時停止")] private TextMeshProUGUI _gameStopText;
    [SerializeField, Header("戻る")] private TextMeshProUGUI _goBackText;
    [SerializeField, Header("撃破数")] private TextMeshProUGUI _killCountText;
    [SerializeField, Header("撃破数")] private TextMeshProUGUI _killCountResultText;
    [SerializeField, Header("タイム")] private TextMeshProUGUI _timeText;
    [SerializeField, Header("タイム")] private TextMeshProUGUI _timeResultText;
    #endregion
    #region 英語文字
    private string _englishStart = "Game Start";
    private string _englishFinish = "Quit The Game";
    private string _englishBackTitle = "Return To Title";
    private string _englishBGM = "BGM Volume";
    private string _englishSE = "SE Volume";
    private string _englishVerticalFlip = "Flip Vertical";
    private string _englishHorizontalFlip = "Flip Horizontal";
    private string _englishSetting = "Game Setting";
    private string _englishGameStop = "Pause";
    private string _englishLanguage = "Language";
    private string _englishTextTrue = "Flip On";
    private string _englishTextFalse = "Flip Off";
    private string _englishGoBack = "Go back with B ";
    private string _englishKill = "Kill Count";
    private string _englishTime = "Time";
    private string _englishResultTime = "Clear Time";
    #endregion
    #region 日本語文字
    private string _japaneceStart = "ゲームスタート";
    private string _japaneceFinish = "ゲーム終了";
    private string _japaneceBackTitle = "タイトルに戻る";
    private string _japaneceBGM = "BGM音量";
    private string _japaneceSE = "SE音量";
    private string _japaneceVerticalFlip = "上下反転";
    private string _japaneceHorizontalFlip = "左右反転";
    private string _japaneceLanguage = "言語";
    private string _japaneceSetting = "設 定 画 面";
    private string _japaneceGameStop = "一 時 停 止";
    private string _japaneceTextTrue = "オン";
    private string _japaneceTextFalse = "オフ";
    private string _japaneceGoBack = "Bボタンで戻る";
    private string _japaneceKill = "撃破数";
    private string _japaneceTime = "経過時間";
    private string _japaneceResultTime = "クリアタイム";
    #endregion

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
        //EnglishSwitchJapanece();
    }

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
    /// 設定でのリセット処理
    /// </summary>
    private void ResetSettings() {
        _settingImages[(int)SettingState.mainVolume].color = Color.white;
        _settingImages[(int)SettingState.seVolume].color = Color.white;
        _settingImages[(int)SettingState.verticalInversion].sprite = _notSprites;
        _settingImages[(int)SettingState.horizontalInversion].sprite = _notSprites;
        _settingImages[(int)SettingState.language].sprite = _notSprites;
    }
    private void EnglishSwitchJapanece() {
        if (!_isChangeLanguage) {
            return;
        }
        //英語化
        if (_isLanguageEnglish) {
            _startButton.text = _englishStart;
            _finishButton.text = _englishFinish;
            _backTitleButton.text = _englishBackTitle;
            _mainVomuleText.text = _englishBGM;
            _subVolumeText.text = _englishSE;
            _verticalInversionText.text = _englishVerticalFlip;
            _horizontalInversionText.text = _englishHorizontalFlip;
            _languageText.text = _englishLanguage;
            _settingText.text = _englishSetting;
            _gameStopText.text = _englishGameStop;
            _goBackText.text = _englishGoBack;
            _timeResultText.text = _englishResultTime;
            _timeText.text = _englishTime;
            _killCountResultText.text = _englishKill;
            _killCountText.text = _englishKill;
            _startButton.font = _englishFont;
            _finishButton.font = _englishFont;
            _backTitleButton.font = _englishFont;
            _mainVomuleText.font = _englishFont;
            _subVolumeText.font = _englishFont;
            _verticalInversionText.font = _englishFont;
            _horizontalInversionText.font = _englishFont;
            _languageText.font = _englishFont;
            _settingText.font = _englishFont;
            _gameStopText.font = _englishFont;
            _goBackText.font = _englishFont;
            _timeResultText.font = _englishFont;
            _timeText.font = _englishFont;
            _killCountResultText.font = _englishFont;
            _killCountText.font = _englishFont;
        }
        //日本語化
        else {
            _startButton.text = _japaneceStart;
            _finishButton.text = _japaneceFinish;
            _backTitleButton.text = _japaneceBackTitle;
            _mainVomuleText.text = _japaneceBGM;
            _subVolumeText.text = _japaneceSE;
            _verticalInversionText.text = _japaneceVerticalFlip;
            _horizontalInversionText.text = _japaneceHorizontalFlip;
            _languageText.text = _japaneceLanguage;
            _settingText.text = _japaneceSetting;
            _gameStopText.text = _japaneceGameStop;
            _goBackText.text = _japaneceGoBack;
            _timeResultText.text = _japaneceResultTime;
            _timeText.text = _japaneceTime;
            _killCountResultText.text = _japaneceKill;
            _killCountText.text = _japaneceKill;
            _startButton.font = _japaneceFont;
            _finishButton.font = _japaneceFont;
            _backTitleButton.font = _japaneceFont;
            _mainVomuleText.font = _japaneceFont;
            _subVolumeText.font = _japaneceFont;
            _verticalInversionText.font = _japaneceFont;
            _horizontalInversionText.font = _japaneceFont;
            _languageText.font = _japaneceFont;
            _settingText.font = _japaneceFont;
            _gameStopText.font = _japaneceFont;
            _goBackText.font = _japaneceFont;
            _timeResultText.font = _japaneceFont;
            _timeText.font = _japaneceFont;
            _killCountResultText.font = _japaneceFont;
            _killCountText.font = _japaneceFont;
        }
        SetTextOnOff(_textBGM, _verticalInversion);
        SetTextOnOff(_textSE, _horizontalInversion);
        _isChangeLanguage = false;
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
    private void OPStartProcess() {
        if (_checkIndexOp != _indexOP) {
            _audioBGM.Stop();
            _textOp[_indexOP].enabled = true;
            _audioSE.PlayOneShot(_opClip[_indexOP]);
            _checkIndexOp = _indexOP;
        }
        if (Input.GetButtonDown("Submit")) {
            _indexOP++;
            if (_indexOP >= _textOp.Length) {
                _audioBGM.Play();
                _canvasManager.OPToGamePlay();
            }
        }
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
    /// <summary>
    /// boolがtrueはオン、falseはオフ
    /// </summary>
    /// <param name="text">セットしたいテキストコンポーネント</param>
    /// <param name="check">判断材料にしたいbool</param>
    private void SetTextOnOff(TextMeshProUGUI text, bool check) {
        if (_isLanguageEnglish) {
            text.text = check ? _englishTextTrue : _englishTextFalse;
        } else {
            text.text = check ? _japaneceTextTrue : _japaneceTextFalse;
        }

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
    #endregion
}
