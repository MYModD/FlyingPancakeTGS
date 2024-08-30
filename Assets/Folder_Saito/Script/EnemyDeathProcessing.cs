using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathProcessing : MonoBehaviour
{
    [SerializeField] private CountTheNumberOfDefeats _countTheNumberOfDefeats;
    [SerializeField, Tag] private string _missileTag;
    [SerializeField] private Plane _plane;
    [SerializeField] private Rigidbody _rigidbody;
    private bool _isDeath=true;

    private void Update() {

        if (other.gameObject.CompareTag(_missileTag)&&_isDeath) {

            _countTheNumberOfDefeats.AdditionOfNumberOfDefeats();
            _rigidbody.useGravity = true;
            this.gameObject.transform.parent = null;
            this.GetComponent<BoxCollider>().enabled = false;
            _isDeath = false;
        }
    }
}
