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

    private float _nowPlayerScore = 0;
    private float _maxScore;
    private float _currentPlayScore = 0;
    private float _audiencePONPONValue = 0;
    private float _targetAudiencePONPONValue = 0; // 目標値
    private string _stagename;

    [SerializeField, Header("ゲージの変化速度")] private float _gaugeSpeed = 2f; // ゲージが変化する速度
    #endregion

    #region メソッド
    void Start() {
        _audienceGauge.value = 0;
        _audienceGauge.maxValue = _audienceMaxValue;
    }

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
            // 小数点第1位まで計算する
            _targetAudiencePONPONValue = Mathf.Round(_nowPlayerScore / _maxScore * 1000) / 10;
        } else {
            _targetAudiencePONPONValue = 0;
        }

        // ゲージを徐々に変化させる（Lerpを使用）
        _audiencePONPONValue = Mathf.Lerp(_audiencePONPONValue, _targetAudiencePONPONValue, _gaugeSpeed * Time.deltaTime);
        _audienceGauge.value = _audiencePONPONValue;
        _currentPlayScore = _nowPlayerScore;
    }

    /// <summary>
    /// ランク設定とその処理
    /// </summary>
    private void ChangeSliderValue() {
        if (_audiencePONPONValue >= 100) {
            _audience[4].SetActive(true);
            _fillImage.color = GetRainbowColor();
        } else if (_audiencePONPONValue >= _rankS) {
            _fillImage.color = GetRainbowColor();
            _rank.text = "S";
            _audience[3].SetActive(true);
            _audience[4].SetActive(false);
        } else if (_audiencePONPONValue >= _rankA) {
            _fillImage.color = Color.red;
            _rank.text = "A";
            _audience[2].SetActive(true);
            _audience[3].SetActive(false);
        } else if (_audiencePONPONValue >= _rankB) {
            _fillImage.color = Color.cyan;
            _rank.text = "B";
            _audience[1].SetActive(true);
            _audience[2].SetActive(false);
        } else if (_audiencePONPONValue >= _rankC) {
            _fillImage.color = Color.green;
            _rank.text = "C";
            _audience[0].SetActive(true);
            _audience[1].SetActive(false);
        } else {
            _fillImage.color = Color.gray;
            _rank.text = "D";
            foreach (GameObject obj in _audience) {
                obj.SetActive(false);
            }
        }
    }

    private void SetResult() {
        _stageTitle.text = _stagename;
        _stageScore.text = _nowPlayerScore + "/" + _maxScore;
    }

    private Color GetRainbowColor() {
        float t = Mathf.PingPong(Time.time, 1f);
        return Color.HSVToRGB(t, 1f, 1f);
    }

    public void SetScoreValue(int nowScore, int maxScoreStage, string stagename) {
        _nowPlayerScore = nowScore;
        _maxScore = maxScoreStage;
        _stagename = stagename;
    }

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
