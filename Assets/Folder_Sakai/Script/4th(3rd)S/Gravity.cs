using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Gravity : MonoBehaviour
{
    [SerializeField, Header("消滅するまでの時間")]
    private float _timer = 1;

    [SerializeField, Header("重力つける？")]
    private bool _isGravityON = false;

    [SerializeField, Header("重力、おちやすさ"), EnableIf(nameof(_isGravityON))]
    private float _gravity = 500f;


    private float _offtimeValue;        // 時間計算用
    private Rigidbody _rigidbody;

    private void Awake() {

        _rigidbody = GetComponent<Rigidbody>();
    }




    private void FixedUpdate() {

        //重力の処理
        if (_isGravityON) {

            _rigidbody.AddForce(new Vector3(0, -1 * _gravity, 0), ForceMode.Acceleration);
        }

    }
}