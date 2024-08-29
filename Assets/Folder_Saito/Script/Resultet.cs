using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resultet : MonoBehaviour {
    [SerializeField] private CanvasManager _canvas;

    [SerializeField,Header("CutInのアニメーター")] private Animator _animator;
    [SerializeField, Header("1stPlayerのタグ"), Tag] private string _playerTag;
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(_playerTag)) {
            _animator.Play("CutIN");
        }
    }
}
