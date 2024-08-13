using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationHatch : MonoBehaviour {
    [SerializeField] private float _rotationSpeed = 90f; // ��]���x�i�x/�b�j
    [SerializeField] private float _waitTime = 2f; // �ҋ@���ԁi�b�j

    private bool _isRotatingToMinus180 = true;
    private float _currentRotation = 0f;

    void Update() {
        if (_isRotatingToMinus180) {
            // -180�x�܂ŉ�]
            if (_currentRotation > -180f) {
                float rotationStep = _rotationSpeed * Time.deltaTime;
                _currentRotation -= rotationStep;
                _currentRotation = Mathf.Max(_currentRotation, -180f); // -180�x�𒴂��Ȃ��悤�ɂ���
                transform.localRotation = Quaternion.Euler(0f, 0f, _currentRotation);
            } else {
                _isRotatingToMinus180 = false;
                StartCoroutine(WaitAndRotateBack());
            }
        }
    }

    private IEnumerator WaitAndRotateBack() {
        // 2�b�ԑҋ@
        yield return new WaitForSeconds(_waitTime);

        while (_currentRotation < 0f) {
            float rotationStep = _rotationSpeed * Time.deltaTime;
            _currentRotation += rotationStep;
            _currentRotation = Mathf.Min(_currentRotation, 0f); // 0�x�𒴂��Ȃ��悤�ɂ���
            transform.localRotation = Quaternion.Euler(0f, 0f, _currentRotation);
            yield return null;
        }
    }
}
