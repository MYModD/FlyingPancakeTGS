// ---------------------------------------------------------
// ScoreManager.cs
//
// 作成日:
// 作成者:
// ---------------------------------------------------------
using UnityEngine;
using TMPro;

/// <summary>
/// ステージごとのスコアを計算し、ランクを決定して表示するクラス
/// </summary>
public class ScoreManager : MonoBehaviour {
    #region 変数
    [SerializeField, Header("1stStageのリザルト表示")] private TextMeshProUGUI _1stStage;
    [SerializeField, Header("2ndStageのリザルト表示")] private TextMeshProUGUI _2ndStage;
    [SerializeField, Header("3rdStageのリザルト表示")] private TextMeshProUGUI _3rdStage;
    [SerializeField, Header("4thStageのリザルト表示")] private TextMeshProUGUI _4thStage;
    [SerializeField, Header("EXStageのリザルト表示")] private TextMeshProUGUI _5thStage;
    [SerializeField, Header("クリアタイムの表示")] private TextMeshProUGUI _gameClearTime;
    [SerializeField, Header("ランクの表示")] private TextMeshProUGUI _rank;

    // 各ランクのしきい値（プレイヤーのスコアに基づいてランクを決定する）
    [SerializeField, Header("SSSランクのしきい値")] private float _rankS = 100;
    [SerializeField, Header("Sランクのしきい値")] private float _rankA = 85;
    [SerializeField, Header("Aランクのしきい値")] private float _rankB = 60;
    [SerializeField, Header("Bランクのしきい値")] private float _rankC = 40;
    [SerializeField, Header("Cランクのしきい値")] private float _rankD = 20;
    [SerializeField, Header("Dランクのしきい値")] private float _rankE = 10;
    [SerializeField, Header("Eランクのしきい値")] private float _rankF = 0;

    // スコア確定用の変数
    private float _ringMainScore;
    private float _killMainScore;
    private float _topMainScore;
    private float _starMainScore;
    private float _pizzaMainScore;
    private float _allStageMainScore;
    private float _resultScore;

    // スコア計算用の変数
    private int _maxRing;
    private int _passedRingCount;

    private int _killCount;
    private int _maxEnemy;

    private float _clearTime;
    private float _maxTime;
    private string _3rdTimeString;

    private int _getStarCount;
    private int _starNumUSA;

    private int _lastGetPizza;
    private int _maxPizza;

    private float _allStageClearTime;
    private float _limitTime; // 全ステージの制限時間
    private string _clearTimeString;

    private string _sorryText = "Sorry I can't do it yet";

    private bool _doRing = false;
    private bool _doKill = false;
    private bool _doTop = false;
    private bool _doStar = false;
    private bool _doPizza = false;
    #endregion

    #region メソッド
    /// <summary>
    /// スコア計算と表示を開始するメソッド
    /// </summary>
    public void StartResultProcess() {
        ResultSetText();
    }

    /// <summary>
    /// 各ステージのスコアを計算し、UIに表示するメソッド
    /// </summary>
    private void ResultSetText() {
        ScoreCalculation();
        if (_doRing) {
            _1stStage.text = ResultTextSet(_passedRingCount.ToString(), _maxRing.ToString());
        } else {
            _1stStage.text = _sorryText;
        }
        if (_doKill) {
            _2ndStage.text = ResultTextSet(_killCount.ToString(), _maxEnemy.ToString());
        } else {
            _2ndStage.text = _sorryText;
        }
        if (_doTop) {
            _3rdStage.text = _3rdTimeString;
        } else {
            _3rdStage.text = _sorryText;
        }
        if (_doStar) {
            _4thStage.text = ResultTextSet(_getStarCount.ToString(), _starNumUSA.ToString());
        } else {
            _4thStage.text = _sorryText;
        }
        //if (_doPizza) {
        //    _5thStage.text = ResultTextSet(_lastGetPizza.ToString(), _maxPizza.ToString());
        //} else {
        _5thStage.text = "";
        //}
        _gameClearTime.text = _clearTimeString;
        _rank.text = RankSettingProcess(_resultScore);
    }

    #region セットスコア情報
    /// <summary>
    /// リングのスコア情報をセットするメソッド
    /// </summary>
    public void InputRingScore(int passedRing, int maxRing) {
        _passedRingCount = passedRing;
        _maxRing = maxRing;
        _doRing = true;
    }

    /// <summary>
    /// 敵撃破のスコア情報をセットするメソッド
    /// </summary>
    public void InputEnemyKillScore(int killCount, int maxEnemy) {
        _killCount = killCount;
        _maxEnemy = maxEnemy;
        _doKill = true;
    }

    /// <summary>
    /// 1位になるためのタイムスコア情報をセットするメソッド
    /// </summary>
    public void InputToBeTheTopScore(float clearTime, float maxTime,string clearTimeText) {
        _clearTime = clearTime;
        _maxTime = maxTime;
        _3rdTimeString = clearTimeText;
        _doTop = true;
    }

    /// <summary>
    /// 星集めのスコア情報をセットするメソッド
    /// </summary>
    public void InputGetStarScore(int getStar, int starUSA) {
        _getStarCount = getStar;
        _starNumUSA = starUSA;
        _doStar = true;
    }

    /// <summary>
    /// ピザ収集のスコア情報をセットするメソッド
    /// </summary>
    public void InputPizzaScore(int lastPizzaCount, int maxPizza) {
        _lastGetPizza = lastPizzaCount;
        _maxPizza = maxPizza;
        _doPizza = true;
    }

    /// <summary>
    /// 全ステージのクリアタイムをセットするメソッド
    /// </summary>
    public void InputTimeScore(float clearTime, float limitTimeAll, string timeStr) {
        _allStageClearTime = clearTime;
        _limitTime = limitTimeAll;
        _clearTimeString = timeStr;
    }
    #endregion

    #region スコア計算
    /// <summary>
    /// 各ステージのスコアを計算し、最終スコアを算出するメソッド
    /// </summary>
    private void ScoreCalculation() {
        // リングのパーセントを計算
        _ringMainScore = PersentResult(_passedRingCount, _maxRing);
        // 敵撃破のパーセントを計算
        _killMainScore = PersentResult(_killCount, _maxEnemy);
        // 1位になるための時間のパーセントを計算
        _topMainScore = PersentResult(_clearTime, _maxTime);
        // 星集めのパーセントを計算
        _starMainScore = PersentResult(_getStarCount, _starNumUSA);
        // ピザ収集のパーセントを計算
        _pizzaMainScore = PersentResult(_lastGetPizza, _maxPizza);
        // 全ステージクリアタイムのパーセントを計算
        _allStageMainScore = PersentResult(_allStageClearTime, _limitTime);
        //分母求める
        int average = FindTheAverage();
        // 平均スコアを計算
        _resultScore = (_ringMainScore + _killMainScore + _topMainScore + _starMainScore + _pizzaMainScore + _allStageMainScore) / average;
    }
    private int FindTheAverage() {
        int average = 0;
        if (_doRing) {
            average++;
        }
        if (_doKill) {
            average++;
        }
        if (_doStar) {
            average++;
        }
        if (_doTop) {
            average++;
        }
        if (_doPizza) {
            average++;
        }
        return average;
    }
    /// <summary>
    /// プレイヤーの得点をもとにパーセントを計算するメソッド
    /// </summary>
    private float PersentResult(float playerPoint, float maxPoint) {
        if (maxPoint == 0) {
            return 0; // 0除算を回避
        }
        return Mathf.Round(playerPoint / maxPoint * 1000) / 10;
    }
    /// <summary>
    /// リザルト表示用のテキスト生成
    /// </summary>
    /// <param name="nowPlayScore">今回のその点数</param>
    /// <param name="maxScore">上限値</param>
    /// <returns>表示されるテキスト</returns>
    private string ResultTextSet(string nowPlayScore,string maxScore) {
        string setResult = nowPlayScore + "/" + maxScore;
        return setResult;
    }

    /// <summary>
    /// 計算されたスコアに基づいてランクを決定するメソッド
    /// </summary>
    private string RankSettingProcess(float score) {
        string rank = "E";

        if (score > _rankS) {
            rank = "SSS"; // 100％よりも上
        } else if (score > _rankA) {
            rank = "S";
        } else if (score > _rankB) {
            rank = "A";
        } else if (score > _rankC) {
            rank = "B";
        } else if (score > _rankD) {
            rank = "C";
        } else if (score > _rankE) {
            rank = "D";
        } else if (score >= _rankF) {
            rank = "E";
        }

        return rank;
    }
    #endregion
    #endregion
}
