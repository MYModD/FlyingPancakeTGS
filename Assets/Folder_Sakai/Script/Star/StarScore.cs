using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarScore : MonoBehaviour {
    #region 変数
    [SerializeField] private int _score;
    [SerializeField, Tag] private string _playerTag;
    [SerializeField] private StarScoreManager _starScoreManager;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private MTAppearanceManagement _mTAppearanceManagement;
    [SerializeField] private GameController3rd _gameController;
    [SerializeField] private bool _multiplication = false;
    [SerializeField] private bool _division = false;
    [SerializeField] private bool _missileStockIncrease = false;

    private bool _justOne = true;
    #endregion
    #region メソッド

    private void OnTriggerEnter(Collider other) {

        print("トリガー");
        if (_starScoreManager == null) {
            //_starScoreManager = StarScoreManager.Instance;
        }
        if (other.gameObject.CompareTag(_playerTag)) {

            if (_justOne) {
                _particleSystem.Play();
               // _starScoreManager.ScoreAddition(_score);

                if (_missileStockIncrease) {

                    _gameController.RoundsRemainingIncrease(_score);

                } else if (_multiplication) {

                    _mTAppearanceManagement.MultiplicationMTSpawn(_score);

                } else if (_division) {

                    _mTAppearanceManagement.DivisionMTReduce(_score);
                    print(_score);

                } else {

                    if (_score >= 1) {
                        print(_score);
                        _mTAppearanceManagement.MTSpawn(_score);

                    } else if (_score <= -1) {
                        print(_score);
                        _mTAppearanceManagement.MTReduce(_score);
                    }
                }

                _justOne = false;
            }

        }
    }
    #endregion
}
