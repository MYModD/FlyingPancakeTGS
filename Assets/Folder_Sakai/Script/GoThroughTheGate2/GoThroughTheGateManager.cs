using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoThroughTheGateManager : MonoBehaviour
{
    #region 変数
    
    [SerializeField] private List<GameObject> _gates;

    private int _score = 0;
    #endregion
    #region　メソッド

    public void GateActivation() {

        foreach (GameObject gate in _gates) {

            // 機体をアクティブ化
            gate.SetActive(true);
        }
    }

    public void ScoreAddition() {

        _score++;
    }
    #endregion
}
