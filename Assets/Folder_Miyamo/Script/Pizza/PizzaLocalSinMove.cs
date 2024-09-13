using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaLocalSinMove : MonoBehaviour
{
    [SerializeField] private float _amplitude = 1f;    // U•
    [SerializeField] private float _frequency = 1f;    // ü”g”
    [SerializeField] private float _speed = 1f;        // ˆÚ“®‘¬“x
    [SerializeField] private float _offset = 0f;       // ˆÊ‘Š

    private Vector3 _startPosition;

    private void Start() {
        _startPosition = transform.localPosition;
    }

    private void Update() {
        float y = Mathf.Sin(Time.time * _speed * Mathf.PI * 2 + _offset) * _amplitude;
        transform.localPosition = _startPosition + new Vector3(0, y, 0);
    }
}
