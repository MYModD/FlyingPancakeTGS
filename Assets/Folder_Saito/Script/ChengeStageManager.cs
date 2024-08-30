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
    [SerializeField] private AudioSource _audioSE;
    [SerializeField] private AudioClip[] _stageVoice;
    private int _indexStage = 0;

    [Header("1stStage")]
    [SerializeField, Header("1stのプレイヤー")] private GameObject _player1st;
    [SerializeField] private SplineAnimate _splineAnimate1st;
    [SerializeField, Header("1stのステージ")] private GameObject _game1st;
    [SerializeField, Header("ringを集計するスクリプト")] private GoThroughTheGateManager _ringCount;

    [Header("2stStage")]
    [SerializeField, Header("2stのプレイヤー")] private GameObject _player2st;
    [SerializeField] private SplineAnimate _splineAnimate2st;
    [SerializeField, Header("2stのステージ")] private GameObject _game2st;
    [SerializeField,Header("killを集計するスクリプト")]private CountTheNumberOfDefeats _numberOfDefeats;

    [Header("3stStage")]
    [SerializeField, Header("3stのプレイヤー")] private GameObject _player3st;
    [SerializeField] private SplineAnimate _splineAnimate3st;
    [SerializeField, Header("3stのステージ")] private GameObject _game3st;
    [SerializeField, Header("killを集計するスクリプト")] private TimeLimit _3rdTime;
    [SerializeField, Header("killを集計するスクリプト")] private PlayerRankManager _3rdRank;

    [Header("4stStage")]
    [SerializeField, Header("4stのプレイヤー")] private GameObject _player4st;
    [SerializeField] private SplineAnimate _splineAnimate4st;
    [SerializeField, Header("4stのステージ")] private GameObject _game4st;
    [SerializeField, Header("killを集計するスクリプト")] private StarScoreManager _star;

    [Header("5stStage")]
    [SerializeField, Header("5stのプレイヤー")] private GameObject _player5st;
    [SerializeField] private SplineAnimate _splineAnimate5st;
    [SerializeField, Header("5stのステージ")] private GameObject _game5st;

    [SerializeField, Header("EDのCamera")] private GameObject _camera;

    [SerializeField, Header("CanvasManager")] private CanvasManager _canvasManager;


    [SerializeField]
    private TestLockOnManager _testLockOnManager;

    private bool _isFinish = false;

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
    }
    /// <summary>
    /// 更新処理
    /// </summary>
    void Update() {
    }
    public void StageChange() {
        if (_indexStage == 1) {
            
            _game1st.SetActive(false);
            _game2st.SetActive(true);
        }
        if (_indexStage == 2) {
            _game2st.SetActive(false);
            _game3st.SetActive(true);
        }
        if (_indexStage == 3) {
            _game3st.SetActive(false);
            _game4st.SetActive(true);
        }
        if (_indexStage == 4) {
            _game4st.SetActive(false);
            _game5st.SetActive(true);
        }
        if (_indexStage == 5) {
            _camera.SetActive(true);
            _game5st.SetActive(false);
            _canvasManager.PlayToED();
        }
        
    }
    
    public void StageStart() {
        if (_indexStage == 1) {
            print("ステチェン");
            _splineAnimate1st.enabled = false;
            _splineAnimate2st.enabled = true;
            _ringCount.enabled = false;
            _numberOfDefeats.enabled = true;

            _testLockOnManager._searchRadius = 2500f;
            _testLockOnManager._coneAngle = 84f*2f;
            _testLockOnManager._coneRange = 3000f;
        }
        if (_indexStage == 2) {
            _splineAnimate2st.enabled = false;
            _splineAnimate3st.enabled = true;
            _numberOfDefeats.enabled = false;
            _3rdTime.LimitTimerStart();
            _3rdRank.enabled = true;

            // ここから
            _testLockOnManager._searchRadius = 642f;
            _testLockOnManager._coneAngle = 84f;
            _testLockOnManager._coneRange = 341.8f;

        }
        if (_indexStage == 3) {
            _splineAnimate3st.enabled = false;
            _splineAnimate4st.enabled = true;
            _3rdRank.enabled = false;
            _star.enabled = true;

            // ここから

           
        }
        if (_indexStage == 4) {
            _splineAnimate4st.enabled = false;
            _splineAnimate5st.enabled = true;
        }
        StartCoroutine(StartVoice());
    }

    IEnumerator StartVoice() {
        yield return new WaitForSeconds(0.75f);
        _audioSE.PlayOneShot(_stageVoice[_indexStage]);
        _indexStage++;
    }
    public void GameStartVoice() {
        StartCoroutine(StartVoice());
    }
    #endregion
}