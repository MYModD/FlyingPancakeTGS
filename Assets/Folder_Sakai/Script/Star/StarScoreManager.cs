using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarScoreManager : MonoBehaviour
{
    #region �ϐ�
    private int _score;
    #endregion
    #region ���\�b�h
    public void ScoreAddition(int score) {

        _score += score;
    }
    #endregion
}
