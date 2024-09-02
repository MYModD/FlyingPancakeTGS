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

    [SerializeField, Header("�����N���������I�u�W�F�N�g")]
    private GameObject[] _playerObjects;

    [ReadOnly, Header("�v���C���[�������ʂ������̊Ǘ�")]
    [SerializeField]
    private int[] _playerWaypointCounts;

    [ReadOnly, Header("�����N�̊Ǘ�")]
    [SerializeField]
    private int[] _playerRanks;

    [SerializeField, Header("�v���C���[�̃I�u�W�F�N�g")]
    private GameObject _currentPlayerObject;

    [SerializeField, Header("�v���C���[�������ʂ�")]
    private int _currentPlayerRank;

    [Range(0f, 0.1f), Header("�����N�����̕p�x")]
    [SerializeField]
    private float _repeatTime;

    [SerializeField, Header("1�ʂ����j���ꂽ�Ƃ��Ɏ��s���鏈��")]
    private TimeLimit _timeLimit;


    /// <summary>
    /// Start����Ȃ��Ă������ł��A���C�\��
    /// </summary>
    private void Start() {
        
        _playerRanks = new int[_playerObjects.Length];
        _playerWaypointCounts = new int[_playerObjects.Length];

        // ������:���ꂪ���s����Ă��牽�b��Ɏ��s���邩
        // ��O����:���b���ƂɎ��s���邩
        //InvokeRepeating(nameof(UpdateRanks), 0.1f, _repeatTime);
    }






    public void UpdatePlayerWaypointCount(GameObject playerObject, int waypointCount) {
        int playerIndex = Array.IndexOf(_playerObjects, playerObject);

        if (playerIndex != -1) {
            _playerWaypointCounts[playerIndex] = waypointCount;
        }

    }

  

    private void Update() {
        if (!gameObject.activeSelf) {
            return;
        }
        _textTitle.text = "ToBeTheTop";
        _textScore.text = _currentPlayerRank.ToString()+"/"+"9";



        int currentPlayerIndex = Array.IndexOf(_playerObjects, _currentPlayerObject);

        int[] sortedIndices = Enumerable.Range(0, _playerWaypointCounts.Length)
            .OrderByDescending(i => _playerWaypointCounts[i])
            .ToArray();

        for (int i = 0; i < sortedIndices.Length; i++) {
            _playerRanks[sortedIndices[i]] = i + 1;
        }

        _currentPlayerRank = _playerRanks[currentPlayerIndex];
        
        

        // -------------------------------------��������{��-----------------------------------


        Debug.Log("�Ȃ񂩂������s����Ă�");

        if (IsFirstPlaceEnemyDefeated() == true) {
            Debug.Log("true�Ȃ����悧");
            //�����Ƀ^�C�}�[�X�g�b�v�̃X�N���v�g
            _timeLimit.End3rdGame();
            this.gameObject.SetActive(false);

        } else {
            Debug.Log("false�ɂȂ����悧");
        }
    }

    private bool IsFirstPlaceEnemyDefeated() {

        if (_currentPlayerRank == 1) {
            return true;
        }

        if (_currentPlayerRank <= 2) {

            // 1�ʂ�player��T��
            GameObject firstRankObject = default;
            for (int i = 0; i < _playerRanks.Length; i++) {
                if (_playerRanks[i] == 1) {

                    firstRankObject = _playerObjects[i];
                    break;
                }
            }

            // ����1�ʂ̓G�����j����Ă���false�Ȃ��true��Ԃ�
            if (firstRankObject.activeSelf == false) {
            
                return true;
            }
        }
        //����ȊO��false
        return false;

    }
}