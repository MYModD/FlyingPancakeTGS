using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMT : MonoBehaviour {
    [SerializeField, Header("��]���x")]
    private float _rotationSpeed = 500f; // ��]���x

    [SerializeField, Header("��]������I�u�W�F�N�g")]
    private GameObject[] _objectsToRotate; // ��]������Ώۂ̃I�u�W�F�N�g

    // Update is called once per frame
    void Update() {
        foreach (GameObject obj in _objectsToRotate) {
            if (obj != null) {
                obj.transform.Rotate(Vector3.right * _rotationSpeed * Time.deltaTime);
            }
        }
    }
}
