using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGate : MonoBehaviour
{
    #region �ϐ�

    [SerializeField,Tag] private string _playerTag;
    [SerializeField] GoThroughTheGateManager _goThroughTheGateManager;
    #endregion
    #region ���\�b�h

    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag(_playerTag)) {

            _goThroughTheGateManager.ScoreAddition();
        }
    }
    #endregion
}
