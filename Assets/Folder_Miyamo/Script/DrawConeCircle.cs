using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawConeCircle : MonoBehaviour {
    public Camera _mainCamera;
    [SerializeField, Header("CameraManger")]
    private TestLockOnManager _testLockOnManager;
    public bool _debugIsON = false;
    private RectTransform _rectTransform;

    [SerializeField]
    private Text _circleCenterPositionText;

    void Start() {
        _rectTransform = GetComponent<RectTransform>();
        if (_rectTransform == null) {
            Debug.LogError("RectTransform��������܂���B");
        }

        if (_circleCenterPositionText == null) {
            Debug.LogError("Circle Center Position Text�����蓖�Ă��Ă��܂���B");
        }
    }

    private void LateUpdate() {
        UpdateCirclePosition();
    }

    private void UpdateCirclePosition() {
        // ���[���h���W����X�N���[�����W�ւ̕ϊ�
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(_mainCamera, _testLockOnManager._circleCenterPostion);

        // �X�N���[�����W����UI���W�ւ̕ϊ�
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _rectTransform.parent as RectTransform,
            screenPoint,
            null,
            out Vector2 localPoint
        );

        // UI�v�f�̈ʒu��ݒ�
        _rectTransform.anchoredPosition = localPoint;

        // UI�v�f�̃T�C�Y��ݒ�
        float scale = _testLockOnManager._coneAngle * 0.1339f;
        _rectTransform.sizeDelta = new Vector2(100 * scale, 100 * scale); // ��{�T�C�Y��100�Ƃ����ꍇ
    }

    

    private void OnDrawGizmos() {
        if (!_debugIsON) {
            UpdateCirclePosition();
        }
    }
}