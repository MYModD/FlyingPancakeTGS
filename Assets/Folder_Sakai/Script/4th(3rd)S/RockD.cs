using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockD : MonoBehaviour {
    [SerializeField, Tag] private string _tag;
    private Rigidbody _rigidbody;

    private void OnEnable() {

        // Rigidbodyの参照を取得
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag(_tag)) {
            this.gameObject.SetActive(false);
        }
    }

    // このメソッドで重力を有効化
    public void OnGravity() {
        if (_rigidbody != null) {
            _rigidbody.useGravity = true;  // 重力をオンにする
        }
    }
}
