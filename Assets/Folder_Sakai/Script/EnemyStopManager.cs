using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStopManager : MonoBehaviour {

    [SerializeField] private GameObject[] _stopBattleships;
    [SerializeField] private GameObject[] _stopEliteEnemies;
    [SerializeField] private CanvasManager _canvasManager;
    private bool _isTheStateCanMove;
    private float _stopSpeed = 0f;
    private float _moveSpeedBattleship = 100f;
    private float _moveSpeedEliteEnemy = 0.02f;
    private bool _canMove = true;
    private bool _prevCanMove = true;

    private void Update() {

        _canMove = _canvasManager.CanMove();

        if (_canMove != _prevCanMove) {
            print("“ü‚Á‚½");
            print(_canMove);
            EnemyMovementOrStopped(_canMove);
        }
        _prevCanMove = _canMove;
    }

    private void EnemyMovementOrStopped(bool isTheStateStopped) {
        _isTheStateCanMove = isTheStateStopped;
        BattleshipMovementOrStopped();
        EliteEnemyMovementOrStopped();
    }

    private void BattleshipMovementOrStopped() {

        foreach (GameObject obj in _stopBattleships) {

            BattleshipController battleshipController = obj.GetComponent<BattleshipController>();
            if (battleshipController != null) {

                if (_isTheStateCanMove) {
                    battleshipController.ChageSpeed(_moveSpeedBattleship);
                } else {
                    battleshipController.ChageSpeed(_stopSpeed);
                }
            }
        }

    }

    private void EliteEnemyMovementOrStopped() {

        foreach (GameObject obj in _stopEliteEnemies) {

            EliteEnemyMoveSpline eliteEnemyMoveSpline = obj.GetComponent<EliteEnemyMoveSpline>();
            if (eliteEnemyMoveSpline != null) {

                if (_isTheStateCanMove) {
                    eliteEnemyMoveSpline.ChageSpeed(_moveSpeedEliteEnemy);
                } else {
                    eliteEnemyMoveSpline.ChageSpeed(_stopSpeed);
                }
            }
        }

    }
}
