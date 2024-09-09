using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGate : MonoBehaviour
{
    #region ïœêî

    [SerializeField,Tag] private string _playerTag;
    [SerializeField] GoThroughTheGateManager _goThroughTheGateManager;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private AudioSource _audioSE;
    [SerializeField] private AudioClip _ringPassedSE;

    private bool _justOne = true;
    #endregion
    #region ÉÅÉ\ÉbÉh

    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag(_playerTag)) {
            _particleSystem.Play();
            _audioSE.PlayOneShot(_ringPassedSE);
            if (_justOne) {
                _goThroughTheGateManager.ScoreAddition();
                _justOne = false;
            }
            
        }
    }
    #endregion
}
