using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGate : MonoBehaviour
{
    #region �ϐ�

    [SerializeField,Tag] private string _playerTag;
    [SerializeField] GoThroughTheGateManager _goThroughTheGateManager;
    [SerializeField] private ParticleSystem _particleSystem;

    private bool _justOne = true;
    #endregion
    #region ���\�b�h

    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag(_playerTag)) {

            if (_justOne) {

                print("cil");
                _particleSystem.Play();
                _goThroughTheGateManager.ScoreAddition();
                _justOne = false;
            }
            
        }
    }
    #endregion
}
