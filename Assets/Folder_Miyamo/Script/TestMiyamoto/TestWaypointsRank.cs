using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWaypointsRank : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _playerRankObject;

    [SerializeField]
    private int[] _playerRankInt;






    public void UpdatePlayerRank(GameObject gameobj,int hoge) {

        int i = Array.IndexOf(_playerRankObject, gameobj);

        _playerRankInt[i] = hoge;
    

    }







}

