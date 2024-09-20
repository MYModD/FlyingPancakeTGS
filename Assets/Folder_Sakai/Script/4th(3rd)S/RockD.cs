using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockD : MonoBehaviour {
    [SerializeField, Tag] private string _tag;
    private Rigidbody _rigidbody;

    private void OnEnable() {

        // Rigidbody�̎Q�Ƃ��擾
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag(_tag)) {
            this.gameObject.SetActive(false);
        }
    }

    // ���̃��\�b�h�ŏd�͂�L����
    public void OnGravity() {
        if (_rigidbody != null) {
            _rigidbody.useGravity = true;  // �d�͂��I���ɂ���
        }
    }
}
