// ---------------------------------------------------------
// GaugeManager.cs
//
// 作成日:
// 作成者:
// ---------------------------------------------------------
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GaugeManager : MonoBehaviour {
    #region 変数
    [SerializeField,Header("ゲージ画像")]
    private Image _gaugeImage;
    [SerializeField,Header("最大値")] private float _maxGauge=100f;
    private float _currentValue;
    
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
        // 初期値を設定
        _currentValue = 0f;
        UpdateGauge();
        
    }
    /// <summary>
    /// 更新処理
    /// </summary>
    void Update() {
        if (Input.GetKey(KeyCode.Space)) {
            SetGaugeValue(1f);
        }
        if (Input.GetKeyDown(KeyCode.Return)) {
            SetPullGauge(33f);
        }
    }
    public void SetGaugeValue(float value) {
        _currentValue = Mathf.Clamp(value, 0, _maxGauge);
        UpdateGauge();
    }
    public void SetPullGauge(float index) {
        if (_gaugeImage.fillAmount <= index/_maxGauge) {
            return;
        }
        _gaugeImage.fillAmount -= index / _maxGauge;
    }
    private void UpdateGauge() {
        // 半円ゲージのフィル量を更新
        _gaugeImage.fillAmount += _currentValue / _maxGauge;
    }
    #endregion
}