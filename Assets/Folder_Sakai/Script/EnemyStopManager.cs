using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStopManager : MonoBehaviour {

    [SerializeField] private GameObject[] _stopObjs;
    bool _isTheStateStopped;
    private float _stopSpeed =0;
    private float _moveSpeed =100;

    public void EnemyMovementOrStopped(bool isTheStateStopped) {

        _isTheStateStopped = isTheStateStopped;

        foreach (GameObject obj in _stopObjs) {

            BattleshipController battleship = obj.GetComponent<BattleshipController>();
            if (battleship != null) {

                if (_isTheStateStopped) {

                    battleship.ChageSpeed(_stopSpeed);
                } else {

                    battleship.ChageSpeed(_moveSpeed);
                }
                
            }
        }

    }
}
