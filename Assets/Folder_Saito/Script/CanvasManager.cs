// ---------------------------------------------------------
// CanvasManager.cs
//
// 作成日:7月後半
// 作成者:G2A118齊藤大志
//このスクリプトは他のスクリプトとの密接な関係です
//ControllerSelectButtonやら何やらまで
//UIのメインシステムかつこのゲームの命
// ---------------------------------------------------------
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Splines;
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
    [SerializeField, Header("ゲームプレイ中に使うオブジェクト")] private GameObject[] _tutorialObjs;
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
        Tutorial,    //チュートリアル
    }

    // 現在と前回のUI状態
    private UIState _state = UIState.Fiction;
    private UIState _prevState;

    // 移動可能かどうかを管理するフラグ
    private bool _canMove = false;
    #endregion
    #region プロパティ
    #endregion
    #region メソッド
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
        //動画中の処理
        if (_state == UIState.Movie) {
            //時間加算
            _nowTitleTime += Time.deltaTime;
            //既定の時間を超えるか何かボタン押されたら
            if (_nowTitleTime >= _videotime||Input.anyKeyDown) {
                //最初に戻す
                _state = UIState.Fiction;
                //フィクションのオブジェクトを有効化
                GameObjTrueFalse(_fiction,_none);
                //動画のオブジェクトを無効化
                _videoPlay.SetActive(false);
                //時間のリセット
                _nowTitleTime = 0;
            }

        }
    }
    #region ゲームフローの根幹
    //フィクション👉オープニング👉カウントダウン👉ゲーム中👉エンディング👉リザルト
    //外部から呼び出しているものが多いです。
    //ゲーム中にこれを持ったオブジェクト一個しかないからシングルトンでもよかったかも


    /// <summary>
    /// フィクションからタイトルに行かせる
    /// </summary>
    public void FictionToTitle() {
        //ステータス：タイトルに
        _state = UIState.title;
        GameObjTrueFalse( _titleObjs,_fiction);
    }

    /// <summary>
    /// タイトルからオープニングへ
    /// </summary>
    private void TitleToOP() {
        GameObjTrueFalse(_openingObjs, _titleObjs);
        //ステータス：オープニング
        _state = UIState.OP;
    }
    public void OPToTutorial() {
        GameObjTrueFalse(_tutorialObjs, _openingObjs);
        _state=UIState.Tutorial;
    }
    private void TutorialToCount() {
        GameObjTrueFalse(_countObjs, _tutorialObjs);
    }

    /// <summary>
    /// オープニングからカウントダウンに遷移
    /// </summary>
    public void OPtoCount() {
        GameObjTrueFalse(_countObjs, _openingObjs);
        ///////////////////////////////////////////
    }

    /// <summary>
    /// オープニング（カウントダウン状態）からゲーム画面へ
    /// </summary>
    public void OPToGamePlay() {
        GameObjTrueFalse(_gamePlayObjs, _openingObjs);
        GameObjTrueFalse(_gameObjs, _openingObjs);
        //スプラインアニメートを有効化
        //初期で無効化しとかないとエディター上で動く
        _spAnime.enabled = true;
        //ステータス：ゲーム中に
        _state = UIState.gamePlay;
        //ステージごとのリクエストボイスを流す
        _staChange.GameStartVoice();
    }

    /// <summary>
    /// ゲームプレイからエンディングへ
    /// </summary>
    public void PlayToED() {
        GameObjTrueFalse(_endingObjs, _gamePlayObjs);
        GameObjTrueFalse(_endingObjs, _gameObjs);
        //エンディング用のBGM鳴らしてもらう
        _button.StartBGM();
        //スプラインアニメートを無効化
        _spAnime.enabled = false;
        //ステータス：エンディングに
        _state = UIState.ED;
        //各ステージに出るリザルトを無効化
        //もしかしたら使ってないかも
        foreach (TextMeshProUGUI item in _miniScore) {
            item.enabled = false;
        }
    }

    /// <summary>
    /// エンディングからリザルト
    /// </summary>
    public void EDToResult() {
        GameObjTrueFalse(_resultObjs, _endingObjs);
        //ステータス：リザルトへ
        _state = UIState.result;
    }
    /// <summary>
    /// リザルトからタイトルへ
    /// </summary>
    public void MenuOrResultToStart() {
        //リロードしてゲームのリセットを行う
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //もしかしたらやらなくていいかも
        _state = UIState.Fiction;
    }

    #endregion
    #region 開発段階で使われなくなった物たち
    /// <summary>
    /// タイトルからゲーム画面へ
    /// </summary>
    private void TitleToGamePlay()
    {

        //OPToGamePlay()とTitleToOP()に仕事とられた

        GameObjTrueFalse(_gamePlayObjs, _titleObjs);
        GameObjTrueFalse(_gameObjs, _titleObjs);
        //ステータス：ゲーム中に
        _state = UIState.gamePlay;
    }
    /// <summary>
    /// ゲーム画面からポーズへ
    /// </summary>
    private void PlayToMenu() {
        //最初とステージ構成変わったから一旦無視
        GameObjTrueFalse(_menuObjs, _gamePlayObjs);
        GameObjTrueFalse(_menuObjs, _gameObjs);
        //ステータス：ポーズに
        _state = UIState.menu;
    }
    /// <summary>
    /// ポーズからゲーム画面へ
    /// </summary>
    private void MenuToPlay() {
        // PlayToMenu()と同じく一旦無視
        print("menuから戻るよ");
        GameObjTrueFalse(_gamePlayObjs, _menuObjs);
        GameObjTrueFalse(_gameObjs, _menuObjs);
        //ステータス：ゲーム中に
        _state = UIState.gamePlay;
    }
    #endregion
    #region 設定関係
    /// <summary>
    /// タイトルかポーズから設定画面へ
    /// </summary>
    private void TitleOrMenuToSetting() {
        //タイトルかポーズかを区別するために前のステータスを保存
        _prevState = _state;
        //前ステータスがタイトルの場合
        if (_prevState == UIState.title) {
            GameObjTrueFalse(_settingObjs, _titleObjs);
        }
        //前ステータスがポーズの場合
        else if (_prevState == UIState.menu) {
            GameObjTrueFalse(_settingObjs, _menuObjs);
        }
        //ステータス：設定に
        _state = UIState.setting;
    }
    /// <summary>
    /// 設定画面からタイトルかポーズへ
    /// </summary>
    private void SettingToTitleOrMenu() {
        //前のステータスがタイトルの場合
        if (_prevState == UIState.title) {
            GameObjTrueFalse(_titleObjs, _settingObjs);
        }
        //前のステータスがポーズの場合
        else if (_prevState == UIState.menu) {
            GameObjTrueFalse(_menuObjs, _settingObjs);
        }
        //ステータスを更新
        _state = _prevState;
    }
    #endregion
    #region その他system
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
    /// 押された時の処理を決める
    /// </summary>
    public void OnClickSw(string tagname) {
        if (tagname == _tagGame) {
            //タイトルからゲームへ
            TitleToOP();
        } else if (tagname == _tagSetting) {
            //タイトルorポーズから設定へ
            TitleOrMenuToSetting();
        } else if (tagname == _tagFinish) {
            //ゲームを終わらせる
            GameFinish();
        } else if (tagname == _tagTitle) {
            //タイトルに戻る
            MenuOrResultToStart();
        } else if (tagname == _tagBackGame) {
            //タイトルに戻る
            //多分冗長な書き方
            MenuOrResultToStart();
        }

    }

    /// <summary>
    /// 有効化と無効化のスイッチ
    /// 引数１：>有効化したいゲームオブジェクト配列　引数２：無効化したいゲームオブジェクト配列
    /// </summary>
    /// <param name="trueObjects">有効化したいゲームオブジェクト配列</param>
    /// <param name="falseObjects">無効化したいゲームオブジェクト配列</param>
    private void GameObjTrueFalse(GameObject[] trueObjects, GameObject[] falseObjects)
    {
        //無効化
        foreach (GameObject obj in falseObjects) {
            obj.SetActive(false);
        }
        //有効化
        foreach (GameObject obj in trueObjects) {
            obj.SetActive(true);
        }
       
    }
    #endregion
    #region 返り値あり
    /// <summary>
    /// ステータス共有
    /// </summary>
    /// <returns>現在のステータスをstring型で返す</returns>
    public string StateSet() {
        return _state.ToString();
    }
    /// <summary>
    /// 動いていいかのスイッチ共有
    /// </summary>
    /// <returns>trueなら動いて、falseなら止める</returns>
    public bool CanMove() {
        return _canMove;
    }
    /// <summary>
    /// ゲームプレイ時間を共有
    /// </summary>
    /// <returns>現在のプレイ時間を送る</returns>
    public float GamePlayTime() {
        return _gamePlayTime;
    }
    #endregion
#endregion
}