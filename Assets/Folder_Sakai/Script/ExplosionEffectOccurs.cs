using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffectOccurs : MonoBehaviour
{
    [SerializeField, Tag] private string _bulletTag;
    [SerializeField] private GameObject _explosion;
    private EnemyMissile _enemyMissile;

    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag(_bulletTag)) {

            _enemyMissile = other.gameObject.GetComponent<EnemyMissile>();
            _explosion.SetActive(true);
            //_enemyMissile.SetActiveFalse();  àÍíUÉRÉÅÉìÉg
            this.gameObject.transform.parent = null;
        }
    }
}
