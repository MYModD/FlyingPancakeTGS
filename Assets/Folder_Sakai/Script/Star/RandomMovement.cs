using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    #region �ϐ�
    [SerializeField, Header("Center Object")]
    private GameObject _centerObject; // ���S�ƂȂ�I�u�W�F�N�g

    [SerializeField, Header("Vertical Move Limit")]
    private float _moveLimitVertical;

    [SerializeField, Header("Horizontal Move Limit")]
    private float _movementLimitHorizontal;

    [SerializeField, Header("Move Speed")]
    private float _moveSpeed = 1.0f;

    private Vector3 _targetPosition;
    #endregion

    #region ���\�b�h
    /// <summary>
    /// ����������
    /// </summary>
    void Awake() {
        SetNewTargetPosition();
    }

    /// <summary>
    /// �X�V�O����
    /// </summary>
    void Start() {
        // �������͓��ɕK�v�Ȃ�
    }

    /// <summary>
    /// �X�V����
    /// </summary>
    void Update() {
        Move();
    }

    /// <summary>
    /// �w��͈͓����ړ����鏈��
    /// </summary>
    void Move() {
        float step = _moveSpeed * Time.deltaTime;
        Vector3 newPosition = Vector3.MoveTowards(this.gameObject.transform.position, _targetPosition, step);
        newPosition.z = this.gameObject.transform.position.z; // Z���W���Œ�
        this.gameObject.transform.position = newPosition;

        if (Vector3.Distance(this.gameObject.transform.position, _targetPosition) < 0.001f) {
            SetNewTargetPosition();
        }
    }

    /// <summary>
    /// �V�����ړ����ݒ肷�鏈��
    /// </summary>
    void SetNewTargetPosition() {
        float centerX = _centerObject.transform.position.x;
        float centerY = _centerObject.transform.position.y;
        float fixedZ = this.gameObject.transform.position.z; // �Œ肳�ꂽZ���W

        float targetX = Random.Range(centerX - _movementLimitHorizontal, centerX + _movementLimitHorizontal);
        float targetY = Random.Range(centerY - _moveLimitVertical, centerY + _moveLimitVertical);

        _targetPosition = new Vector3(targetX, targetY, fixedZ);
    }
    #endregion
}
