using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StarScoreManager : MonoBehaviour {
    #region ïœêî
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private TextMeshProUGUI _textTitle;
    [SerializeField] private TextMeshProUGUI _textScore;
    private int _score;
    private int _maxStarUSA = 50;
    #endregion
    #region ÉÅÉ\ÉbÉh
    public void ScoreAddition(int score) {

        _score += score;
        _scoreManager.InputRingScore(_score, _maxStarUSA);
        _textTitle.text = "Star Count";
        _textScore.text = _score.ToString();
    }
    #endregion
}
