using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaCoinRotate : MonoBehaviour {
    [SerializeField] private float _rotateYspeed = 100f;  // Y軸の回転速度

    void FixedUpdate() {
        // Y軸に沿ってオブジェクトを回転させる
        transform.Rotate(0f, _rotateYspeed * Time.fixedDeltaTime, 0f);
    }
}
