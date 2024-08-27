// ---------------------------------------------------------
// ScoreManager.cs
//
// 作成日:
// 作成者:
// ---------------------------------------------------------
using UnityEngine;
using System.Collections;
using Unity.Mathematics;
using System;
public class ScoreManager : MonoBehaviour {
    #region 変数
    private int _score;

    #region スコア確定保存
    private float _ringMainScore;
    private float _killMainScore;
    private float _topMainScore;
    private float _starMainScore;
    private float _pizzaMainScore;
    private float _allStageMainScore;
    private float _resultScore;
    #endregion
    #region　スコア計算用
    private int _maxRing;
    private int _passedRingCount;

    private int _killCount;
    private int _maxEnemy;

    private float _clearTime;
    private float _maxTime;

    private int _getStarCount;
    private int _starNumUSA;

    private int _lastGetPizza;
    private int _maxPizza;

    private float _allStageClearTime;
    private float _limitTime;//しきい値
    #endregion

    #endregion
    #region プロパティ
    #endregion
    #region メソッド
    public void UpdateScore(int addScore) {
        _score += addScore;
        //後でテキスト追加
    }
    #region セットスコア情報
    ///1stリング
    public void InputRingScore(int passedRing, int maxRing) {
        _passedRingCount = passedRing;
        _maxRing = maxRing;
    }
    ///2st撃破
    public void InputEnemyKillScore(int killCount, int maxEnemy) {
        _killCount = killCount;
        _maxEnemy = maxEnemy;
    }
    ///3st１位
    public void InputToBeTheTopScore(float clearTime, float maxTime) {
        _clearTime = clearTime;
        _maxTime = maxTime;
    }
    ///4st☆集め
    public void InputGetStarScore(int getStar, int starUSA) {
        _getStarCount = getStar;
        _starNumUSA = starUSA;
    }
    ///5stピザ
    public void InputPizzaScore(int lastPizzaCount,int maxPizza) {
        _lastGetPizza = lastPizzaCount;
        _maxPizza = maxPizza;
    }
    //Time
    public void InputTimeScore(float clearTime,float limitTimeAll) {
        _allStageClearTime = clearTime;
        _limitTime = limitTimeAll;
    }
    #endregion
    #region スコア計算
    private void ScoreCalculation() {
        //リングのパーセント
        _ringMainScore = PersentResult(_passedRingCount, _maxRing);
        //撃破のパーセント
        _killMainScore = PersentResult(_killCount, _maxEnemy);
        //１位になるための時間のパーセント
        _topMainScore = PersentResult(_clearTime, _maxTime);
        //☆集めのパーセント
        _starMainScore = PersentResult(_getStarCount, _starNumUSA);
        //ピザのパーセント
        _pizzaMainScore = PersentResult(_lastGetPizza, _maxPizza);
        //時間のパーセント
        _allStageMainScore = PersentResult(_allStageClearTime, _limitTime);
        //最終スコア
        _resultScore = (_ringMainScore + _killMainScore + _topMainScore + _starMainScore + _pizzaMainScore + _allStageMainScore)/6;
    }
    private float PersentResult(float playerPoint , float maxPoint) {
        return Mathf.Round(playerPoint / maxPoint * 1000)/10;
    }
    #endregion
    #endregion
}