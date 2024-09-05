// ---------------------------------------------------------
// CanvasManager.cs
//
// 作成日:
// 作成者:
// ---------------------------------------------------------
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using NaughtyAttributes;
using UnityEngine.InputSystem;
using UnityEngine.Splines;
using Unity.VisualScripting;
public class CanvasManager : MonoBehaviour
{
    #region 変数
    [SerializeField, Header("ControllerSelectButton")] private ChengeStageManager _staChange;
    [SerializeField, Header("ControllerSelectButton")] private ControllerSelectButton _button;
    [Header("タイトルのボタン")]
    [SerializeField, Header("スタートボタン")] private Button _titleStart;
    [SerializeField, Header("ゲーム終了ボタン")] private Button _titleGameEnd;
    [SerializeField, Header("設定ボタン")] private Button _titleSetting;

    [SerializeField, Header("タイトルのオブジェクト")] private GameObject[] _titleObjs;
    [SerializeField, Header("カウントダウンのオブジェクト")] private GameObject[] _countObjs;
    [SerializeField, Header("一時停止のオブジェクト")] private GameObject[] _menuObjs;
    [SerializeField, Header("ゲームUIのオブジェクト")] private GameObject[] _gamePlayObjs;
    [SerializeField, Header("リザルトのオブジェクト")] private GameObject[] _resultObjs;
    [SerializeField, Header("設定画面のオブジェクト")] private GameObject[] _settingObjs;
    [SerializeField, Header("オープニングのオブジェクト")] private GameObject[] _openingObjs;
    [SerializeField, Header("エンディングのオブジェクト")] private GameObject[] _endingObjs;
    [SerializeField, Header("ゲームプレイ中に使うオブジェクト")] private GameObject[] _gameObjs;
    [SerializeField] private SplineAnimate _spAnime;

    [SerializeField, Header("タイトルに行かせたいタグ"),Tag] private string _tagTitle;
    [SerializeField, Header("ゲームに行かせたいタグ"), Tag] private string _tagGame;
    [SerializeField, Header("ゲームに戻すタグ"), Tag] private string _tagBackGame;
    [SerializeField, Header("設定に行かせたいタグ"),Tag] private string _tagSetting;
    [SerializeField, Header("ゲーム終了させたいタグ"), Tag] private string _tagFinish;
    [SerializeField]
    private ResultManager _resultManager;

    private float _gamePlayTime;

    [SerializeField,Header("何秒操作がなかったら動画を流すか")]
    private float _titleTime;
    private float _nowTitleTime;
    [SerializeField]
    private GameObject _videoPlay;
    [SerializeField]
    private AudioSource _audioBGM;
    [SerializeField]
    private float _videotime;

    private bool _isStartPush=true;
    private enum UIState {
        title = 0,
        gamePlay,
        result,
        menu,
        setting,
        OP,
        ED,
        Movie,
    }
    private UIState _state;
    private UIState _prevState;
    private bool _canMove = true;
    #endregion
    #region プロパティ
    #endregion
    #region メソッド
    /// <summary>
    /// 初期化処理 使わないなら消す
    /// </summary>
    void Awake()
    {
        _canMove = false;
    }
    /// <summary>
    /// 更新前処理
    /// </summary>
    void Start()
    {
        _state = UIState.title;

    }
    /// <summary>
    /// 更新処理
    /// </summary>
    void Update() {
        if (_state == UIState.title) {
            _nowTitleTime += Time.deltaTime;
            if (_titleTime <= _nowTitleTime) {
                _state = UIState.Movie;
                _audioBGM.Stop();
                _videoPlay.SetActive(true);
                _nowTitleTime = 0;
            }
        }
        if (_state == UIState.gamePlay) {
            _gamePlayTime += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Z)) {
                PlayToED();
            }
            if (Input.GetKeyDown("joystick button 7")&&_isStartPush) {
                PlayToMenu();
                _isStartPush = false;
            }
            if (Input.GetKeyUp("joystick button 7")) {
                _isStartPush = true;
            }
            _canMove = true;
        }
        if (_state == UIState.result) {
            //if (Input.GetButtonDown("Cancel") &&_isStartPush) {
            //    MenuOrResultToStart();
            //}
            _resultManager.SetTexts();
            _canMove= false;
        }
        if (_state == UIState.menu) {
            if (Input.GetKeyDown("joystick button 7") && _isStartPush) {
                MenuToPlay();
                _isStartPush=false;
            }
            if (Input.GetKeyUp("joystick button 7")) {
                _isStartPush = true;
            }
            _canMove=false;
        }
        if (_state == UIState.setting) {
            if (Input.GetButtonDown("Cancel")) {
                SettingToTitleOrMenu();
            }
        }
        if (_state == UIState.Movie) {
            _nowTitleTime += Time.deltaTime;
            if (_nowTitleTime >= _videotime||Input.anyKeyDown) {
                _state = UIState.title;
                _audioBGM.Play();
                _videoPlay.SetActive(false);
                _nowTitleTime = 0;
            }

        }
    }
    public void OPtoCount() {
        print("スタート！！");
        GameObjTrueFalse(_countObjs,_openingObjs);
        
    }
    /// <summary>
    /// 押された時の処理を決める
    /// </summary>
    public void OnClickSw(string tagname) {
        //タイトルからゲームへ
        if (tagname==_tagGame) {
            TitleToOP();
        } else if (tagname==_tagSetting) {
            TitleOrMenuToSetting();
        } else if (tagname==_tagFinish) {
            GameFinish();
        }
        else if (tagname==_tagTitle) {
            MenuOrResultToStart();
        }
        else if (tagname == _tagBackGame) {
            MenuOrResultToStart();
        }

    }
    /// <summary>
    /// タイトルからゲーム画面へ
    /// </summary>
    private void TitleToGamePlay()
    {
        GameObjTrueFalse(_gamePlayObjs, _titleObjs);
        GameObjTrueFalse(_gameObjs, _titleObjs);
        _state = UIState.gamePlay;
    }
    /// <summary>
    /// タイトルからオープニングへ
    /// </summary>
    private void TitleToOP()
    {
        GameObjTrueFalse(_openingObjs, _titleObjs);
        _state = UIState.OP;
    }
    /// <summary>
    /// オープニングからゲーム画面へ
    /// </summary>
    public void OPToGamePlay() {
        GameObjTrueFalse(_gamePlayObjs, _openingObjs);
        GameObjTrueFalse(_gameObjs, _openingObjs);
        _spAnime.enabled = true;
        _state = UIState.gamePlay;
        _staChange.GameStartVoice();
    }
    /// <summary>
    /// ゲーム終了
    /// </summary>
    private void GameFinish()
    {
#if UNITY_EDITOR
        // Unityエディタ内での終了処理
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // ビルドされたゲームの終了処理
        Application.Quit();
#endif
    }
    /// <summary>
    /// ゲーム画面からポーズへ
    /// </summary>
    private void PlayToMenu()
    {
        GameObjTrueFalse(_menuObjs,_gamePlayObjs);
        GameObjTrueFalse(_menuObjs,_gameObjs);
        _state = UIState.menu;
    }
    /// <summary>
    /// ポーズからゲーム画面へ
    /// </summary>
    private void MenuToPlay()
    {
        print("menuから戻るよ");
        GameObjTrueFalse(_gamePlayObjs,_menuObjs);
        GameObjTrueFalse(_gameObjs,_menuObjs);
        _state=UIState.gamePlay;
    }
    /// <summary>
    /// タイトルかポーズから設定画面へ
    /// </summary>
    private void TitleOrMenuToSetting() {
        _prevState = _state;
        if (_prevState == UIState.title) {
            GameObjTrueFalse(_settingObjs, _titleObjs);
        } else if (_prevState == UIState.menu) {
            GameObjTrueFalse(_settingObjs, _menuObjs);
        }
        _state = UIState.setting;
    }
    /// <summary>
    /// 設定画面からタイトルかポーズへ
    /// </summary>
    private void SettingToTitleOrMenu() {
        if (_prevState == UIState.title) {
            GameObjTrueFalse(_titleObjs,_settingObjs);
        } else if (_prevState == UIState.menu) {
            GameObjTrueFalse(_menuObjs, _settingObjs);
        }
        _state = _prevState;
    }
    public void PlayToED() {
        GameObjTrueFalse(_endingObjs, _gamePlayObjs);
        GameObjTrueFalse(_endingObjs, _gameObjs);
        _button.StartBGM();
        _spAnime.enabled=false;
        _state = UIState.ED;
    }
    public void EDToResult() {
        GameObjTrueFalse(_resultObjs, _endingObjs);
        _state = UIState.result;
    }
    /// <summary>
    /// 有効化と無効化のスイッチ
    /// 引数１：有効　引数２：無効
    /// </summary>
    /// <param name="trueObjects">有効化したいゲームオブジェクト配列</param>
    /// <param name="falseObjects">無効化したいゲームオブジェクト配列</param>
    private void GameObjTrueFalse(GameObject[] trueObjects, GameObject[] falseObjects)
    {
        foreach (GameObject obj in falseObjects) {
            obj.SetActive(false);
        }
        foreach (GameObject obj in trueObjects) {
            obj.SetActive(true);
        }
       
    }
    /// <summary>
    /// リザルトからタイトルへ
    /// </summary>
    public void MenuOrResultToStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        _state = UIState.title;
    }
    public string StateSet() {
        return _state.ToString();
    }
    public bool CanMove() {
        return _canMove;
    }
    public float GamePlayTime() {
        return _gamePlayTime;
    }
    #endregion
}