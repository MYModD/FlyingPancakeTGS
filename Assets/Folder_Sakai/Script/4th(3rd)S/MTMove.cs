using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class MTMove : MonoBehaviour
{
    [SerializeField] private SplineAnimate _splineAnimate;
    

    private void Update() {

        _splineAnimate.ElapsedTime += CalculateSpeed(0);
        
    }

    private float CalculateSpeed(float input) {
        //０をなくすために加算
        input += 2;
        //速度変化させる値の決定
        float changePower = Time.deltaTime * input;
        return changePower;
    }

    

    //public void GetSpownPointNumber(int spawnPointNumber) {

    //    _spawnPointNumber = spawnPointNumber;
    //}
}
