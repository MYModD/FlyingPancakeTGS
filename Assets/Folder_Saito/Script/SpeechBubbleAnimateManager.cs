// ---------------------------------------------------------
// SpeechBubbleAnimateManager.cs
//
// 作成日:
// 作成者:
// ---------------------------------------------------------
using UnityEngine;
using System.Collections;
using UnityEngine.Splines;
using TMPro;
public class SpeechBubbleAnimateManager : MonoBehaviour {
    [SerializeField, Header("スプラインを通るオブジェクト")] private SplineAnimate _splineAnimate1;
    [SerializeField, Header("スプラインを通るオブジェクト")] private SplineAnimate _splineAnimate2;
    [SerializeField, Header("スプラインを通るオブジェクト")] private SplineAnimate _splineAnimate3;
    [SerializeField, Header("スプラインを通るオブジェクト")] private SplineAnimate _splineAnimate4;
    [SerializeField, Header("スプラインを通るオブジェクト")] private SplineAnimate _splineAnimate5;

    [SerializeField, Header("1stの吹き出し")] private GameObject _1stBubble;
    [SerializeField, Header("2ndの吹き出し")] private GameObject _2ndBubble;
    [SerializeField, Header("3ndの吹き出し")] private GameObject _3rdBubble;
    [SerializeField, Header("4thの吹き出し")] private GameObject _4thBubble;

    [Header("スコア表示テキスト")]
    [SerializeField] private TextMeshProUGUI _stageTitle;
    [SerializeField] private TextMeshProUGUI _stageScore;
    [SerializeField] private TextMeshProUGUI _time;
    [SerializeField] private TextMeshProUGUI _nowtime;

    private Color _color;

    private bool _is1st = true;
    private bool _is2nd = true;
    private bool _is3rd = true;
    private bool _is4th = true;

    #region 変数
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
        if (_splineAnimate1.enabled ) {
            if (!_is1st) {
                return;
            }
            StartCoroutine(StartBubble(1,0.1f));
            _is1st = false;
        }
        else if (_splineAnimate2.enabled && _is2nd) {
            if (!_is2nd) {
                return;
            }
            _1stBubble.SetActive(false);
            StartCoroutine(StartBubble(2,1.5f));
            _is2nd |= false;
        }
        else if (_splineAnimate3.enabled && _is3rd) {
            if (!_is3rd) {
                return;
            }
            _2ndBubble.SetActive(false);
            StartCoroutine(StartBubble(3,1.5f));
            _is3rd |= false;
        }else if (_splineAnimate4.enabled && _is4th) {
            if (!_is4th) {
                return;
            }
            _3rdBubble.SetActive(false);
            StartCoroutine(StartBubble(4, 1.5f));
            _is3rd |= false;
        } else {
                StartCoroutine(StartBubble(0, 0f));
            }
       
    }
    IEnumerator StartBubble(int index,float time) {
        yield return new WaitForSeconds(time);
        switch (index) {
            case 1:
                _1stBubble.SetActive(true);
                _color = Color.white;
                break;
            case 2:
                _2ndBubble.SetActive(true);
                _color = Color.black;
                break;
            case 3:
                _3rdBubble.SetActive(true);
                _color = Color.black;
                break;
            case 4:
                _4thBubble.SetActive(true);
                _color= Color.white;
                break;
            default:
                _1stBubble.SetActive(false);
                _2ndBubble.SetActive(false);
                _3rdBubble.SetActive(false);
                _4thBubble.SetActive(false);
                break;
        }
        _time.color = _color;
        _stageScore.color = _color;
        _stageTitle.color = _color;
        _nowtime.color = _color;
    }
    #endregion
}