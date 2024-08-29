using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarScore : MonoBehaviour
{
    #region ïœêî
    [SerializeField] private int _score;
    [SerializeField, Tag] private string _playerTag;
    [SerializeField] StarScoreManager _starScoreManager;
    [SerializeField] private ParticleSystem _particleSystem;

    private bool _justOne = true; 
    #endregion
    #region ÉÅÉ\ÉbÉh

    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag(_playerTag)) {

            if (_justOne) {
                _particleSystem.Play();
                _starScoreManager.ScoreAddition(_score);
                _justOne = false;
            }

        }
    }
    #endregion
}
