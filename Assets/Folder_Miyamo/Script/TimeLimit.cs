using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLimit : MonoBehaviour
{

    [Header("制限時間")]
    public float _limitTime = 60f;

    [SerializeField,Header("できたらreadonlyにしたい")]
    public bool _isStart = false;


    private float _firstMaxtime = default;
    // 後で直す


    // ゲームをスタートするとき実行する
    public void LimitTimerStart() {
        _isStart = true;
        _firstMaxtime = _limitTime;
    }

    // Update is called once per frame
    void Update()
    {
        // isStartがtrueのとき
        if (_isStart) {

            _limitTime -= Time.deltaTime;
            if (_limitTime <= 0f) {
                End3rdGame();
            }
        
        }
    }


    /// <summary>
    /// タイマーが0になったもしくは、1位になった敵が撃破されたら実行する
    /// </summary>
    public  void End3rdGame() {

        _isStart = false;
        float cashTime = _limitTime;
        if (cashTime <= 0) {
            cashTime = 0;
        }

        Debug.Log($"3rdマップのタイマーは  {cashTime}  でした!!");

        string floatTostring = ChangeTimeText(cashTime);
        Debug.LogWarning(floatTostring);

        GameObject scoreMangerObject = GameObject.Find("ScoreManager");
        scoreMangerObject.GetComponent<ScoreManager>().InputToBeTheTopScore(cashTime, _firstMaxtime,floatTostring);


    }




    #region 便利メソッドByさいとう

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


    #endregion
}
