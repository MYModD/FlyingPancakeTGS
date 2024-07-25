using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class EnemiesPassingThroughMoveSpline : MonoBehaviour
{
    #region �ϐ�

    [SerializeField, Tooltip("�X�v���C��")]
    private SplineContainer _spline;

    [Header("�ړ��֘A")]
    [SerializeField, Tooltip("���[�g���x")]
    private float _rootSpeed;

    [SerializeField, Tooltip("���x�̕ύX���x")]
    private float _changeingSpeed;

    //�ړ����x
    private float _moveSpeed;

    //�ύX��̑��x
    private float _changeSpeed;

    //����x
    private float _defaultSpeed;

    //�X�v���C���ɉ����Ĉړ�������Ώ�
    private Transform _moveTarget;

    //��Ԃ̊���(0~1�̊Ԃ��n�_^�I�_�ňړ�)
    private float _percentage;

    //�O�t���[���̃��[���h�ʒu
    private Vector3 _prevPos;

    //�X�v���C���̏I�_
    private const int ENDSPLINE = 1;

    //�X�v���C���̊J�n
    private const int STARTSPLINE = 0;

    //��~��
    private bool _isStop = true;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //�X�v���C���ɉ����Ĉړ�������Ώ�
        _moveTarget = this.gameObject.transform;

        //�ړ����x�����[�g���x�ɐݒ�
        _moveSpeed = _rootSpeed;
        _changeSpeed = _moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isStop) {return;}

        //���������Ԃŉ��Z
        _percentage += Time.deltaTime * _moveSpeed;

        MovePosRotate();
    }

    private void MovePosRotate() {

        if (_percentage >= ENDSPLINE) {

            _percentage = STARTSPLINE;
            _isStop = true;
        }

        // �v�Z�����ʒu�i���[���h���W�j���^�[�Q�b�g�ɑ��
        _moveTarget.position = _spline.EvaluatePosition(_percentage);

        // ���݃t���[���̃t���[���ʒu
        Vector3 position = _moveTarget.position;

        // �ړ��ʂ��v�Z
        Vector3 moveVolume = position - _prevPos;

        // ����Update�Ŏg�����߂̑O�t���[���ʒu�⊮
        _prevPos = position;

        // �Î~���Ă����Ԃ��ƁA�i�s���������ł��Ȃ����߉�]���Ȃ�
        if (moveVolume == Vector3.zero) {
            return;
        }

        // �i�s�����Ɋp�x��ύX
        _moveTarget.rotation = Quaternion.LookRotation(moveVolume, Vector3.up);
    }

    public void StartMoving() {

        _percentage = STARTSPLINE;
        _isStop = false;
    }

}