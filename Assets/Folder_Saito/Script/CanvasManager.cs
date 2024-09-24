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
using TMPro;

public class CanvasManager : MonoBehaviour {
    #region 変数
    // ミニスコア表示用のTextMeshProUGUI配列
    [SerializeField] private TextMeshProUGUI[] _miniScore;

    // ステージ変更用のマネージャー
    [SerializeField, Header("ControllerSelectButton")] private ChengeStageManager _staChange;

    // コントローラーボタンの設定
    [SerializeField, Header("ControllerSelectButton")] private ControllerSelectButton _button;

    // タイトル画面の各種ボタン
    [Header("タイトルのボタン")]
    [SerializeField, Header("スタートボタン")] private Button _titleStart;
    [SerializeField, Header("ゲーム終了ボタン")] private Button _titleGameEnd;
    [SerializeField, Header("設定ボタン")] private Button _titleSetting;

    // ゲーム内で表示するオブジェクトのグループ
    [SerializeField, Header("フィクションのオブジェクト")] private GameObject[] _fiction;
    [SerializeField, Header("タイトルのオブジェクト")] private GameObject[] _titleObjs;
    [SerializeField, Header("カウントダウンのオブジェクト")] private GameObject[] _countObjs;
    [SerializeField, Header("一時停止のオブジェクト")] private GameObject[] _menuObjs;
    [SerializeField, Header("ゲームUIのオブジェクト")] private GameObject[] _gamePlayObjs;
    [SerializeField, Header("リザルトのオブジェクト")] private GameObject[] _resultObjs;
    [SerializeField, Header("設定画面のオブジェクト")] private GameObject[] _settingObjs;
    [SerializeField, Header("オープニングのオブジェクト")] private GameObject[] _openingObjs;
    [SerializeField, Header("エンディングのオブジェクト")] private GameObject[] _endingObjs;
    [SerializeField, Header("ゲームプレイ中に使うオブジェクト")] private GameObject[] _gameObjs;
    [SerializeField] private GameObject[] _none;  // オブジェクトが無い場合の処理用
    [SerializeField] private SplineAnimate _spAnime;

    // タグに基づいてシーン遷移を制御するための設定
    [SerializeField, Header("タイトルに行かせたいタグ"), Tag] private string _tagTitle;
    [SerializeField, Header("ゲームに行かせたいタグ"), Tag] private string _tagGame;
    [SerializeField, Header("ゲームに戻すタグ"), Tag] private string _tagBackGame;
    [SerializeField, Header("設定に行かせたいタグ"), Tag] private string _tagSetting;
    [SerializeField, Header("ゲーム終了させたいタグ"), Tag] private string _tagFinish;

    // リザルト画面の管理
    [SerializeField] private ResultManager _resultManager;

    // ゲームプレイ時間とタイトル画面での放置時間のカウント
    private float _gamePlayTime;

    // タイトル画面での自動遷移用タイマー設定
    [SerializeField, Header("何秒操作がなかったら動画を流すか")]
    private float _titleTime;
    private float _nowTitleTime;  // タイトルでの経過時間カウント

    // 動画再生用のオブジェクトとBGM制御
    [SerializeField] private GameObject _videoPlay;
    [SerializeField] private AudioSource _audioBGM;
    [SerializeField] private float _videotime;  // 動画再生時間

    // 各種状態を管理するためのフラグ
    private bool _isStartPush = true;

    // UIの状態を管理するための列挙体
    private enum UIState {
        title,       // タイトル画面
        gamePlay,    // ゲームプレイ中
        result,      // リザルト画面
        menu,        // メニュー（ポーズ）画面
        setting,     // 設定画面
        OP,          // オープニング画面
        ED,          // エンディング画面
        Movie,       // ムービー再生中
        Fiction,     // フィクションシーン
    }

    // 現在と前回のUI状態
    private UIState _state;
    private UIState _prevState;

    // 移動可能かどうかを管理するフラグ
    private bool _canMove = false;
    #endregion
    #region プロパティ
    #endregion
    #region メソッド
    /// <summary>
    /// 更新前処理
    /// </summary>
    void Start()
    {
        //フィクション流してタイトルに行かせるから変更
        _state = UIState.Fiction;
    }
    /// <summary>
    /// 更新処理
    /// </summary>
    void Update() {
        //マウス同時に押すとシーンをリロード
        if ((Input.GetMouseButton(0)&&Input.GetMouseButton(1))||(Input.GetMouseButton(1)&&Input.GetMouseButton(0))) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        //フィクションの時
        if (_state == UIState.Fiction) {
            //まだ処理はない
        }
        //タイトルの時
        if (_state == UIState.title) {
            //タイトルにいる時間の計測
            _nowTitleTime += Time.deltaTime;
            //規定値を超えたら
            if (_titleTime <= _nowTitleTime) {
                //ステータスをムービーに
                _state = UIState.Movie;
                //タイトルオブジェクトを無効化
                GameObjTrueFalse(_none, _titleObjs);
                //BGMを止める
                _audioBGM.Stop();
                //ビデオ再生
                _videoPlay.SetActive(true);
                //時間をリセット
                _nowTitleTime = 0;
            }
        }
        //ゲーム中の時
        if (_state == UIState.gamePlay) {
            //プレイ時間加算
            _gamePlayTime += Time.deltaTime;
            //メニューに行かせるのは一旦無くす

            //if (Input.GetKeyDown("joystick button 7")&&_isStartPush) {
            //    PlayToMenu();
            //    _isStartPush = false;
            //}
            //if (Input.GetKeyUp("joystick button 7")) {
            //    _isStartPush = true;
            //}
            _canMove = true;
        }
        //処理書いてるけど今は使ってない

        //if (_state == UIState.result) {
        //    if (Input.GetButtonDown("Cancel") && _isStartPush) {
        //        MenuOrResultToStart();
        //    }
        //    _resultManager.SetTexts();
        //    _canMove = false;
        //}
        //if (_state == UIState.menu) {
        //    if (Input.GetKeyDown("joystick button 7") && _isStartPush) {
        //        MenuToPlay();
        //        _isStartPush=false;
        //    }
        //    if (Input.GetKeyUp("joystick button 7")) {
        //        _isStartPush = true;
        //    }
        //    _canMove=false;
        //}
        if (_state == UIState.setting) {
            //Bボタンで戻る
            if (Input.GetButtonDown("Cancel")) {
                SettingToTitleOrMenu();
            }
        }
        //
        if (_state == UIState.Movie) {
            _nowTitleTime += Time.deltaTime;
            if (_nowTitleTime >= _videotime||Input.anyKeyDown) {
                _state = UIState.Fiction;
                GameObjTrueFalse(_fiction,_none);
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
    public void FictionAnimEnd() {
        _state = UIState.title;
        GameObjTrueFalse( _titleObjs,_fiction);
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
        foreach (TextMeshProUGUI item in _miniScore) {
            item.enabled = false;
        }
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