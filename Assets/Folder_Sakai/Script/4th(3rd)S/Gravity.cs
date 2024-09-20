using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Gravity : MonoBehaviour
{
    [SerializeField, Header("���ł���܂ł̎���")]
    private float _timer = 1;

    [SerializeField, Header("�d�͂���H")]
    private bool _isGravityON = false;

    [SerializeField, Header("�d�́A�����₷��"), EnableIf(nameof(_isGravityON))]
    private float _gravity = 500f;


    private float _offtimeValue;        // ���Ԍv�Z�p
    private Rigidbody _rigidbody;

    private void Awake() {

        _rigidbody = GetComponent<Rigidbody>();
    }




    private void FixedUpdate() {

        //�d�͂̏���
        if (_isGravityON) {

            _rigidbody.AddForce(new Vector3(0, -1 * _gravity, 0), ForceMode.Acceleration);
        }

    }
}