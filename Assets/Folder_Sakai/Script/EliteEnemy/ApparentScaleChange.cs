using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApparentScaleChange : MonoBehaviour {
    // �J��������̋�����1�̂Ƃ��̃X�P�[���l
    Vector3 _baseScale;

    // �X�N���v�g�̓����L��/�����ɂ���t���O
    [SerializeField] private bool _isActive = true;

    void Start() {
        // �J��������̋�����1�̂Ƃ��̃X�P�[���l���Z�o
        this._baseScale = this.transform.localScale / this.GetDistance();
    }

    void Update() {
        // �X�N���v�g���L���ȏꍇ�̂݃X�P�[�����X�V
        if (_isActive) {
            this.transform.localScale = this._baseScale * this.GetDistance();
        }
    }

    // �J��������̋������擾
    float GetDistance() {
        return (this.transform.position - Camera.main.transform.position).magnitude;
    }

    // �X�N���v�g�̓����؂�ւ��郁�\�b�h
    public void ToggleActive() {
        _isActive = !_isActive;
    }
}
