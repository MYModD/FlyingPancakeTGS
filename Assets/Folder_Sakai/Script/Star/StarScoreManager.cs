using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarScoreManager : MonoBehaviour
{
    #region •Ï”
    private int _score;
    #endregion
    #region ƒƒ\ƒbƒh
    public void ScoreAddition(int score) {

        _score += score;
    }
    #endregion
}
