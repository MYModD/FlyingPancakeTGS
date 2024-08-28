using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TestWaypointsRank : MonoBehaviour {
    [SerializeField, Header("ランクをつけたいオブジェクト")]
    private GameObject[] _playersRankObject;

    [SerializeField, Header("プレイヤーがいくつ通ったかの管理")]
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
            
            //ここから書くよじゃあね
            
            }


        }


    }



}

