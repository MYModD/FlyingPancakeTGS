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
public class CanvasManager : MonoBehaviour
{
    #region 変数
    [Header("タイトルのボタン")]
    [SerializeField, Header("スタートボタン")] private Button _titleStart;
    [SerializeField, Header("ゲーム終了ボタン")] private Button _titleGameEnd;
    [SerializeField, Header("設定ボタン")] private Button _titleSetting;

    [SerializeField, Header("タイトルのオブジェクト")] private GameObject[] _titleObjs;
    [SerializeField, Header("一時停止のオブジェクト")] private GameObject[] _menuObjs;
    [SerializeField, Header("ゲーム中のオブジェクト")] private GameObject[] _gamePlayObjs;
    [SerializeField, Header("リザルトのオブジェクト")] private GameObject[] _resultObjs;
    [SerializeField, Header("設定画面のオブジェクト")] private GameObject[] _settingObjs;

    [SerializeField, Header("タイトルに行かせたいタグ"),Tag] private string _tagTitle;
    [SerializeField, Header("ゲームに行かせたいタグ"), Tag] private string _tagGame;
    [SerializeField, Header("ゲームに戻すタグ"), Tag] private string _tagBackGame;
    [SerializeField, Header("設定に行かせたいタグ"),Tag] private string _tagSetting;
    [SerializeField, Header("ゲーム終了させたいタグ"), Tag] private string _tagFinish;

    public enum UIState {
        title,
        gamePlay,
        result,
        menu,
        setting
    }
    private UIState _state;
    private UIState _prevState;
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
    void Start()
    {
        _state = UIState.title;
    }
    /// <summary>
    /// 更新処理
    /// </summary>
    void Update() {
        if (_state == UIState.gamePlay) {
            if (Input.GetKeyDown(KeyCode.Z)) {
                PlayToResult();
            }
            if (Input.GetKeyDown(KeyCode.Q)) {
                PlayToMenu();
            }
        }
        if (_state == UIState.result) {
            if (Input.GetKeyDown(KeyCode.Return)) {
                MenuOrResultToStart();
            }
        }
        if (_state == UIState.menu) {
            if (Input.GetKeyDown(KeyCode.R)) {
                MenuToPlay();
            }
            if (Input.GetKeyDown(KeyCode.E)) {
                TitleOrMenuToSetting();
            }
        }
        if (_state == UIState.setting) {
            if (Input.GetKeyDown(KeyCode.T)) {
                SettingToTitleOrMenu();
            }
        }
    }
    /// <summary>
    /// 押された時の処理を決める
    /// </summary>
    public void OnClickSw(string tagname) {
        //タイトルからゲームへ
        if (tagname==_tagGame) {
            TitleToGamePlay();
        } else if (tagname==_tagSetting) {
            TitleOrMenuToSetting();
        } else if (tagname==_tagFinish) {
            GameFinish();
        }
        else if (tagname==_tagTitle) {
            MenuOrResultToStart();
        }
        else if (tagname == _tagBackGame) {
            MenuToPlay();
        }

    }
    /// <summary>
    /// タイトルからゲーム画面へ
    /// </summary>
    private void TitleToGamePlay()
    {
        GameObjTrueFalse(_gamePlayObjs, _titleObjs);
        _state = UIState.gamePlay;
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
        _state = UIState.menu;
    }
    /// <summary>
    /// ポーズからゲーム画面へ
    /// </summary>
    private void MenuToPlay()
    {
        GameObjTrueFalse(_gamePlayObjs,_menuObjs);
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
    private void PlayToResult() {
        GameObjTrueFalse(_resultObjs, _gamePlayObjs);
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
        foreach (GameObject obj in trueObjects) {
            obj.SetActive(true);
        }
        foreach (GameObject obj in falseObjects) {
        obj.SetActive(false);
        }
    }
    /// <summary>
    /// リザルトからタイトルへ
    /// </summary>
    private void MenuOrResultToStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        _state = UIState.title;
    }
    public string StateSet() {
        return _state.ToString();
    }
    #endregion
}