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
        //�O���Ȃ������߂ɉ��Z
        input += 2;
        //���x�ω�������l�̌���
        float changePower = Time.deltaTime * input;
        return changePower;
    }

    

    //public void GetSpownPointNumber(int spawnPointNumber) {

    //    _spawnPointNumber = spawnPointNumber;
    //}
}
