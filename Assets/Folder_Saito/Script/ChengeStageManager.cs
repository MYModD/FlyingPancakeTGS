// ---------------------------------------------------------
// ChengeStageManager.cs
//
// 作成日:9月中旬
// 作成者:G2A118齊藤大志
//ステージクリアしたときのカットインの最中に呼ばれるスクリプト
//この書き方は非常によくない
// ---------------------------------------------------------
using System.Collections;
using UnityEngine;
using UnityEngine.Splines;

public class ChengeStageManager : MonoBehaviour {
    #region 変数
    [SerializeField,Header("オーディエンスゲージ")] private AudienceGaugeManager _gaugeManager;
    [SerializeField, Header("効果音再生用のAudioSource")] private AudioSource _audioSE;
    [SerializeField, Header("ステージごとのボイスクリップを格納する配列")] private AudioClip[] _stageVoice;

    // 現在のステージを示すインデックス (0からスタート)
    private int _indexStage = 0;

    // 1stステージに関する設定
    [Header("1stStage")]
    // 1stステージのプレイヤーオブジェクト
    [SerializeField, Header("1stのプレイヤー")] private GameObject _player1st;
    // 1stステージのSplineアニメーション
    [SerializeField,Header("1stステージのSplineアニメーション")] private SplineAnimate _splineAnimate1st;
    // 1stステージのステージオブジェクト
    [SerializeField, Header("1stのステージ")] private GameObject _game1st;
    // 1stステージでのRingを集計するスクリプト
    [SerializeField, Header("ringを集計するスクリプト")] private GoThroughTheGateManager _ringCount;

    // 2ndステージに関する設定
    [Header("2stStage")]
    [SerializeField, Header("2stのプレイヤー")] private GameObject _player2st;
    [SerializeField, Header("2stステージのSplineアニメーション")] private SplineAnimate _splineAnimate2st;
    [SerializeField, Header("2stのステージ")] private GameObject _game2st;
    [SerializeField, Header("killを集計するスクリプト")] private CountTheNumberOfDefeats _numberOfDefeats;

    // 3rdステージに関する設定
    [Header("3stStage")]
    [SerializeField, Header("4stのプレイヤー")] private GameObject _player3st;
    [SerializeField, Header("4stステージのSplineアニメーション")] private SplineAnimate _splineAnimate3st;
    [SerializeField, Header("stのステージ")] private GameObject _game3st;
    [SerializeField, Header("killを集計するスクリプト")] private TimeLimit _3rdTime;
    [SerializeField]
    private GameObject _gamePad;
    [SerializeField] private PizzaCoinCount _pizzaCoinCount;

    // 4thステージに関する設定
     [Header("4stStage")]
    [SerializeField, Header("4stのプレイヤー")] private GameObject _player4st;
    [SerializeField] private SplineAnimate _splineAnimate4st;
    [SerializeField, Header("4stのステージ")] private GameObject _game4st;

    // 5thステージに関する設定
    [Header("5stStage")]
    [SerializeField, Header("5stのプレイヤー")] private GameObject _player5st;
    [SerializeField] private SplineAnimate _splineAnimate5st;
    [SerializeField, Header("5stのステージ")] private GameObject _game5st;

    // エンディング用のカメラ
    [SerializeField, Header("EDのCamera")] private GameObject _camera;

    // ゲーム全体のUIを管理するCanvasManager
    [SerializeField, Header("CanvasManager")] private CanvasManager _canvasManager;

    [SerializeField]
    private PlayerMove _playerMove;
    [SerializeField]
    private CameraRotate _cameraRotate;
    [SerializeField]
    private GameObject _title;
    [SerializeField]
    private GameObject _score;

    // ゲーム終了フラグ
    private bool _isFinish = false;
    #endregion

    #region プロパティ
    // (ここにプロパティを定義する場合は、このセクションに記載)
    #endregion

    #region メソッド

    /// <summary>
    /// ステージを切り替える処理
    /// アニメーションで最初に呼ばれる
    /// 仕様変更でステージの進む順番が１👉２👉４👉３になってるから注意
    /// </summary>
    public void StageChange() {
        // 1stステージから2ndステージへの切り替え
        if (_indexStage == 1) {
            _game1st.SetActive(false); // 1stステージを非表示
            if (_game2st != null) {
                _game2st.SetActive(true); // 2ndステージを表示
            }
            //セカンドがないとき
            else {
                FinishStage();
            }
        }

        // 2ndステージから4thステージへの切り替え
        if (_indexStage == 2) {
            _game2st.SetActive(false);
            if (_game3st != null) {
                _game4st.SetActive(true); // 4thステージを表示
            }
            //4thがないとき
            else {
                FinishStage();
            }
        }

        // 4thステージから3rdステージへの切り替え
        if (_indexStage == 3) {
            _game4st.SetActive(false);
            if (_game4st != null) {
                _game3st.SetActive(true);//３ｒｄで使うオブジェクトの有効化
                _gamePad.SetActive(true);
                _pizzaCoinCount.enabled = true;//３ｒｄで使うスクリプト起動
            } 
            //３ｒｄがないとき
            else {
                FinishStage();
            }
        }

        // 4thステージから5thステージへの切り替え
        if (_indexStage == 4) {
            _game3st.SetActive(false);//３ｒｄで使っていたオブジェクトの無効化
            _pizzaCoinCount.enabled = false;//スクリプト無効化
            if (_game5st != null) {
                _game5st.SetActive(true); // 5thステージを表示
            }
            //５ステージがないとき
            else {
                FinishStage();
            }
        }
        //ミニリザルト用のカメラアングルにしてもらう
        _cameraRotate.IsMainGameSwitch();
        _playerMove.StartMoving();
        _gaugeManager.ResetValue();
    }

    private void FinishStage() {
        _isFinish = true;
        _canvasManager.PlayToED(); // エンディングへ
    }

    /// <summary>
    /// ステージの開始処理
    /// </summary>
    public void StageStart() {
        // 1stから2ndステージへ
        if (_indexStage == 1) {
            //スプラインの切り替え：無効化を先のほうが望ましい
            _splineAnimate1st.enabled = false;
            _splineAnimate2st.enabled = true;
            _ringCount.enabled = false; // 1stステージの集計を無効化
            _numberOfDefeats.enabled = true; // 2ndステージの集計を有効化
        }

        // 2ndから3rdステージへ
        if (_indexStage == 2) {
            //スプラインの切り替え：無効化を先のほうが望ましい
            _splineAnimate2st.enabled = false;
            _splineAnimate4st.enabled = true;
            _numberOfDefeats.enabled = false; // 2ndステージの集計を無効化
        }

        // 3rdから4thステージへ
        if (_indexStage == 3) {
            //スプラインの切り替え：無効化を先のほうが望ましい
            _splineAnimate4st.enabled = false;
            _splineAnimate3st.enabled = true;
            _gamePad.SetActive(true);
            _3rdTime.LimitTimerStart(); // 3rdステージのタイマー開始
            _title.SetActive(false);
            _score.SetActive(false);
        }

        // 4thから5thステージへ
        if (_indexStage == 4) {
            //スプラインの切り替え：無効化を先のほうが望ましい
            _splineAnimate3st.enabled = false;
            _splineAnimate5st.enabled = true;
        }

        // ステージ開始時のボイス再生を開始
        StartCoroutine(StartVoice());
    }

    /// <summary>
    /// ステージのボイスを再生するコルーチン
    /// </summary>
    IEnumerator StartVoice() {
        // 少し待ってからボイスを再生
        yield return new WaitForSeconds(0.75f);
        if (!_isFinish) {
            //時間ないからこれで音量上げる
            _audioSE.PlayOneShot(_stageVoice[_indexStage]); // ボイスを再生
            _audioSE.PlayOneShot(_stageVoice[_indexStage]); // ボイスを再生
            _audioSE.PlayOneShot(_stageVoice[_indexStage]); // ボイスを再生
            _indexStage++; // ステージインデックスを進める
        }
    }

    /// <summary>
    /// ゲーム開始時にボイスを再生する
    /// </summary>
    public void GameStartVoice() {
        StartCoroutine(StartVoice());
    }
    #endregion
}
