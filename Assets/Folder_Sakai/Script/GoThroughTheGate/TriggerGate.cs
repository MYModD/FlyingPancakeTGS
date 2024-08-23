using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGate : MonoBehaviour
{
    #region ïœêî

    [SerializeField,Tag] private string _playerTag;
    [SerializeField] GoThroughTheGateManager _goThroughTheGateManager;
    [SerializeField] private ParticleSystem _particleSystem;

    private bool _justOne = true;
    #endregion
    #region ÉÅÉ\ÉbÉh

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
