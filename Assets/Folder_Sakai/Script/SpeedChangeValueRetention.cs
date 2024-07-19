using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedChangeValueRetention : MonoBehaviour
{
    [SerializeField, Tag] private string _player;
    [SerializeField] private EnemyMove _enemyMove;
    [SerializeField] private float _changeValue;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag(_player)) {

            _enemyMove.ChangeSpeed(_changeValue);
        }
    }
}
