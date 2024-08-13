using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationHatch : MonoBehaviour {
    [SerializeField] private float _rotationSpeed = 90f; // 回転速度（度/秒）
    [SerializeField] private float _waitTime = 2f; // 待機時間（秒）

    private bool _isRotatingToMinus180 = true;
    private float _currentRotation = 0f;

    void Update() {
        if (_isRotatingToMinus180) {
            // -180度まで回転
            if (_currentRotation > -180f) {
                float rotationStep = _rotationSpeed * Time.deltaTime;
                _currentRotation -= rotationStep;
                _currentRotation = Mathf.Max(_currentRotation, -180f); // -180度を超えないようにする
                transform.localRotation = Quaternion.Euler(0f, 0f, _currentRotation);
            } else {
                _isRotatingToMinus180 = false;
                StartCoroutine(WaitAndRotateBack());
            }
        }
    }

    private IEnumerator WaitAndRotateBack() {
        // 2秒間待機
        yield return new WaitForSeconds(_waitTime);

        while (_currentRotation < 0f) {
            float rotationStep = _rotationSpeed * Time.deltaTime;
            _currentRotation += rotationStep;
            _currentRotation = Mathf.Min(_currentRotation, 0f); // 0度を超えないようにする
            transform.localRotation = Quaternion.Euler(0f, 0f, _currentRotation);
            yield return null;
        }
    }
}
