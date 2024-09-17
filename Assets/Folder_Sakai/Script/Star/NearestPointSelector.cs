using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearestPointSelector : MonoBehaviour {

    [SerializeField] private GameObject[] _points; // �w�肷��n�_���V���A���C�Y�t�B�[���h�Ŏw��
    [SerializeField, Tag] private string _f14Tag;

    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag(_f14Tag)) {


            if (_points.Length == 0) {
                Debug.LogWarning("�|�C���g���w�肳��Ă��܂���");
                return;
            }

            GameObject nearestPoint = GetNearestPoint(other.transform.position);
            if (nearestPoint != null) {
                Debug.Log("�ł��߂��|�C���g: " + nearestPoint.name);

                // �����ŃX�N���v�g���擾���A���\�b�h�����s����
                var targetScript = nearestPoint.GetComponent<StarScore>();
                if (targetScript != null) {
                    targetScript.ScoreJudgment(); // ���\�b�h�����s
                } else {
                    Debug.LogWarning("TargetScript��������܂���");
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
