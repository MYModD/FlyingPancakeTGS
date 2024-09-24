using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DegreePizzaEffect : MonoBehaviour {
    public float _upSpeed = 40f;
    public float _alphaSpeed = 2;
    public float _timer;
    public Transform _target;
    public Image[] _images;
    private bool _isStart = false;
    private RectTransform _rectTransform;
    private float _timerValue;
    private Vector3 _firstPostion;

    void Awake() {
        _rectTransform = GetComponent<RectTransform>();
        _firstPostion = _rectTransform.position;
    }

    // Update is called once per frame
    void Update() {
        // �����Ƀ^�C�}�[���X�V���鏈����0�ȉ��ɂȂ�����_isStart��false�ɂ��鏈��
        _timerValue -= Time.deltaTime;
        if (_timerValue <= 0) {
            _isStart = false;
        }

        if (_isStart) {
            _rectTransform.position += new Vector3(0, _upSpeed * Time.deltaTime, 0);
            foreach (Image item in _images) {
                // ������_alphaSpeed���g���ď��X�ɓ��������鏈��
                Color color = item.color;
                color.a -= _alphaSpeed * Time.deltaTime;
                item.color = color;
            }
        }
    }
    [Button]
    public void StartDegreeEffect() {
        //������
        // ������_rectTransform.postion��_firstPostion�ɂ���
        _rectTransform.position = _firstPostion;

        // image�̓����x��100���ɂ��鏈��
        foreach (Image item in _images) {
            Color color = item.color;
            color.a = 1f;
            item.color = color;
        }

        _isStart = true;
        _timerValue = _timer;
    }
}