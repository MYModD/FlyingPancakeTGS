using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearestPointSelector : MonoBehaviour
{
    [SerializeField] private Transform[] _points; // �w�肷��n�_���V���A���C�Y�t�B�[���h�Ŏw��

    private void OnTriggerEnter(Collider other) {
        if (_points.Length == 0) {
            Debug.LogWarning("�|�C���g���w�肳��Ă��܂���");
            return;
        }

        Transform nearestPoint = GetNearestPoint(other.transform.position);
        if (nearestPoint != null) {
            Debug.Log("�ł��߂��|�C���g: " + nearestPoint.name);
        }
    }

    private Transform GetNearestPoint(Vector3 position) {
        Transform nearest = null;
        float minDistance = Mathf.Infinity;

        foreach (Transform point in _points) {
            float distance = Vector3.Distance(position, point.position);
            if (distance < minDistance) {
                minDistance = distance;
                nearest = point;
            }
        }

        return nearest;
    }
}
