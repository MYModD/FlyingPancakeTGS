using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TestWaypointsRank : MonoBehaviour {
    [SerializeField, Header("ランクをつけたいオブジェクト")]
    private GameObject[] _playersRankObject;

    [SerializeField, Header("プレイヤーがいくつ通ったか")]
    private int[] _playerWaypointsInt;

    [SerializeField, Header("ランクの管理")]
    private int[] _playersRank;

    [SerializeField, Header("プレイヤーのオブジェクト")]
    private GameObject _gamePlayObject;

    [SerializeField, Header("プレイヤーが今何位か")]
    private int _rankCurrent;

    private void Start() {

        // _playersRankの長さを_playersRankObjectと同じにする
        _playersRank = new int[_playersRankObject.Length];



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
        int i = Array.IndexOf(_playersRankObject, _gamePlayObject);
        int[] hoge = _playerWaypointsInt;

        // ランクを計算
        _playersRank = _playerWaypointsInt
            .Select((score, index) => new { Score = score, Index = index })
            .OrderByDescending(x => x.Score)
            .Select((x, rank) => new { x.Index, Rank = rank + 1 })
            .OrderBy(x => x.Index)
            .Select(x => x.Rank)
            .ToArray();

        // 現在のプレイヤーのランクを取得
        _rankCurrent = _playersRank[i];

        
    }












}

