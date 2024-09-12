using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeR : MonoBehaviour {
    // ��]������������̃I�u�W�F�N�g
    [SerializeField] private GameObject _targetObject;

    // Y���̉�]�p�x�i�x���j
    [SerializeField] private float _rNumber;

    private void OnEnable() {
        if (_targetObject != null) {
            // ����̃I�u�W�F�N�g�̌��݂̉�]�p�x���擾
            Vector3 currentRotation = _targetObject.transform.eulerAngles;

            // Y���̉�]�p�x��ݒ�
            currentRotation.y = _rNumber;

            // �V������]���I�u�W�F�N�g�ɓK�p
            _targetObject.transform.eulerAngles = currentRotation;
        } else {
            Debug.LogWarning("�^�[�Q�b�g�I�u�W�F�N�g���w�肳��Ă��܂���B");
        }
    }
}
