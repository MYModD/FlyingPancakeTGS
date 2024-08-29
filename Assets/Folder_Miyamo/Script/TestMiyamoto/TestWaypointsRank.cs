using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using NaughtyAttributes;

public class TestWaypointsRank : MonoBehaviour {
    [SerializeField, Header("�����N���������I�u�W�F�N�g")]
    private GameObject[] _playersRankObject;

    [SerializeField, Header("�v���C���[�������ʂ������̊Ǘ�")]
    [ReadOnly]
    private int[] _playerWaypointsInt;

    [SerializeField, Header("�����N�̊Ǘ�")]
    [ReadOnly]
    private int[] _playersRank;

    [SerializeField, Header("�v���C���[�̃I�u�W�F�N�g")]
    private GameObject _gamePlayObject;

    [SerializeField, Header("�v���C���[�������ʂ�")]
    private int _rankCurrent;

    private void Start() {

        // _playersRank�̒�����_playersRankObject�Ɠ����ɂ���
        _playersRank = new int[_playersRankObject.Length];
        // _playerWaypointsInt�����������ŏ�����
        _playerWaypointsInt = new int[_playersRankObject.Length];

    }

    /// <summary>
    /// �S�v���C���[��waypoint��ʉ߂����Ƃ��ɏ������郁�\�b�h
    /// </summary>
    public void UpdatePlayerRank(GameObject gameobj, int points) {
        int i = Array.IndexOf(_playersRankObject, gameobj);
        _playerWaypointsInt[i] = points;
        Goge();
    }

    public void Goge() {
        // �S�v���C���[�̔z��̒��Ŏ��������Ԗڂɂ��邩
        int correctMe = Array.IndexOf(_playersRankObject, _gamePlayObject);

        // �����N���v�Z
        int[] sortedIndices = Enumerable.Range(0, _playerWaypointsInt.Length)
            .OrderByDescending(i => _playerWaypointsInt[i])
            .ToArray();

        for (int i = 0; i < sortedIndices.Length; i++) {
            _playersRank[sortedIndices[i]] = i + 1;
        }

        // ���݂̃v���C���[�̃����N���X�V
        _rankCurrent = _playersRank[correctMe];

        // �f�o�b�O�p�F�����N��\��
        for (int i = 0; i < _playersRank.Length; i++) {
            Debug.Log($"Player {i}: Score {_playerWaypointsInt[i]}, Rank {_playersRank[i]}");
        }
        Debug.Log($"Current player rank: {_rankCurrent}");
    }
}