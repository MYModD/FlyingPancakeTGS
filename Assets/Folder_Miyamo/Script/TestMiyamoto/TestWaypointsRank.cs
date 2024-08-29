using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using NaughtyAttributes;

public class TestWaypointsRank : MonoBehaviour {
    [SerializeField, Header("ランクをつけたいオブジェクト")]
    private GameObject[] _playersRankObject;

    [SerializeField, Header("プレイヤーがいくつ通ったかの管理")]
    [ReadOnly]
    private int[] _playerWaypointsInt;

    [SerializeField, Header("ランクの管理")]
    [ReadOnly]
    private int[] _playersRank;

    [SerializeField, Header("プレイヤーのオブジェクト")]
    private GameObject _gamePlayObject;

    [SerializeField, Header("プレイヤーが今何位か")]
    private int _rankCurrent;

    private void Start() {

        // _playersRankの長さを_playersRankObjectと同じにする
        _playersRank = new int[_playersRankObject.Length];
        // _playerWaypointsIntも同じ長さで初期化
        _playerWaypointsInt = new int[_playersRankObject.Length];

    }

    /// <summary>
    /// 全プレイヤーがwaypointを通過したときに処理するメソッド
    /// </summary>
    public void UpdatePlayerRank(GameObject gameobj, int points) {
        int i = Array.IndexOf(_playersRankObject, gameobj);
        _playerWaypointsInt[i] = points;
        Goge();
    }

    public void Goge() {
        // 全プレイヤーの配列の中で自分が何番目にいるか
        int correctMe = Array.IndexOf(_playersRankObject, _gamePlayObject);

        // ランクを計算
        int[] sortedIndices = Enumerable.Range(0, _playerWaypointsInt.Length)
            .OrderByDescending(i => _playerWaypointsInt[i])
            .ToArray();

        for (int i = 0; i < sortedIndices.Length; i++) {
            _playersRank[sortedIndices[i]] = i + 1;
        }

        // 現在のプレイヤーのランクを更新
        _rankCurrent = _playersRank[correctMe];

        // デバッグ用：ランクを表示
        for (int i = 0; i < _playersRank.Length; i++) {
            Debug.Log($"Player {i}: Score {_playerWaypointsInt[i]}, Rank {_playersRank[i]}");
        }
        Debug.Log($"Current player rank: {_rankCurrent}");
    }
}