using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : MonoBehaviour
{
    [SerializeField] private ExplosionPoolManager _explosionPoolManager;
    [SerializeField, Tag] private string _missileTag;

    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag(_missileTag)) {

            _explosionPoolManager.StartExplosion(this.gameObject.transform);
            this.gameObject.SetActive(false);
        }
    }
}
