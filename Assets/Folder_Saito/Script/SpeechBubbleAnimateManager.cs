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
using UnityEngine.UI;
public class SpeechBubbleAnimateManager : MonoBehaviour {
    [SerializeField, Header("スプラインを通るオブジェクト")] private SplineAnimate _splineAnimate1;
    [SerializeField, Header("スプラインを通るオブジェクト")] private SplineAnimate _splineAnimate2;
    [SerializeField, Header("スプラインを通るオブジェクト")] private SplineAnimate _splineAnimate3;
    [SerializeField, Header("スプラインを通るオブジェクト")] private SplineAnimate _splineAnimate4;
    [SerializeField, Header("スプラインを通るオブジェクト")] private SplineAnimate _splineAnimate5;

    [SerializeField, Header("1stの吹き出し")] private Animator _bubble;

    [Header("スコア表示テキスト")]
    [SerializeField] private TextMeshProUGUI _stageTitle;
    [SerializeField] private TextMeshProUGUI _stageScore;
    [SerializeField] private TextMeshProUGUI _time;
    [SerializeField] private TextMeshProUGUI _nowtime;

    [Header("0：日本語画像　1：英語画像")]
    [SerializeField] private Sprite[] _ring;
    [SerializeField] private Sprite[] _enemy;
    [SerializeField] private Sprite[] _monster;
    [SerializeField] private Sprite[] _pizza;

    [SerializeField,Header("画像出したいところ")] private Image _image;

    [SerializeField, Header("英語か日本語化のスイッチ")] private ControllerSelectButton _controller;

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
    /// 更新処理
    /// </summary>
    void Update() {
        //色の設定とスタートコルーチンの時間設定
        if (_splineAnimate1.enabled ) {
            if (!_is1st) {
                return;
            }
            StartCoroutine(StartBubble(1,0.1f));
            _is1st = false;
            _color = Color.white;
        }
        else if (_splineAnimate2.enabled && _is2nd) {
            if (!_is2nd) {
                return;
            }
            StartCoroutine(StartBubble(2,1.5f));
            _color = Color.black;
            _is2nd |= false;
        }
        else if (_splineAnimate3.enabled && _is3rd) {
            if (!_is3rd) {
                return;
            }
            StartCoroutine(StartBubble(3,1.5f));
            _color = Color.black;
            _is3rd |= false;
        }else if (_splineAnimate4.enabled && _is4th) {
            if (!_is4th) {
                return;
            }
            StartCoroutine(StartBubble(4, 1.5f));
            _is3rd |= false;
            _color = Color.white;
        } else {
                StartCoroutine(StartBubble(0, 0f));
            }
       
    }
    IEnumerator StartBubble(int index,float time) {
        yield return new WaitForSeconds(time);
        //ステージごとの吹き出しアニメーション
        switch (index) {
            case 1:
                _image.sprite = _ring[_controller.LanguageInversionCheak() ? 1 : 0];
                _bubble.Play("RingAnim");
                _bubble.Play("RingBig");
                _color = Color.white;
                print("RING!!!!");
                break;
            case 2:
                _image.sprite = _enemy[_controller.LanguageInversionCheak() ? 1 : 0];
                _bubble.Play("EnemyAnime");
                _bubble.Play("EnemyBig");
                _color = Color.black;
                break;
            case 3:
                _image.sprite = _monster[_controller.LanguageInversionCheak() ? 1 : 0];
                _bubble.Play("PizzaAnim");
                _bubble.Play("PizzaBig");
                _color = Color.black;
                break;
            case 4:
                _image.sprite = _pizza[_controller.LanguageInversionCheak() ? 1 : 0];
                _bubble.Play("MonsterAnim");
                _bubble.Play("Monster");
                _color = Color.white;
                break;
            default:
                break;
        }
        //ステージごとのテキスト色を設定
        _time.color = _color;
        _stageScore.color = _color;
        _stageTitle.color = _color;
        _nowtime.color = _color;
    }
    #endregion
}