// ---------------------------------------------------------
// ControllerBuruBuru.cs
//
// 作成日:
// 作成者:
// ---------------------------------------------------------
using UnityEngine;
using System.Collections;
using XInputDotNetPure;
public class ControllerBuruBuru : MonoBehaviour {
    #region 変数
    [SerializeField] float _rightPower = 1;
    [SerializeField] float _leftPower = 1;
    [SerializeField] float _duration = 0.5f;
    // シングルトンインスタンスを保持するための静的フィールド
    private static ControllerBuruBuru _instance;
    #endregion
    #region プロパティ
    public static ControllerBuruBuru Instance {
        get {
            // インスタンスがnullの場合、シーン内から探す
            if (_instance == null) {
                _instance = FindObjectOfType<ControllerBuruBuru>();
                if (_instance == null) {
                    Debug.LogError("StarScoreManagerのインスタンスがシーンに存在しません");
                }
            }
            return _instance;
        }
    }
    #endregion
    #region メソッド
    /// <summary>
    /// 初期化処理 使わないなら消す
    /// </summary>
    void Awake() {
        // インスタンスが既に存在している場合はこのオブジェクトを破棄
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        }
    }
    public void StartVibration() {
        StartCoroutine(Vibration());
    }
    public void StartLongVibration() {
        StartCoroutine(LBVibration());
    }
    public void CustomVibrationStart(float leftVibration,float rightVibration,float vibrationTime) {
        StartCoroutine(CustomVibration(leftVibration, rightVibration, vibrationTime));
    }
    #region 振動コルーチン
    private IEnumerator Vibration() {
        GamePad.SetVibration(0, _leftPower, _rightPower);
        yield return new WaitForSecondsRealtime(_duration);
        GamePad.SetVibration(0, 0, 0);
    }
    private IEnumerator LBVibration() {
        GamePad.SetVibration(0, _leftPower, _rightPower);
        yield return new WaitForSecondsRealtime(_duration*5);
        GamePad.SetVibration(0, 0, 0);
    }
    private IEnumerator CustomVibration(float leftVibration, float rightVibration, float vibrationTime) {
        GamePad.SetVibration(0, leftVibration, rightVibration);
        yield return new WaitForSecondsRealtime(vibrationTime);
        GamePad.SetVibration(0, 0, 0);
    }
    #endregion;
    #endregion
}