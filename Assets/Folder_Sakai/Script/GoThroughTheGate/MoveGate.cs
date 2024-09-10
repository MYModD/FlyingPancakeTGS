using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGate : MonoBehaviour {

    #region �ϐ�
    [SerializeField, Tooltip("�ړI�n�ƂȂ�^�[�Q�b�g")]
    private Transform _target;

    [SerializeField, Tooltip("�ړ����x")]
    private float _speed = 5f;

    [SerializeField, Tooltip("���B�Ƃ݂Ȃ�����")]
    private float _stoppingDistance = 0.1f;

    private bool _isMoving = false;
    #endregion

    #region ���\�b�h

    private void Start() {
        DetachChild();
    }

    void Update() {
        if (_isMoving) {
            MoveTowardsTarget();
        }
    }

    // �ړI�n�Ɍ������Ĉړ����鏈��
    private void MoveTowardsTarget() {
        if (_target == null) {
            Debug.LogWarning("�^�[�Q�b�g���ݒ肳��Ă��܂���B");
            return;
        }

        // �^�[�Q�b�g�܂ł̋������v�Z
        float distanceToTarget = Vector3.Distance(transform.position, _target.position);

        // �^�[�Q�b�g�Ƃ̋�������~���������傫���ꍇ�ɂ݈̂ړ�����
        if (distanceToTarget > _stoppingDistance) {
            // ���݂̈ʒu�ƃ^�[�Q�b�g�̈ʒu�̍������v�Z
            Vector3 direction = (_target.position - transform.position).normalized;

            // �t���[�����Ƃɏ������ړ�������
            transform.position += direction * _speed * Time.deltaTime;
        } else {
            // �ړI�n�ɔ��ɋ߂Â����ꍇ�A�ʒu���^�[�Q�b�g�̈ʒu�ɃX�i�b�v���Ē�~
            transform.position = _target.position;
            _isMoving = false;  // �ړ����~
            Debug.Log("�ړI�n�ɓ��B���܂����B");
        }
    }

    // �w�肵���b���ҋ@��Ɉړ����J�n���郁�\�b�h
    public void StartMovingWithDelay(float delaySeconds) {
        StartCoroutine(DelayedStartMoving(delaySeconds));
    }

    // �R���[�`���Ŏw�肵���b���҂���
    private IEnumerator DelayedStartMoving(float delaySeconds) {
        yield return new WaitForSeconds(delaySeconds);
        _isMoving = true;
    }

    // �ړI�n��ݒ肷�郁�\�b�h
    public void SetTarget(Transform newTarget) {
        _target = newTarget;
        _isMoving = false;  // �V�����^�[�Q�b�g��ݒ肵���Ƃ��͒�~��Ԃɂ��Ă���
    }

    private void DetachChild() {
        // �q�I�u�W�F�N�g�̐e�q�֌W������
        if (_target != null) {
            _target.SetParent(null);
        }
    }

    #endregion
}
