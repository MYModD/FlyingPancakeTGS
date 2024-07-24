using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApparentScaleChange : MonoBehaviour
{
    // �J��������̋�����1�̂Ƃ��̃X�P�[���l
    Vector3 _baseScale;

    void Start() {
        // �J��������̋�����1�̂Ƃ��̃X�P�[���l���Z�o
        this._baseScale = this.transform.localScale / this.GetDistance();
    }

    void Update() {
        this.transform.localScale = this._baseScale * this.GetDistance();
    }

    // �J��������̋������擾
    float GetDistance() {
        return (this.transform.position - Camera.main.transform.position).magnitude;
    }
}
