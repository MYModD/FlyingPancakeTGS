using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathProcessing : MonoBehaviour
{
    [SerializeField] private CountTheNumberOfDefeats _countTheNumberOfDefeats;
    [SerializeField, Tag] private string _missileTag;
    [SerializeField] private Plane _plane;
    [SerializeField] private Rigidbody _rigidbody;

    private void Update() {

        if (Input.GetKeyDown(KeyCode.A)) {
            print("-100");
            _plane.hp = -100;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.CompareTag(_missileTag)) {

            _countTheNumberOfDefeats.AdditionOfNumberOfDefeats();
            _rigidbody.useGravity = true;
            this.gameObject.transform.parent = null;
        }
    }
}
