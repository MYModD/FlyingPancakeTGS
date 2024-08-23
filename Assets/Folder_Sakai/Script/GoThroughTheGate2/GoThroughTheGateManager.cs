using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoThroughTheGateManager : MonoBehaviour
{
    #region �ϐ�
    
    [SerializeField] private List<GameObject> _gates;

    private int _score = 0;
    #endregion
    #region�@���\�b�h

    public void GateActivation() {

        foreach (GameObject gate in _gates) {

            // �@�̂��A�N�e�B�u��
            gate.SetActive(true);
        }
    }

    public void ScoreAddition() {

        _score++;
    }
    #endregion
}
