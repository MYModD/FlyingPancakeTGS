using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using NaughtyAttributes;
using TMPro;

public class PlayerRankManager : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI _textTitle;
    [SerializeField] private TextMeshProUGUI _textScore;
    [SerializeField] private AudienceGaugeManager _gauge;

    [SerializeField, Header("�v���C���[�̃I�u�W�F�N�g")]
    private GameObject[] _playerObjects;

    [ReadOnly, Header("�v���C���[���Ƃ̃E�F�C�|�C���g���̊Ǘ�")]
    [SerializeField]
    private int[] _playerWaypointCounts;

    [ReadOnly, Header("�v���C���[�̏��ʊǗ�")]
    [SerializeField]
    private int[] _playerRanks;

    [SerializeField, Header("���݂̃v���C���[�I�u�W�F�N�g")]
    private GameObject _currentPlayerObject;

    [SerializeField, Header("���݂̃v���C���[�̏���")]
    private int _currentPlayerRank;

    [Range(0f, 0.1f), Header("���ʍX�V�̊Ԋu")]
    [SerializeField]
    private float _repeatTime;

    [SerializeField, Header("1�ʂɂȂ������ɔ������鎞�Ԑ���")]
    private TimeLimit _timeLimit;


    /// <summary>
    /// Start�֐��B����������
    /// </summary>
    private void Start() {

        _playerRanks = new int[_playerObjects.Length];
        _playerWaypointCounts = new int[_playerObjects.Length];
        // �v���C���[�ȊO�̃I�u�W�F�N�g�Ƀ����_���ȃE�F�C�|�C���g�������蓖�Ă�
        for (int i = 0; i < _playerWaypointCounts.Length; i++) {
            if (_playerObjects[i] != _currentPlayerObject) {
                _playerWaypointCounts[i] = UnityEngine.Random.Range(1, 10); // 1�`10�̃����_���Ȑ��l
            } else {
                _playerWaypointCounts[i] = 0; // �v���C���[��0�ɏ�����
            }
        }
    }

    public void UpdatePlayerWaypointCount(GameObject playerObject, int waypointCount) {
        int playerIndex = Array.IndexOf(_playerObjects, playerObject);

        if (playerIndex != -1) {
            _playerWaypointCounts[playerIndex] = waypointCount;
        }

    }

    private void Update() {
        
        

        int currentPlayerIndex = Array.IndexOf(_playerObjects, _currentPlayerObject);

        int[] sortedIndices = Enumerable.Range(0, _playerWaypointCounts.Length)
            .OrderByDescending(i => _playerWaypointCounts[i])
            .ToArray();

        for (int i = 0; i < sortedIndices.Length; i++) {
            _playerRanks[sortedIndices[i]] = i + 1;
        }

        _currentPlayerRank = _playerRanks[currentPlayerIndex];

        // -------------------------------------�ȉ��f�o�b�O�o��-----------------------------------

        Debug.Log("���ʂ��X�V���܂���");

        if (IsFirstPlaceEnemyDefeated()) {
            Debug.Log("1�ʂ̓G���|����܂���");
            // �^�C�}�[��~����
            _timeLimit.End3rdGame();

        } else {
            Debug.Log("1�ʂ̓G�͂܂��|����Ă��܂���");
        }
    }

    private bool IsFirstPlaceEnemyDefeated() {

        if (_currentPlayerRank == 1) {
            return true;
        }

        if (_currentPlayerRank <= 2) {

            // 1�ʂ̃v���C���[��T��
            GameObject firstRankObject = default;
            for (int i = 0; i < _playerRanks.Length; i++) {
                if (_playerRanks[i] == 1) {
                    firstRankObject = _playerObjects[i];
                    break;
                }
            }

            // ����1�ʂ̃I�u�W�F�N�g����A�N�e�B�u�Ȃ�true��Ԃ�
            if (!firstRankObject.activeSelf) {
                return true;
            }
        }
        // ����ȊO��false
        return false;

    }
}
