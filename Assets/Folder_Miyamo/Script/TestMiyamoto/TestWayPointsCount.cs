using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class TestWayPointsCount : MonoBehaviour {
    [SerializeField, Header("waypoint�̃^�O")]
    [Tag]
    private string _wayPoint;

    [Header("waypoint��ʂ�����"),ReadOnly]
    [SerializeField]
    private  int _wayPointsCount;

    [SerializeField, Header("WayPoint�N���X")]
    private TestWaypointsRank _testWaypointsRank;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }


    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(_wayPoint)) {

            _wayPointsCount++;
            _testWaypointsRank.UpdatePlayerRank(this.gameObject, _wayPointsCount);

        }

    }
}
