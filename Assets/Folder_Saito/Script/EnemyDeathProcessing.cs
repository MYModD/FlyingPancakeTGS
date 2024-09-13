using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathProcessing : MonoBehaviour {
    [SerializeField] private CountTheNumberOfDefeats _countTheNumberOfDefeats;
    [SerializeField, Tag] private string _missileTag;
    [SerializeField] private Planeee _planeee;
    [SerializeField] private Rigidbody _rigidbody;
    private ControllerBuruBuru _controller;
    private bool _isDeath = true;


    private void OnTriggerEnter(Collider other) {
        if (_controller == null) {
            _controller = ControllerBuruBuru.Instance;
        }
        if (other.gameObject.CompareTag(_missileTag) && _isDeath) {
            _planeee.hp = -100;
            _countTheNumberOfDefeats.AdditionOfNumberOfDefeats();
            _controller.StartVibration();
            _rigidbody.useGravity = true;
            this.gameObject.transform.parent = null;
        }
    }
}