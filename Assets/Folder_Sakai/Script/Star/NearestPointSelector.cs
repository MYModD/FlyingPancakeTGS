using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearestPointSelector : MonoBehaviour
{
    [SerializeField] private Transform[] _points; // 指定する地点をシリアライズフィールドで指定

    private void OnTriggerEnter(Collider other) {
        if (_points.Length == 0) {
            Debug.LogWarning("ポイントが指定されていません");
            return;
        }

        Transform nearestPoint = GetNearestPoint(other.transform.position);
        if (nearestPoint != null) {
            Debug.Log("最も近いポイント: " + nearestPoint.name);
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
