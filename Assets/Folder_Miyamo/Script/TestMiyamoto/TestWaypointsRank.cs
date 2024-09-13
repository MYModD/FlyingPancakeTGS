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

    [SerializeField, Header("プレイヤーのオブジェクト")]
    private GameObject[] _playerObjects;

    [ReadOnly, Header("プレイヤーごとのウェイポイント数の管理")]
    [SerializeField]
    private int[] _playerWaypointCounts;

    [ReadOnly, Header("プレイヤーの順位管理")]
    [SerializeField]
    private int[] _playerRanks;

    [SerializeField, Header("現在のプレイヤーオブジェクト")]
    private GameObject _currentPlayerObject;

    [SerializeField, Header("現在のプレイヤーの順位")]
    private int _currentPlayerRank;

    [Range(0f, 0.1f), Header("順位更新の間隔")]
    [SerializeField]
    private float _repeatTime;

    [SerializeField, Header("1位になった時に発動する時間制限")]
    private TimeLimit _timeLimit;


    /// <summary>
    /// Start関数。初期化処理
    /// </summary>
    private void Start() {

        _playerRanks = new int[_playerObjects.Length];
        _playerWaypointCounts = new int[_playerObjects.Length];
        // プレイヤー以外のオブジェクトにランダムなウェイポイント数を割り当てる
        for (int i = 0; i < _playerWaypointCounts.Length; i++) {
            if (_playerObjects[i] != _currentPlayerObject) {
                _playerWaypointCounts[i] = UnityEngine.Random.Range(1, 10); // 1～10のランダムな数値
            } else {
                _playerWaypointCounts[i] = 0; // プレイヤーは0に初期化
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

        // -------------------------------------以下デバッグ出力-----------------------------------

        Debug.Log("順位を更新しました");

        if (IsFirstPlaceEnemyDefeated()) {
            Debug.Log("1位の敵が倒されました");
            // タイマー停止処理
            _timeLimit.End3rdGame();

        } else {
            Debug.Log("1位の敵はまだ倒されていません");
        }
    }

    private bool IsFirstPlaceEnemyDefeated() {

        if (_currentPlayerRank == 1) {
            return true;
        }

        if (_currentPlayerRank <= 2) {

            // 1位のプレイヤーを探す
            GameObject firstRankObject = default;
            for (int i = 0; i < _playerRanks.Length; i++) {
                if (_playerRanks[i] == 1) {
                    firstRankObject = _playerObjects[i];
                    break;
                }
            }

            // もし1位のオブジェクトが非アクティブならtrueを返す
            if (!firstRankObject.activeSelf) {
                return true;
            }
        }
        // それ以外はfalse
        return false;

    }
}
