using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class EnemyMove : MonoBehaviour
{
    #region 変数
    [SerializeField] private SplineAnimate _splineAnimate;
    [SerializeField, Header("敵(エリート)の基本移動速度に足す値")] private float _moveSpeed;
    #endregion

    private void Update() {

        _splineAnimate.ElapsedTime += _moveSpeed;
    }

    public void ChangeSpeed(float changeSpeed) {

        _moveSpeed = changeSpeed;
    }
}
