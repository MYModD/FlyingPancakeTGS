using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoThroughTheGateManager : MonoBehaviour
{
    #region ïœêî
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private AudienceGaugeManager _gauge;
    [SerializeField] private TextMeshProUGUI _textTitle;
    [SerializeField] private TextMeshProUGUI _textScore;
    private int _score = 0;
    private int _maxScore = 90;
    #endregion
    #region ÉÅÉ\ÉbÉh

    public void ScoreAddition() {

        _score++;
        _scoreManager.InputRingScore(_score, _maxScore);
        _textTitle.text = "Ring Count";
        _textScore.text = _score.ToString() + " / "+_maxScore.ToString();
        _gauge.SetScoreValue(_score, _maxScore, "Ring Count");
    }
    #endregion
}
