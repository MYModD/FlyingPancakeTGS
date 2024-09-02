// ---------------------------------------------------------
// SpeechBubbleAnimateManager.cs
//
// 作成日:
// 作成者:
// ---------------------------------------------------------
using UnityEngine;
using System.Collections;
using UnityEngine.Splines;
public class SpeechBubbleAnimateManager : MonoBehaviour {
    [SerializeField, Header("スプラインを通るオブジェクト")] private SplineAnimate _splineAnimate1;
    [SerializeField, Header("スプラインを通るオブジェクト")] private SplineAnimate _splineAnimate2;
    [SerializeField, Header("スプラインを通るオブジェクト")] private SplineAnimate _splineAnimate3;
    [SerializeField, Header("スプラインを通るオブジェクト")] private SplineAnimate _splineAnimate4;
    [SerializeField, Header("スプラインを通るオブジェクト")] private SplineAnimate _splineAnimate5;

    [SerializeField, Header("1stの吹き出し")] private GameObject _1stBubble;
    [SerializeField, Header("2ndの吹き出し")] private GameObject _2ndBubble;
    [SerializeField, Header("3ndの吹き出し")] private GameObject _3rdBubble;

    private bool _is1st = true;
    private bool _is2nd = true;
    private bool _is3rd = true;

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
        }
        else {
            StartCoroutine(StartBubble(0,0f));
        }
    }
    IEnumerator StartBubble(int index,float time) {
        yield return new WaitForSeconds(time);
        switch (index) {
            case 1:
                _1stBubble.SetActive(true);
                break;
            case 2:
                _2ndBubble.SetActive(true);
                break;
            case 3:
                _3rdBubble.SetActive(true);
                break;
            default:
                _1stBubble.SetActive(false);
                _2ndBubble.SetActive(false);
                _3rdBubble.SetActive(false);
                break;
        }
    }
    #endregion
}