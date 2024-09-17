using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearestPointSelector : MonoBehaviour {

    [SerializeField] private GameObject[] _points; // 指定する地点をシリアライズフィールドで指定
    [SerializeField, Tag] private string _f14Tag;

    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag(_f14Tag)) {


            if (_points.Length == 0) {
                Debug.LogWarning("ポイントが指定されていません");
                return;
            }

            GameObject nearestPoint = GetNearestPoint(other.transform.position);
            if (nearestPoint != null) {
                Debug.Log("最も近いポイント: " + nearestPoint.name);

                // ここでスクリプトを取得し、メソッドを実行する
                var targetScript = nearestPoint.GetComponent<StarScore>();
                if (targetScript != null) {
                    targetScript.ScoreJudgment(); // メソッドを実行
                } else {
                    Debug.LogWarning("TargetScriptが見つかりません");
                }
            }
        }

    }

    private GameObject GetNearestPoint(Vector3 position) {
        GameObject nearest = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject point in _points) {
            float distance = Vector3.Distance(position, point.transform.position);
            if (distance < minDistance) {
                minDistance = distance;
                nearest = point;
            }
        }

        return nearest;
    }
}
