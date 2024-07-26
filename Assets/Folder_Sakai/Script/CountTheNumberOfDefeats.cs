using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountTheNumberOfDefeats : MonoBehaviour
{
    private int _countTheNumberOfDefeats = 0;

    public void AdditionOfNumberOfDefeats() {

        _countTheNumberOfDefeats++;
    }

    public int NumberOfDefeats() {

        return _countTheNumberOfDefeats;
    }
}
