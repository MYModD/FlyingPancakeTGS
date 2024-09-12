using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarScore : MonoBehaviour
{
    #region 変数
    [SerializeField] private int _score;
    [SerializeField, Tag] private string _playerTag;
    [SerializeField] StarScoreManager _starScoreManager;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private MTAppearanceManagement _mTAppearanceManagement;

    private bool _justOne = true; 
    #endregion
    #region メソッド

    private void OnTriggerEnter(Collider other) {

        print("トリガー");
        if (_starScoreManager == null) {
            _starScoreManager = StarScoreManager.Instance;
        }
        if (other.gameObject.CompareTag(_playerTag)) {

            if (_justOne) {
                _particleSystem.Play();
                _starScoreManager.ScoreAddition(_score);

                if (_score >= 1) {
                    print(_score);
                    _mTAppearanceManagement.MTSpawn(_score);

                } else if (_score <= -1) {
                    print(_score);
                    _mTAppearanceManagement.MTReduce(_score);
                }
                _justOne = false;
            }

        }
    }
    #endregion
}
