using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApparentScaleChange : MonoBehaviour {
    // �J��������̋�����1�̂Ƃ��̃X�P�[���l
    private Vector3 _baseScale;

    [SerializeField] private GameObject _camera;
    [SerializeField] private float _minScale = 0.5f;  // �X�P�[��A
    [SerializeField] private float _maxScale = 2.0f;  // �X�P�[��B

    private void OnEnable() {
        // �J��������̋�����1�̂Ƃ��̃X�P�[���l���Z�o
        _baseScale = transform.localScale / GetDistance();
    }

    void Update() {
        // ���݂̃X�P�[�����v�Z
        float distance = GetDistance();
        Vector3 newScale = _baseScale * distance;

        // �X�P�[�����w��͈͂Ɏ��܂�悤�ɐ���
        float clampedScaleX = Mathf.Clamp(newScale.x, _minScale, _maxScale);
        float clampedScaleY = Mathf.Clamp(newScale.y, _minScale, _maxScale);
        float clampedScaleZ = Mathf.Clamp(newScale.z, _minScale, _maxScale);

        transform.localScale = new Vector3(clampedScaleX, clampedScaleY, clampedScaleZ);
    }

    // �J��������̋������擾
    float GetDistance() {
        return (transform.position - _camera.transform.position).magnitude;
    }
}
