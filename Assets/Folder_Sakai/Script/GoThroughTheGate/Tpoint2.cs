using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tpoint2 : MonoBehaviour
{
    [SerializeField] private MoveGate2 _moveGate2;
    [SerializeField] private float _delayTime;
    [SerializeField, Tag] private string _playerTag;

    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag(_playerTag)) {

            _moveGate2.StartMovingWithDelay(_delayTime);
        }
    }
}
