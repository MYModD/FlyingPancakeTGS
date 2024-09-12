using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnable3rd : MonoBehaviour
{
    [SerializeField] private TestLockOnManager _testLockOnManager;
    [SerializeField] private float _changeConeRange;
    [SerializeField] private float _changeSearchRadius;
    [SerializeField] private GameObject _gameController;
    [SerializeField] private GameObject _gameController3rd;
    private void OnEnable() {

        _testLockOnManager.ChangeConeRange(_changeConeRange);
        _testLockOnManager.ChangeSearchRadius(_changeSearchRadius);
        _gameController.SetActive(false);
        _gameController3rd.SetActive(true);
    }
}
