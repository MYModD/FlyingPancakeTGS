using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class EnemyMove : MonoBehaviour
{
    #region �ϐ�
    [SerializeField] private SplineAnimate _splineAnimate;
    [SerializeField, Header("�G(�G���[�g)�̊�{�ړ����x�ɑ����l")] private float _moveSpeed;
    #endregion

    private void Update() {

        _splineAnimate.ElapsedTime += _moveSpeed;
    }

    public void ChangeSpeed(float changeSpeed) {

        _moveSpeed = changeSpeed;
    }
}
