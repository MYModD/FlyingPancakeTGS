using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissileStuckManager : MonoBehaviour
{
    [SerializeField, Header("スタック")]
    private MissileStuck[] _missileStucks;

    [SerializeField, Header("UI スクロールバー")]
    private Scrollbar[] _scrollbar;

    public int _testNum;

    private void Start() {
        for (int i = 0; i < _missileStucks.Length; i++) {

            _missileStucks[i]._isFired= true;

        }
    }


    private void Update() {
        if (Input.GetKeyDown(KeyCode.N)) {

            FireMissile();
        }
    }

    public void FireMissile() {

        for (int i = 0; i < _testNum; i++) {

            if (_missileStucks[i]._isFired) {

                //_missileStucks[i].Hit();
            }

        }

    }



}
