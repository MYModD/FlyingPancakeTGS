using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gatling : MonoBehaviour
{
    [SerializeField] private GameObject[] _objectsToRotate; // 回転させるオブジェクトの配列
    [SerializeField] private float _rotationSpeed = 100f; // 回転速度

    void Update() {
        foreach (GameObject obj in _objectsToRotate) {
            // 各オブジェクトをZ軸周りに回転
            obj.transform.Rotate(Vector3.forward, _rotationSpeed * Time.deltaTime);
        }
    }
}
