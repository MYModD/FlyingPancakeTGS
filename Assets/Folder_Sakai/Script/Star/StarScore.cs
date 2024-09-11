using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarScore : MonoBehaviour
{
    #region �ϐ�
    [SerializeField] private int _score;
    [SerializeField, Tag] private string _playerTag;
    [SerializeField] StarScoreManager _starScoreManager;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private AudioClip _audioClip;

    private bool _justOne = true; 
    #endregion
    #region ���\�b�h

    private void OnTriggerEnter(Collider other) {
        if (_starScoreManager == null) {
            _starScoreManager = StarScoreManager.Instance;
        }
        if (other.gameObject.CompareTag(_playerTag)) {

            if (_justOne) {
                _particleSystem.Play();
                _starScoreManager.ScoreAddition(_score,_audioClip);
                print(_score);
                _justOne = false;
            }

        }
    }
    #endregion
}
