// ---------------------------------------------------------
// ChengeStageManager.cs
//
// 作成日:
// 作成者:
// ---------------------------------------------------------
using UnityEngine;
using UnityEngine.Splines;
public class ChengeStageManager : MonoBehaviour {
    #region 変数
    private int _indexStage = 1;

    [Header("1stStage")]
    [SerializeField, Header("1stのプレイヤー")] private GameObject _player1st;
    [SerializeField] private SplineAnimate _splineAnimate1st;
    [SerializeField, Header("1stのステージ")] private GameObject _game1st;

    [Header("2stStage")]
    [SerializeField, Header("2stのプレイヤー")] private GameObject _player2st;
    [SerializeField] private SplineAnimate _splineAnimate2st;
    [SerializeField, Header("2stのステージ")] private GameObject _game2st;

    [Header("3stStage")]
    [SerializeField, Header("3stのプレイヤー")] private GameObject _player3st;
    [SerializeField] private SplineAnimate _splineAnimate3st;
    [SerializeField, Header("3stのステージ")] private GameObject _game3st;

    [Header("4stStage")]
    [SerializeField, Header("4stのプレイヤー")] private GameObject _player4st;
    [SerializeField] private SplineAnimate _splineAnimate4st;
    [SerializeField, Header("4stのステージ")] private GameObject _game4st;

    [Header("5stStage")]
    [SerializeField, Header("5stのプレイヤー")] private GameObject _player5st;
    [SerializeField] private SplineAnimate _splineAnimate5st;
    [SerializeField, Header("5stのステージ")] private GameObject _game5st;

    [SerializeField, Header("EDのCamera")] private GameObject _camera;
    [SerializeField,Header("CanvasManager")] private CanvasManager _canvasManager;
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
            _game4st.SetActive(true);
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
        _indexStage++;
    }
    public void StageStart() {
        if (_indexStage == 2) {
            print("ステチェン");
            _splineAnimate1st.enabled = false;
            _splineAnimate2st.enabled = true;
        }
        if (_indexStage == 3) {
            _splineAnimate2st.enabled = false;
            _splineAnimate3st.enabled = true;
        }
        if (_indexStage == 4) {
            _splineAnimate3st.enabled = false;
            _splineAnimate4st.enabled = true;
        }
        if (_indexStage == 5) {
            _splineAnimate4st.enabled = false;
            _splineAnimate5st.enabled = true;
        }

    }
    #endregion
}