using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMT : MonoBehaviour {
    [SerializeField, Header("回転速度")]
    private float _rotationSpeed = 500f; // 回転速度

    [SerializeField, Header("回転させるオブジェクト")]
    private GameObject[] _objectsToRotate; // 回転させる対象のオブジェクト

    // Update is called once per frame
    void Update() {
        foreach (GameObject obj in _objectsToRotate) {
            if (obj != null) {
                obj.transform.Rotate(Vector3.right * _rotationSpeed * Time.deltaTime);
            }
        }
    }
}
