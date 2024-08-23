using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankManager : MonoBehaviour {
    [SerializeField] private GameObject _playerAircraft;
    [SerializeField] private List<GameObject> _aircraft; // レースに参加する車両リスト
    [SerializeField] private List<Transform> _waypoints; // ウェイポイントリスト

    private AircraftProgress _playerProgress;
    private List<AircraftProgress> _aircraftProgresses; // 各車両の進行状況を保持
    private int _previousPlayerRank = default;
    private int _playerRank = default;
    private int _waypointCount = default;
    private int _score = default;

    void Awake() {
        _waypointCount = _waypoints.Count;
    }

    void Start() {
        _playerProgress = _playerAircraft.GetComponent<AircraftProgress>();
        _aircraftProgresses = new List<AircraftProgress>();

        foreach (GameObject aircraft in _aircraft) {
            _aircraftProgresses.Add(aircraft.GetComponent<AircraftProgress>());
        }
    }

    private void Update() {

        int playerRank = CalculatePlayerRank();
    }

    private int CalculatePlayerRank() {
        int playerWaypointsPassed = _playerProgress.CurrentWaypoint();
        _previousPlayerRank = _playerRank; // 前回の順位を保持する
        _playerRank = 1;

            // 次のウェイポイントまでの距離が近いほど順位が上がる
            float playerDistanceToNextWaypoint = Vector3.Distance(_playerProgress.transform.position, _waypoints[playerWaypointsPassed].position);

            for (int i = 0; i < _aircraft.Count; i++) {
                if (_aircraft[i] == _playerAircraft) {
                    continue;
                }

                AircraftProgress otherProgress = _aircraftProgresses[i];

                if (otherProgress.CurrentWaypoint() > playerWaypointsPassed) {
                    // プレイヤーの順位を計算
                    _playerRank++;
                } else if (otherProgress.CurrentWaypoint() == playerWaypointsPassed) {

                    if (otherProgress.CurrentWaypoint() < _waypointCount) {
                        float otherDistanceToNextWaypoint = Vector3.Distance(otherProgress.transform.position, _waypoints[otherProgress.CurrentWaypoint()].position);

                        if (otherDistanceToNextWaypoint < playerDistanceToNextWaypoint) {
                            _playerRank++;
                        }

                    } else {
                        _playerRank++;
                    }
                }
            }

        if (_previousPlayerRank != _playerRank) {
            ScoreAddition();
        }
        return _playerRank;
    }

    private void ScoreAddition (){

        _score++;
    }


}
