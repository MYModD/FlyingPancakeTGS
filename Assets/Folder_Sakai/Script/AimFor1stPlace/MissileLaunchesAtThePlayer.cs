using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLaunchesAtThePlayer : MonoBehaviour
{

    [SerializeField] private TestEnemyMissilePoolManger _enemyMissilePoolManger;
    [SerializeField, Tag] private string _playerTag;
    [SerializeField] private Transform _firePos;

    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag(_playerTag)) {

            print("”­ŽË");
            _enemyMissilePoolManger.EnemyFireMissile(_firePos);
        }
    }
}
