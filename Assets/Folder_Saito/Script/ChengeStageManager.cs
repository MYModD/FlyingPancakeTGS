// ---------------------------------------------------------
// ChengeStageManager.cs
//
// 作成日:
// 作成者:
// ---------------------------------------------------------
using System.Collections;
using UnityEngine;
using UnityEngine.Splines;

public class ChengeStageManager : MonoBehaviour {
    #region 変数
    [SerializeField]
    private AudienceGaugeManager _gaugeManager;
    // 効果音再生用のAudioSource
    [SerializeField] private AudioSource _audioSE;

    // ステージごとのボイスクリップを格納する配列
    [SerializeField] private AudioClip[] _stageVoice;

    // 現在のステージを示すインデックス (0からスタート)
    private int _indexStage = 0;

    // 1stステージに関する設定
    [Header("1stStage")]
    // 1stステージのプレイヤーオブジェクト
    [SerializeField, Header("1stのプレイヤー")] private GameObject _player1st;
    // 1stステージのSplineアニメーション
    [SerializeField] private SplineAnimate _splineAnimate1st;
    // 1stステージのステージオブジェクト
    [SerializeField, Header("1stのステージ")] private GameObject _game1st;
    // 1stステージでのRingを集計するスクリプト
    [SerializeField, Header("ringを集計するスクリプト")] private GoThroughTheGateManager _ringCount;

    // 2ndステージに関する設定
    [Header("2stStage")]
    [SerializeField, Header("2stのプレイヤー")] private GameObject _player2st;
    [SerializeField] private SplineAnimate _splineAnimate2st;
    [SerializeField, Header("2stのステージ")] private GameObject _game2st;
    [SerializeField, Header("killを集計するスクリプト")] private CountTheNumberOfDefeats _numberOfDefeats;

    // 3rdステージに関する設定
    [Header("3stStage")]
    [SerializeField, Header("3stのプレイヤー")] private GameObject _player3st;
    [SerializeField] private SplineAnimate _splineAnimate3st;
    [SerializeField, Header("3stのステージ")] private GameObject _game3st;
    [SerializeField, Header("3stのMapMagic")] private GameObject _map3st;
    [SerializeField, Header("killを集計するスクリプト")] private TimeLimit _3rdTime;
    [SerializeField] private FogControl _fogControl;
    [SerializeField]
    private GameObject _gamePad;
    [SerializeField] private PizzaCoinCount _pizzaCoinCount;

    // 4thステージに関する設定
     [Header("4stStage")]
    [SerializeField, Header("4stのプレイヤー")] private GameObject _player4st;
    [SerializeField] private SplineAnimate _splineAnimate4st;
    [SerializeField, Header("4stのステージ")] private GameObject _game4st;
    //[SerializeField, Header("killを集計するスクリプト")] private StarScoreManager _star;

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

    // ゲーム終了フラグ
    private bool _isFinish = false;
    #endregion

    #region プロパティ
    // (ここにプロパティを定義する場合は、このセクションに記載)
    #endregion

    #region メソッド
    /// <summary>
    /// ゲーム開始前の初期化処理 (必要に応じて使用)
    /// </summary>
    void Awake() {
        // 初期化処理
    }

    /// <summary>
    /// ゲーム開始時に一度だけ呼ばれる処理
    /// </summary>
    void Start() {
        // ゲーム開始時の初期処理
    }

    /// <summary>
    /// 毎フレーム呼ばれる更新処理
    /// </summary>
    void Update() {
        // 毎フレーム行いたい処理
    }

    /// <summary>
    /// ステージを切り替える処理
    /// </summary>
    public void StageChange() {
        // 1stステージから2ndステージへの切り替え
        if (_indexStage == 1) {
            _game1st.SetActive(false); // 1stステージを非表示
            if (_game2st != null) {
                _game2st.SetActive(true); // 2ndステージを表示
            } else {
                _isFinish = true; // 全ステージクリア
                _canvasManager.PlayToED(); // エンディングへ
            }
        }

        // 2ndステージから4thステージへの切り替え
        if (_indexStage == 2) {
            _game2st.SetActive(false);
            if (_game3st != null) {
                _game4st.SetActive(true); // 4thステージを表示
            } else {
                _isFinish = true;
                _canvasManager.PlayToED();
            }
        }

        // 4thステージから3rdステージへの切り替え
        if (_indexStage == 3) {
            _game4st.SetActive(false);
            if (_game4st != null) {
                _game3st.SetActive(true);
                _map3st.SetActive(true); // 3rdステージのマップを有効化
                _gamePad.SetActive(true);
                _pizzaCoinCount.enabled = true;
                _fogControl.SetFog(true); // フォグを有効化
            } else {
                _isFinish = true;
                _canvasManager.PlayToED();
            }
        }

        // 4thステージから5thステージへの切り替え
        if (_indexStage == 4) {
            _game3st.SetActive(false);
            _map3st.SetActive(false); // マップを無効化
            _fogControl.SetFog(false); // フォグを無効化
            _pizzaCoinCount.enabled = false;
            if (_game5st != null) {
                _game5st.SetActive(true); // 5thステージを表示
            } else {
                _isFinish = true;
                _canvasManager.PlayToED();
            }
        }
        // 最終ステージ（5thステージ）の終了処理
        if (_indexStage == 5) {
            _camera.SetActive(true); // エンディング用カメラを有効化
            _game5st.SetActive(false);
            _isFinish = true;
            _canvasManager.PlayToED(); // エンディングへ
        }
        _cameraRotate.IsMainGameSwitch();
        _playerMove.StartMoving();
        _gaugeManager.ResetValue();
    }

    /// <summary>
    /// ステージの開始処理
    /// </summary>
    public void StageStart() {
        // 1stから2ndステージへ
        if (_indexStage == 1) {
            _splineAnimate1st.enabled = false;
            _splineAnimate2st.enabled = true;
            _ringCount.enabled = false; // 1stステージの集計を無効化
            _numberOfDefeats.enabled = true; // 2ndステージの集計を有効化
        }

        // 2ndから3rdステージへ
        if (_indexStage == 2) {
            _splineAnimate2st.enabled = false;
            _splineAnimate4st.enabled = true;
            _numberOfDefeats.enabled = false; // 2ndステージの集計を無効化
            //_star.enabled = true; // 4thステージのスクリプトを有効化
        }

        // 3rdから4thステージへ
        if (_indexStage == 3) {
            _splineAnimate4st.enabled = false;
            _splineAnimate3st.enabled = true;
            _gamePad.SetActive(true);
            _3rdTime.LimitTimerStart(); // 3rdステージのタイマー開始
        }

        // 4thから5thステージへ
        if (_indexStage == 4) {
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
