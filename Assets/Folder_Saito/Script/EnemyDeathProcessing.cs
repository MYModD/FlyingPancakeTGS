using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathProcessing : MonoBehaviour
{
    [SerializeField] private CountTheNumberOfDefeats _countTheNumberOfDefeats;
    [SerializeField, Tag] private string _missileTag;
    [SerializeField] private Planeee _planeee;
    [SerializeField] private Rigidbody _rigidbody;

    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag(_missileTag)) {

            _planeee.hp = -100;
            _countTheNumberOfDefeats.AdditionOfNumberOfDefeats();
            _rigidbody.useGravity = true;
            this.gameObject.transform.parent = null;
        }
    }
}
