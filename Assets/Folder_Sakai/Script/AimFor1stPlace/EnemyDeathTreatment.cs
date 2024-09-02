using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathTreatment : MonoBehaviour {
    [SerializeField, Tag] private string _missileTag;
    [SerializeField] private Planeee _planeee;
    [SerializeField] private Rigidbody _rigidbody;

    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag(_missileTag)) {

            _planeee.hp = -100;
            _rigidbody.useGravity = true;
            this.gameObject.transform.parent = null;

            // 5秒後に非アクティブ化するメソッドを呼び出す
            Invoke(nameof(Deactivate), 5f);
        }
    }

    private void Deactivate() {
        this.gameObject.SetActive(false);
    }
}
