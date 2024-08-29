using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class TestWayPointsCount : MonoBehaviour {
    [Tag, Header("waypointのタグ")]
    [SerializeField]
    private string _wayPoint;

    [Header("waypointを通った回数"),ReadOnly]
    [SerializeField]
    private  int _wayPointsCount;

    [SerializeField, Header("順位クラスマネージャー")]
    private PlayerRankManager _playerRankManager;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }


    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(_wayPoint)) {

            _wayPointsCount++;
            _playerRankManager.UpdatePlayerWaypointCount(this.gameObject, _wayPointsCount);

        }

    }
}
