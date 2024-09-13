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
        //‚O‚ð‚È‚­‚·‚½‚ß‚É‰ÁŽZ
        input += 2;
        //‘¬“x•Ï‰»‚³‚¹‚é’l‚ÌŒˆ’è
        float changePower = Time.deltaTime * input;
        return changePower;
    }

    

    //public void GetSpownPointNumber(int spawnPointNumber) {

    //    _spawnPointNumber = spawnPointNumber;
    //}
}
