using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountTheNumberOfDefeats : MonoBehaviour
{
    private int _countTheNumberOfDefeats = 0;
    [SerializeField] private TextMeshProUGUI _killCount;
    [SerializeField] private TextMeshProUGUI _killTitle;

    private void Update() {
        _killTitle.text = "Kill Count";
        _killCount.text = _countTheNumberOfDefeats.ToString();
    }

    public void AdditionOfNumberOfDefeats() {

        _countTheNumberOfDefeats++;
    }

    public int NumberOfDefeats() {

        return _countTheNumberOfDefeats;
    }
}
