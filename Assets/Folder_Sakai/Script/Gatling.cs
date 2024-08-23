using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gatling : MonoBehaviour
{
    [SerializeField] private GameObject[] _objectsToRotate; // ��]������I�u�W�F�N�g�̔z��
    [SerializeField] private float _rotationSpeed = 100f; // ��]���x

    void Update() {
        foreach (GameObject obj in _objectsToRotate) {
            // �e�I�u�W�F�N�g��Z������ɉ�]
            obj.transform.Rotate(Vector3.forward, _rotationSpeed * Time.deltaTime);
        }
    }
}
