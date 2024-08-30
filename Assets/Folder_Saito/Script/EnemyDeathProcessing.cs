using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathProcessing : MonoBehaviour
{
    [SerializeField] private CountTheNumberOfDefeats _countTheNumberOfDefeats;
    [SerializeField, Tag] private string _missileTag;

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.CompareTag(_missileTag)) {

            _countTheNumberOfDefeats.AdditionOfNumberOfDefeats();
        }
    }
}
