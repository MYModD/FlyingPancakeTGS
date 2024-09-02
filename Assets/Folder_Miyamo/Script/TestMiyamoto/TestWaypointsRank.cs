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

    [SerializeField, Header("ランクをつけたいオブジェクト")]
    private GameObject[] _playerObjects;

    [ReadOnly, Header("プレイヤーがいくつ通ったかの管理")]
    [SerializeField]
    private int[] _playerWaypointCounts;

    [ReadOnly, Header("ランクの管理")]
    [SerializeField]
    private int[] _playerRanks;

    [SerializeField, Header("プレイヤーのオブジェクト")]
    private GameObject _currentPlayerObject;

    [SerializeField, Header("プレイヤーが今何位か")]
    private int _currentPlayerRank;

    [Range(0f, 0.1f), Header("ランク処理の頻度")]
    [SerializeField]
    private float _repeatTime;

    [SerializeField, Header("1位が撃破されたときに実行する処理")]
    private TimeLimit _timeLimit;


    /// <summary>
    /// Startじゃなくてもいいです、改修予定
    /// </summary>
    private void Start() {
        
        _playerRanks = new int[_playerObjects.Length];
        _playerWaypointCounts = new int[_playerObjects.Length];

        // 第二引数:これが実行されてから何秒後に実行するか
        // 第三引数:何秒ごとに実行するか
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
        
        

        // -------------------------------------ここから本編-----------------------------------


        Debug.Log("なんかいも実行されてる");

        if (IsFirstPlaceEnemyDefeated() == true) {
            Debug.Log("trueなったよぉ");
            //ここにタイマーストップのスクリプト
            _timeLimit.End3rdGame();
            this.gameObject.SetActive(false);

        } else {
            Debug.Log("falseになったよぉ");
        }
    }

    private bool IsFirstPlaceEnemyDefeated() {

        if (_currentPlayerRank == 1) {
            return true;
        }

        if (_currentPlayerRank <= 2) {

            // 1位のplayerを探す
            GameObject firstRankObject = default;
            for (int i = 0; i < _playerRanks.Length; i++) {
                if (_playerRanks[i] == 1) {

                    firstRankObject = _playerObjects[i];
                    break;
                }
            }

            // もし1位の敵が撃破されていてfalseならばtrueを返す
            if (firstRankObject.activeSelf == false) {
            
                return true;
            }
        }
        //それ以外はfalse
        return false;

    }
}