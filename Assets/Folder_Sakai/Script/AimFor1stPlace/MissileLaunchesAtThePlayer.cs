using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class MissileLaunchesAtThePlayer : MonoBehaviour
{

    [SerializeField] private TestEnemyMissilePoolManger _enemyMissilePoolManger;
    [SerializeField, Tag] private string _playerTag;
    [SerializeField] private Transform[] _firesPos;

    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag(_playerTag)) {

            foreach (Transform firePos in _firesPos) {

                print("”­ŽË");
                _enemyMissilePoolManger.EnemyFireMissile(firePos);
            }
        }
    }
}
