using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tpoint : MonoBehaviour {

    [SerializeField] private MoveGate _moveGate;
    [SerializeField] private float _delayTime;
    [SerializeField, Tag] private string _playerTag;

    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag(_playerTag)) {

            _moveGate.StartMovingWithDelay(_delayTime);
        }
    }
}
