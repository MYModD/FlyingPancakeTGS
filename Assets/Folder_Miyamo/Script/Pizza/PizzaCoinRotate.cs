using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaCoinRotate : MonoBehaviour {
    [SerializeField] private float _rotateYspeed = 100f;  // Y���̉�]���x

    void FixedUpdate() {
        // Y���ɉ����ăI�u�W�F�N�g����]������
        transform.Rotate(0f, _rotateYspeed * Time.fixedDeltaTime, 0f);
    }
}
