using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class TestWayPointsCount : MonoBehaviour {
    [Tag, Header("waypoint�̃^�O")]
    [SerializeField]
    private string _wayPoint;

    [Header("waypoint��ʂ�����"),ReadOnly]
    [SerializeField]
    private  int _wayPointsCount;

    [SerializeField, Header("���ʃN���X�}�l�[�W���[")]
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
