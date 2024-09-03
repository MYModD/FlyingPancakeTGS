using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathTreatment : MonoBehaviour {
    [SerializeField, Tag] private string _missileTag;
    [SerializeField] private Planeee _planeee;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Vector3 _customGravity = new Vector3(0, -50f, 0);  // �J�X�^���d��

    private bool _isGravity = false;

    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag(_missileTag)) {

            _planeee.hp = -100;
            this.gameObject.transform.parent = null;
            _rigidbody.constraints = RigidbodyConstraints.None;
            _isGravity = true;

            // 5�b��ɔ�A�N�e�B�u�����郁�\�b�h���Ăяo��
            Invoke(nameof(Deactivate), 5f);
        }
    }
    void FixedUpdate() {

        if (!_isGravity) return;

        // ���t���[���Ǝ��̏d�͂�K�p
        _rigidbody.AddForce(_customGravity, ForceMode.Acceleration);
    }


    private void Deactivate() {
        this.gameObject.SetActive(false);
    }
}
