using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TestWaypointsRank : MonoBehaviour {
    [SerializeField, Header("�����N���������I�u�W�F�N�g")]
    private GameObject[] _playersRankObject;

    [SerializeField, Header("�v���C���[�������ʂ�����")]
    private int[] _playerWaypointsInt;

    [SerializeField, Header("�����N�̊Ǘ�")]
    private int[] _playersRank;

    [SerializeField, Header("�v���C���[�̃I�u�W�F�N�g")]
    private GameObject _gamePlayObject;

    [SerializeField, Header("�v���C���[�������ʂ�")]
    private int _rankCurrent;

    private void Start() {

        // _playersRank�̒�����_playersRankObject�Ɠ����ɂ���
        _playersRank = new int[_playersRankObject.Length];



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
        int i = Array.IndexOf(_playersRankObject, _gamePlayObject);
        int[] hoge = _playerWaypointsInt;

        // �����N���v�Z
        _playersRank = _playerWaypointsInt
            .Select((score, index) => new { Score = score, Index = index })
            .OrderByDescending(x => x.Score)
            .Select((x, rank) => new { x.Index, Rank = rank + 1 })
            .OrderBy(x => x.Index)
            .Select(x => x.Rank)
            .ToArray();

        // ���݂̃v���C���[�̃����N���擾
        _rankCurrent = _playersRank[i];

        
    }












}

