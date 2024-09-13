// ---------------------------------------------------------
// AudienceGaugeManager.cs
//
// 作成日:
// 作成者:
// ---------------------------------------------------------
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
public class AudienceGaugeManager : MonoBehaviour {
    #region 変数
    [SerializeField, Header("オーディエンスPONPON")] private GameObject[] _audience;
    [Header("各ランク下限値％設定")]
    [SerializeField, Header("Sランク")] private int _rankS;
    [SerializeField, Header("Aランク")] private int _rankA;
    [SerializeField, Header("Bランク")] private int _rankB;
    [SerializeField, Header("Cランク")] private int _rankC;
    [SerializeField, Header("Dランク")] private int _rankD;

    [SerializeField, Header("オーディエンスゲージ")] private Image _fillImage;
    [SerializeField] private Slider _audienceGauge;

    [SerializeField] private TextMeshProUGUI _stageTitle;
    [SerializeField] private TextMeshProUGUI _stageScore;
    [SerializeField] private TextMeshProUGUI _rank;
    [SerializeField] private TextMeshProUGUI _rankTitle;
    private int _audienceMaxValue = 100;

    private float _nowPlayerScore=0;
    private float _maxScore;
    private float _currentPlayScore=0;
    private float _audiencePONPONValue=0;
    private string _stagename;

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
        _audienceGauge.value = 0;
        _audienceGauge.maxValue = _audienceMaxValue;
    }
    /// <summary>
    /// 更新処理
    /// </summary>
    void Update() {
        SetPercentValue();
        ChangeSliderValue();
        SetResult();
    }
    /// <summary>
    /// ゲージ変更
    /// </summary>
    private void SetPercentValue() {
        if (_currentPlayScore == _nowPlayerScore) {
            return;
        }
        if (_nowPlayerScore != 0) {
            print("socool");
            //小数点第1位まで計算する
            _audiencePONPONValue = Mathf.Round(_nowPlayerScore / _maxScore * 1000) / 10;
        }
        else {
            _audiencePONPONValue = 0;
        }
        print($"_audiencePONPONValue{_audiencePONPONValue}");
        _audienceGauge.value = _audiencePONPONValue;
        print($"_audienceGauge.value{_audienceGauge.value}");
        _currentPlayScore = _nowPlayerScore;
    }
    /// <summary>
    /// ランク設定とその処理
    /// </summary>
    private void ChangeSliderValue() {
        if (_audiencePONPONValue >= 100) {
            _audience[4].SetActive(true);
            _fillImage.color = GetRainbowColor();
        }
        else if (_audiencePONPONValue >= _rankS) {
            // Sランク処理 - 時間経過で虹色に変化
            _fillImage.color = GetRainbowColor();
            _rank.text = "S";
            _audience[3].SetActive(true);
            _audience[4].SetActive(false);
        } else if (_audiencePONPONValue >= _rankA) {
            // Aランク処理
            _fillImage.color = Color.red;
            _rank.text = "A";
            _audience[2].SetActive(true);
            _audience[3].SetActive(false);
        } else if (_audiencePONPONValue >= _rankB) {
            // Bランク処理
            _fillImage.color = Color.cyan;
            _rank.text = "B";
            _audience[1].SetActive(true);
            _audience[2].SetActive(false);
        } else if (_audiencePONPONValue >= _rankC) {
            // Cランク処理
            _fillImage.color = Color.green;
            _rank.text = "C";
            _audience[0].SetActive(true);
            _audience[1].SetActive (false);
        } else {
            // Dランク処理
            _fillImage.color = Color.gray;
            _rank.text = "D";
            foreach (GameObject obj in _audience) {
                obj.SetActive(false);
            }
        }
    }
    private void SetResult() {
        _stageTitle.text = _stagename;
        _stageScore.text = _nowPlayerScore +"/"+ _maxScore;
    }

    /// <summary>
    /// 虹色に変化するカラーを取得
    /// </summary>
    /// <returns>虹色のColor</returns>
    private Color GetRainbowColor() {
        // 時間に基づいて色相を変化させる
        float t = Mathf.PingPong(Time.time, 1f);
        return Color.HSVToRGB(t, 1f, 1f); // 彩度1、明度1の色相を利用
    }

    /// <summary>
    /// プレイヤーのスコアとマックスの値を記録
    /// </summary>
    /// <param name="nowScore">現在のスコア</param>
    /// <param name="maxScoreStage">マックス</param>
    public void SetScoreValue(int nowScore,int maxScoreStage,string stagename) {
        _nowPlayerScore = nowScore;
        _maxScore = maxScoreStage;
        _stagename = stagename;
        print($"マックスバリュー{_maxScore}今のスコアは{_nowPlayerScore}");
    }
    /// <summary>
    /// ステージ変更の時にリセット
    /// </summary>
    public void ResetValue() {
        _nowPlayerScore = 0;
        _maxScore = 0;
    }
    public void TextTrue(bool isSw) {
        _stageScore.enabled = isSw;
        _stageTitle.enabled = isSw;
        _rank.enabled = isSw;
        _rankTitle.enabled = isSw;
    }
    #endregion
}