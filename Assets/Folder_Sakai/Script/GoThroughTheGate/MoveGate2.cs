using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGate2 : MonoBehaviour
{
    #region �ϐ�
    [SerializeField, Tooltip("�ړI�n�ƂȂ�^�[�Q�b�g1")]
    private Transform _target1;

    [SerializeField, Tooltip("�ړI�n�ƂȂ�^�[�Q�b�g2")]
    private Transform _target2;

    [SerializeField, Tooltip("�ړ����x")]
    private float _speed = 150f;

    [SerializeField, Tooltip("���B�Ƃ݂Ȃ�����")]
    private float _stoppingDistance = 5f;

    private bool _isMoving1 = false;
    private bool _isMoving2 = false;
    #endregion

    #region ���\�b�h

    private void Start() {
        DetachChild();
    }

    void Update() {

        if (_isMoving1) {
            MoveTowardsTarget1();

        } else if (!_isMoving1 && _isMoving2) {
            MoveTowardsTarget2();
        }
    }

    // �ړI�n�Ɍ������Ĉړ����鏈��
    private void MoveTowardsTarget1() {
        if (_target1 == null) {
            Debug.LogWarning("�^�[�Q�b�g���ݒ肳��Ă��܂���B");
            return;
        }

        // �^�[�Q�b�g�܂ł̋������v�Z
        float distanceToTarget = Vector3.Distance(transform.position, _target1.position);

        // �^�[�Q�b�g�Ƃ̋�������~���������傫���ꍇ�ɂ݈̂ړ�����
        if (distanceToTarget > _stoppingDistance) {
            // ���݂̈ʒu�ƃ^�[�Q�b�g�̈ʒu�̍������v�Z
            Vector3 direction = (_target1.position - transform.position).normalized;

            // �t���[�����Ƃɏ������ړ�������
            transform.position += direction * _speed * Time.deltaTime;
        } else {
            // �ړI�n�ɔ��ɋ߂Â����ꍇ�A�ʒu���^�[�Q�b�g�̈ʒu�ɃX�i�b�v���Ē�~
            print("��������");
            transform.position = _target1.position;
            _isMoving1 = false;
            _isMoving2 = true;
        }
    }

    private void MoveTowardsTarget2() {
        if (_target2 == null) {
            Debug.LogWarning("�^�[�Q�b�g���ݒ肳��Ă��܂���B");
            return;
        }

        // �^�[�Q�b�g�܂ł̋������v�Z
        float distanceToTarget = Vector3.Distance(transform.position, _target2.position);

        // �^�[�Q�b�g�Ƃ̋�������~���������傫���ꍇ�ɂ݈̂ړ�����
        if (distanceToTarget > _stoppingDistance) {
            // ���݂̈ʒu�ƃ^�[�Q�b�g�̈ʒu�̍������v�Z
            Vector3 direction = (_target2.position - transform.position).normalized;

            // �t���[�����Ƃɏ������ړ�������
            transform.position += direction * _speed * Time.deltaTime;
        } else {
            // �ړI�n�ɔ��ɋ߂Â����ꍇ�A�ʒu���^�[�Q�b�g�̈ʒu�ɃX�i�b�v���Ē�~
            transform.position = _target2.position;
 
            _isMoving2 = false;
        }
    }

    // �w�肵���b���ҋ@��Ɉړ����J�n���郁�\�b�h
    public void StartMovingWithDelay(float delaySeconds) {
        StartCoroutine(DelayedStartMoving(delaySeconds));
    }

    // �R���[�`���Ŏw�肵���b���҂���
    private IEnumerator DelayedStartMoving(float delaySeconds) {
        yield return new WaitForSeconds(delaySeconds);
        _isMoving1 = true;
    }

    // �ړI�n��ݒ肷�郁�\�b�h
    public void SetTarget(Transform newTarget) {
        _target1 = newTarget;
        _isMoving1 = false;  // �V�����^�[�Q�b�g��ݒ肵���Ƃ��͒�~��Ԃɂ��Ă���
    }

    private void DetachChild() {

        // �q�I�u�W�F�N�g�̐e�q�֌W������
        if (_target1 != null) {
            _target1.SetParent(null);
        }
        if (_target2 != null) {
            _target2.SetParent(null);
        }
    }

    #endregion
}
