using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankManager : MonoBehaviour {
    [SerializeField] private GameObject _playerAircraft;
    [SerializeField] private List<GameObject> _aircraft; // ���[�X�ɎQ������ԗ����X�g
    [SerializeField] private List<Transform> _waypoints; // �E�F�C�|�C���g���X�g

    private AircraftProgress _playerProgress;
    private List<AircraftProgress> _aircraftProgresses; // �e�ԗ��̐i�s�󋵂�ێ�
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
        _previousPlayerRank = _playerRank; // �O��̏��ʂ�ێ�����
        _playerRank = 1;

            // ���̃E�F�C�|�C���g�܂ł̋������߂��قǏ��ʂ��オ��
            float playerDistanceToNextWaypoint = Vector3.Distance(_playerProgress.transform.position, _waypoints[playerWaypointsPassed].position);

            for (int i = 0; i < _aircraft.Count; i++) {
                if (_aircraft[i] == _playerAircraft) {
                    continue;
                }

                AircraftProgress otherProgress = _aircraftProgresses[i];

                if (otherProgress.CurrentWaypoint() > playerWaypointsPassed) {
                    // �v���C���[�̏��ʂ��v�Z
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
