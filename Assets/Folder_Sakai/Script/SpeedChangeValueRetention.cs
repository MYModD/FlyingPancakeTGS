using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedChangeValueRetention : MonoBehaviour
{
    [SerializeField, Tag] private string _eliteEnemy;
    [SerializeField] private EnemyMove _enemyMove;
    //[SerializeField] private EliteEnemyIndex _eliteEnemyIndex;
    [SerializeField] private float _changeValue;
    [SerializeField] private bool _isItOnStandby;
    [SerializeField] private EliteEnemyManager _eliteEnemyManager;
    [SerializeField] private float _stopTime;

    private int _index = default;
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag(_eliteEnemy)) {

            EliteEnemyIndex eliteEnemyIndex = other.GetComponent<EliteEnemyIndex>();

            _index = eliteEnemyIndex.SetIndex();
            _enemyMove.ChangeSpeed(_changeValue);
            if (_isItOnStandby) {

                _eliteEnemyManager.Adsd(_index, _stopTime);
            }
        }
    }
}
