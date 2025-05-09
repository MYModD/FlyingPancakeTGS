// ---------------------------------------------------------
// ResultManager.cs
//
// 作成日:
// 作成者:
// ---------------------------------------------------------
using UnityEngine;
using System.Collections;
using TMPro;
using System.Data.SqlTypes;
public class ResultManager : MonoBehaviour {
    #region 変数
    [SerializeField] private TextMeshProUGUI _clearTimeText;
    [SerializeField] private TextMeshProUGUI _gameTimeText;
    [SerializeField] private CanvasManager _canvasManager;
    [SerializeField,Header("ScoreManagerこのゲームオブジェクト")]private ScoreManager _scoreManager;

    [SerializeField, Header("クリア時間のしきい値")] private float _limitTime = 240f;

    private float _clearTime;

    
    //private bool _doSetText=false;
    #endregion
    #region プロパティ
    #endregion
    #region メソッド
    /// <summary>
    /// 更新処理
    /// </summary>
    void Update() {
        _gameTimeText.text = ChangeTimeText(_canvasManager.GamePlayTime());
        SetClearTime();
    }
    /// <summary>
    /// 秒数を分、秒、秒以下３桁の形にする
    /// </summary>
    /// <param name="secondTime">その形式にしたい秒数</param>
    /// <returns></returns>
    private string ChangeTimeText(float secondTime) {
        // 定数定義
        const int SECONDS_IN_MINUTE = 60;
        const int MILLISECONDS_IN_SECOND = 1000;
        const int TWO_DIGIT_THRESHOLD = 10;
        const int THREE_DIGIT_THRESHOLD = 100;

        // 時間が60秒未満の場合、そのまま小数点以下3桁までの秒数を返す
        if (secondTime < SECONDS_IN_MINUTE) {
            return secondTime.ToString("F3");
        }

        // 分の整数部分を計算
        int minuteTime = (int)(secondTime / SECONDS_IN_MINUTE);

        // 残りの秒数を計算
        float remainingSeconds = secondTime % SECONDS_IN_MINUTE;
        int seconds = (int)remainingSeconds;

        // 残りのミリ秒を計算
        int milliseconds = (int)((remainingSeconds - seconds) * MILLISECONDS_IN_SECOND);

        // 秒を2桁表示にフォーマット
        string secondText = seconds < TWO_DIGIT_THRESHOLD ? "0" + seconds.ToString() : seconds.ToString();

        // ミリ秒を3桁表示にフォーマット
        string millisecondText = milliseconds < THREE_DIGIT_THRESHOLD
            ? milliseconds < TWO_DIGIT_THRESHOLD
                ? "00" + milliseconds.ToString()
                : "0" + milliseconds.ToString()
            : milliseconds.ToString();

        // フォーマットされた時間文字列を返す
        return minuteTime.ToString() + ":" + secondText + ":" + millisecondText;
    }
    public void SetTexts() {
        SetClearTime();
        _scoreManager.StartResultProcess();
    }
    /// <summary>
    /// 各種データ渡し
    /// </summary>
    private void SetClearTime() {
        _clearTime = _canvasManager.GamePlayTime();
        _scoreManager.InputTimeScore(_clearTime, _limitTime, ChangeTimeText(_clearTime));
    }
    #endregion
}