using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountTheNumberOfDefeats : MonoBehaviour
{
    [SerializeField] private ScoreManager _score;
    private int _countTheNumberOfDefeats = 0;
    private int _maxEnemy = 21;
    [SerializeField] private TextMeshProUGUI _killCount;
    [SerializeField] private TextMeshProUGUI _killTitle;

    [SerializeField] private AudienceGaugeManager _gauge;



    private void Update() {
        _killTitle.text = "Kill Count";
        _killCount.text = _countTheNumberOfDefeats.ToString();
        _gauge.SetScoreValue(_countTheNumberOfDefeats, _maxEnemy, "Kill Count");
    }

    public void AdditionOfNumberOfDefeats() {

        _countTheNumberOfDefeats++;
        _score.InputEnemyKillScore(_countTheNumberOfDefeats, _maxEnemy);
    }

    public int NumberOfDefeats() {

        return _countTheNumberOfDefeats;
    }
}
