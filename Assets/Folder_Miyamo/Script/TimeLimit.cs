using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLimit : MonoBehaviour
{

    [Header("制限時間")]
    public float _limitTime = 60f;



    private float _hohgoehgoehogh;
    public bool _isStart = false;
    // 後で直す

    public void LimitTimerStart() {
        _isStart = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isStart) {

            _limitTime -= Time.deltaTime;
            if (_limitTime <= 0f) {
                End3rdGame();
            }
        
        }
    }


    /// <summary>
    /// タイマーが0になったもしくは、1位になった敵が撃破されたら実行する
    /// </summary>
    public  void End3rdGame() {

        _isStart = false;
        float cashTimer = _limitTime;
        if (cashTimer <= 0) {
            cashTimer = 0;
        }

        Debug.Log($"3rdマップのタイマーは  {cashTimer}  でした!!");

        //ここに書く


    }
}
