using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountTheNumberOfDefeats : MonoBehaviour
{
    private int _countTheNumberOfDefeats = 0;
    [SerializeField] private TextMeshProUGUI _killCount;

    private void Update() {
        _killCount.text = _countTheNumberOfDefeats.ToString();
    }

    public void AdditionOfNumberOfDefeats() {

        _countTheNumberOfDefeats++;
    }

    public int NumberOfDefeats() {

        return _countTheNumberOfDefeats;
    }
}
