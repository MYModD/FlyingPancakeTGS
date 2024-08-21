using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedChangeValueRetention : MonoBehaviour
{
    [SerializeField, Tag] private string _eliteEnemy;
    [SerializeField] private float _changeSpeedValue;
    [SerializeField] private EliteEnemyMoveSpline _eliteEnemyMoveSpline;
    [SerializeField] private float _stopTime;

    private int _index = default;
    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag(_eliteEnemy)) {

            _eliteEnemyMoveSpline.ChageSpeed(_changeSpeedValue);
        }
    }
}
