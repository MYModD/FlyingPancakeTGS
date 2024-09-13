using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class NotRotaionCopyPlayerMove : MonoBehaviour
{
    #region �ϐ�
    [SerializeField, Header("�v���C���[�̏㉺�ړ��ő�l")] private float _maxHeight;
    [SerializeField, Header("�v���C���[�̍��E�ړ��ő�l")] private float _maxWidth;
    [SerializeField, Header("�v���C���[�̈ړ����x�l")] private float _moveSpeed;
    [SerializeField, Header("�p�x�߂��X�s�[�h")] private float _resetSpeed;

    [SerializeField, Header("���E�p�x�̍ŏ��l")] private float _minimumAngle = -45f;
    [SerializeField, Header("���E�p�x�̍ő�l")] private float _maximumAngle = 45;
    [SerializeField, Header("�㉺�p�x�̍ŏ��l")] private float _minimumAngleUp = -45f;
    [SerializeField, Header("�㉺�p�x�̍ő�l")] private float _maximumAngleUp = 45;

    [SerializeField, Header("�X�s�[�h�����{��")] private float _speedMagnification;
    [SerializeField, Header("�v���C���[�̊p�x�{��")] private float _rotateSpeed = 10;

    [SerializeField, Header("�v���C���[�̈ړ��p�x�l")] private float _moveAngle;

    [SerializeField, Header("�J���������Ă���I�u�W�F�N�g")] private GameObject _lookAtObj;
    [SerializeField, Header("�J���������")] private Camera _camera;
    [SerializeField, Header("�X�v���C����ʂ�I�u�W�F�N�g")] private SplineAnimate _splineAnimate1;
    [SerializeField, Header("�X�v���C����ʂ�I�u�W�F�N�g")] private SplineAnimate _splineAnimate2;
    [SerializeField, Header("�X�v���C����ʂ�I�u�W�F�N�g")] private SplineAnimate _splineAnimate3;
    [SerializeField, Header("�X�v���C����ʂ�I�u�W�F�N�g")] private SplineAnimate _splineAnimate4;
    [SerializeField, Header("�X�v���C����ʂ�I�u�W�F�N�g")] private SplineAnimate _splineAnimate5;

    [SerializeField, Header("CanvasManager�̃I�u�W�F�N�g������")] private CanvasManager _canvas;
    [SerializeField] private ControllerSelectButton _selectButton;

    [SerializeField] GameObject _aa;
    bool _a = true;
    private float _stopTime;
    private float _nowTime;
    private bool _isStop = false;
    private bool _isVerticalInversion = false;
    private bool _ishorizontalInversion = false;
    private int _verticalIndex = 1;
    private int _horizontalIndex = 1;
    private float _changePower = 0f;
    #endregion
    #region �v���p�e�B
    #endregion
    #region ���\�b�h

    /// <summary>
    /// �X�V����
    /// </summary>
    void Update() {

        if (!_canvas.CanMove()) {

            return;
        }
        StopOrMoving();
       
    }
    /// <summary>
    /// �����Ă��邩�~�܂��Ă��邩�̕���
    /// </summary>
    private void StopOrMoving() {
        _isVerticalInversion = _selectButton.VerticalInversionCheak();
        _verticalIndex = _isVerticalInversion ? -1 : 1;
        _ishorizontalInversion = _selectButton.HorizontalInversionCheak();
        _horizontalIndex = _ishorizontalInversion ? -1 : 1;
        if (_isStop) {

            transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, Time.deltaTime * _resetSpeed);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.identity, Time.deltaTime * _resetSpeed);
            _nowTime += Time.deltaTime;
            if (_nowTime >= _stopTime) {
                _isStop = false;
                _nowTime = 0;
            }
        } else {

            //�����Ɗp�x
            MovePosition();

        }
        //���x
        //ChangeSpeed();
    }
    /// <summary>
    /// �����Ǘ��v���Z�X�����s
    /// </summary>
    private void MovePosition() {
        //Input��Update�ł܂Ƃ߂Ď�肽��
        //�c�����̓��͒l�ۑ�
        float inputVertical = Input.GetAxis("Vertical");
        //�������̓��͒l�ۑ�
        float inputHorizontal = Input.GetAxis("Horizontal");

        // �����Ŕ��]�������s��
        if (_isVerticalInversion) {
            inputVertical *= -1;
        }

        if (_ishorizontalInversion) {
            inputHorizontal *= -1;
        }

        //�J���������Ă���I�u�W�F�N�g�̈ʒu�̒���

        //_lookAtObj.transform.localPosition = new Vector3(transform.localPosition.x / 2, transform.localPosition.y / 2, _lookAtObj.transform.localPosition.z);


        //���͒l���O�������牽�������Ȃ�
        if (inputHorizontal == 0 && inputVertical == 0) {
            ResetRotation();
            return;
        }

        VerticalProcess(inputVertical, inputHorizontal);
        HorizontalProcess(inputHorizontal);
    }

    /// <summary>
    /// �c�̓��� �㉺�̏�������
    /// </summary>
    private void VerticalProcess(float inputVertical, float inputHorizontal) {

        // �c�̓���
        if (inputVertical < 0) // ���̏���
        {
            if (this.transform.localPosition.y < -_maxHeight) {
                ResetRotation();
                HorizontalProcess(inputHorizontal);
                return;
            }
            Vertical_RotateMove(inputVertical);
        } else if (inputVertical > 0) // ��̏���
          {
            if (this.transform.localPosition.y > _maxHeight) {
                ResetRotation();
                HorizontalProcess(inputHorizontal);
                return;
            }
            Vertical_RotateMove(inputVertical);
        }
    }

    /// <summary>
    /// ���̓����@���E�̏�������
    /// </summary>
    private void HorizontalProcess(float inputHorizontal) {
        //���̓���
        if (inputHorizontal > 0)//���̏���
        {
            //�����l�ȏ�ɓ������Ȃ�
            if (this.transform.localPosition.x < -_maxWidth) {
                //�p�x���O�ɖ߂�����
                ResetRotation();
                return;
            }
            Horizontal_RotateMove(inputHorizontal);
        } else if (inputHorizontal < 0)//�E�̏���
          {
            //�����l�ȏ�ɓ������Ȃ�
            if (this.transform.localPosition.x > _maxWidth) {
                //�p�x���O�ɖ߂�����
                ResetRotation();
                return;
            }
            Horizontal_RotateMove(inputHorizontal);
        }
    }/// <summary>
     /// �v���C���[�̑��x�ω�
     /// </summary>
    private void ChangeSpeed() {
        //�C���v�b�g��Update�ł܂Ƃ߂Ď�肽��
        float inputRStick = Input.GetAxis("RStickV");

        if (_splineAnimate3.enabled) {
            _splineAnimate3.ElapsedTime += _changePower;
        }
        //���x�v�Z�����l
        float speed = CalculateSpeed(inputRStick);
        //�X�v���C����ʂ�I��鎞�Ԃ̐ݒ�l��ς��ĉ�����
        if (_splineAnimate1.enabled) {
            _splineAnimate1.ElapsedTime += speed;
        } else if (_splineAnimate2.enabled) {
            _splineAnimate2.ElapsedTime += speed;
        } else if (_splineAnimate4.enabled) {
            _splineAnimate4.ElapsedTime += speed;
        } else {
            _splineAnimate5.ElapsedTime += speed;
        }

    }
    #region �����Ɋւ��郁�\�b�h
    /// <summary>
    /// �c�̍s���v���Z�X�����s
    /// </summary>
    /// <param name="vertical">Vertical�̓��͒l</param>
    private void Vertical_RotateMove(float vertical) {
        //transform.Rotate(RotateVertical(vertical));
        transform.localPosition += MoveVertical(vertical);
    }
    /// <summary>
    /// ���̍s���v���Z�X�����s
    /// </summary>
    /// <param name="horizontal">Horizontal�̓��͒l</param>
    private void Horizontal_RotateMove(float horizontal) {
        // ��]�̍������v�Z
        Vector3 rotateIndex = RotateHorizontal(horizontal);

        // ���݂�Z���̉�]�p�x���擾���A-180�x����180�x�͈̔͂ɕϊ�
        float currentZRotation = transform.localEulerAngles.z;
        float semicircularAngle = 180f;
        float circularAngle = 360f;
        if (currentZRotation > semicircularAngle) {
            currentZRotation -= circularAngle;
        }

        // �V������]�p�x���v�Z���A�ݒ肵���ŏ��l����ő�l�͈̔͂ɐ���
        float newZRotation = currentZRotation + rotateIndex.z;
        newZRotation = Mathf.Clamp(newZRotation, _minimumAngle, _maximumAngle);

        // �V������]�p�x�̓K�p
        transform.localRotation = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, newZRotation);

        // �ʒu���X�V
        transform.localPosition += MoveHorizontal(horizontal);
    }
    /// <summary>
    /// ���[�J�����[�e�[�V�������O��
    /// </summary>
    private void ResetRotation() {
        // �ڕW�̉�]�iQuaternion.identity�͉�]�Ȃ��j
        Quaternion targetRotation = Quaternion.identity;
        //�O�ɖ߂�
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * _resetSpeed);
    }
    #endregion
    //--------------��������Ԃ�l����-------------------------------------------------------------------------------------------------
    #region Vertical�֘A���\�b�h
    /// <summary>
    /// �c�̓����̐���
    /// </summary>
    /// <param name="vertical">Vertical�̓��͒l</param>
    /// <returns>�P�t���[���̐��i��</returns>
    private Vector3 MoveVertical(float vertical) {
        //��ŏ㉺���씽�]�ł���悤�ɂ���
        //�����̌v�Z
        Vector3 movePower = Vector3.down * vertical * Time.deltaTime * _moveSpeed;
        return -movePower;
    }
    /// <summary>
    /// �c�̊p�x����
    /// </summary>
    /// <param name="vertical">Vertical�̓��͒l</param>
    /// <returns>�P�t���[���ŉ��Z�A���Z�����p�x</returns>
    private Vector3 RotateVertical(float vertical) {
        //����l
        float divide = 2;
        //�p�x�v�Z
        Vector3 rotateIndex = (Vector3.right * _moveAngle / divide * Time.deltaTime);
        //���͒l�ɂ���Đ�����ς���
        if (vertical > 0) {
            return rotateIndex;
        }
        return -rotateIndex;
    }
    #endregion
    #region Horizontal�֘A���\�b�h
    /// <summary>
    /// ���̓����̐���
    /// </summary>
    /// <param name="horizontal">Horizontal�̓��͒l</param>
    /// <returns>�P�t���[���̉��̐��i��</returns>
    private Vector3 MoveHorizontal(float horizontal) {
        //Vector3.left���Ȃ����甽�]�p
        float signIndex = -1;
        //�����̌v�Z
        Vector3 movePower = Vector3.right * horizontal * Time.deltaTime * _moveSpeed * signIndex;
        return movePower;
    }
    /// <summary>
    /// ���̊p�x�̐���
    /// </summary>
    /// <param name="horizontal">Horizontal�̓��͒l</param>
    /// <returns>�P�t���[���ŉ��Z�A���Z�����p�x</returns>
    private Vector3 RotateHorizontal(float horizontal) {
        //�p�x�v�Z
        Vector3 rotateIndex = (Vector3.forward * _moveAngle * Time.deltaTime * _rotateSpeed);
        //���͒l�ɂ���Đ�����ς���
        if (horizontal > 0) {
            return rotateIndex;
        }
        return -rotateIndex;
    }
    #endregion
    /// <summary>
    /// R�X�e�B�b�N�ő��x�ω�
    /// </summary>
    /// <param name="input">R�X�e�B�b�N�̓��͒l�@-1�`1</param>
    /// <returns>���x�ʂ̕ω������l</returns>
    private float CalculateSpeed(float input) {
        //�O���Ȃ������߂ɉ��Z
        input += 2;
        int index = _canvas.CanMove() ? 1 : 0;
        //���x�ω�������l�̌���
        float changePower = Time.deltaTime * input * _speedMagnification * index;
        return changePower;
    }

    public void CalculateSpeed3rd(float input) {

        int index = _canvas.CanMove() ? 1 : 0;
        //���x�ω�������l�̌���
        _changePower = Time.deltaTime * input * _speedMagnification * index;
    }
    //-------------------------��������p�u���b�N���\�b�h---------------------------------------
    /// <summary>
    /// �w��̕b����~���\�b�h
    /// </summary>
    /// <param name="seconds">�~�߂����b��</param>
    public void StopMoving(float seconds) {
        _stopTime = seconds;
        _isStop = true;
    }
    public void StartMoving() {
        _isStop = false;
        _stopTime = 0;
    }
    #endregion
}
