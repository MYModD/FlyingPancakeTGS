using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StarScoreManager : MonoBehaviour {
    #region ïœêî
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private TextMeshProUGUI _textTitle;
    [SerializeField] private TextMeshProUGUI _textScore;
    [SerializeField] private TextMeshProUGUI _timeText1;
    [SerializeField] private TextMeshProUGUI _timeText2;
    private int _score;
    private int _maxStarUSA = 50;
    #endregion
    #region ÉÅÉ\ÉbÉh
    public void ScoreAddition(int score) {

        _score += score;
        _scoreManager.InputGetStarScore(_score, _maxStarUSA);
        _textScore.color = Color.white;
        _textTitle.color = Color.white;
        _timeText1.color = Color.white;
        _timeText2.color = Color.white;
        _textTitle.text = "Star Count";
        _textScore.text = _score.ToString();
    }
    #endregion
}
