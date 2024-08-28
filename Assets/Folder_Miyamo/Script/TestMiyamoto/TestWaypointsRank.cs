using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TestWaypointsRank : MonoBehaviour {
    [SerializeField, Header("�����N���������I�u�W�F�N�g")]
    private GameObject[] _playersRankObject;

    [SerializeField, Header("�v���C���[�������ʂ������̊Ǘ�")]
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
        int correctMe = Array.IndexOf(_playersRankObject, _gamePlayObject);
        int[] hoge = _playerWaypointsInt;

        for (int i = 0; i < _playerWaypointsInt.Length; i++) {
            int maxPoint = _playerWaypointsInt[i];
            int maxPointNum = default;

            for (int j = i + 1; i < _playerWaypointsInt.Length; j++) {

                if (_playerWaypointsInt[j] > maxPoint) {
                    maxPoint = _playerWaypointsInt[j];
                    maxPointNum = j;

                }

            }

            if (maxPoint != _playerWaypointsInt[i]) {
            
            //�������珑���悶�Ⴀ��
            
            }


        }


    }



}

