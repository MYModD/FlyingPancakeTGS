using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBattleship : MonoBehaviour {
    [Foldout("��C")]
    [SerializeField, Header("��C01")]
    private Transform _largeTurret01;
    [Foldout("��C")]
    [SerializeField, Header("��C01�̉�]�͈�")]
    [MinMaxSlider(-360, 360f)] private Vector2 _largeTurret01Range;

    [Foldout("��C")]
    [SerializeField, Header("��C02")]
    private Transform _largeTurret02;
    [Foldout("��C")]
    [SerializeField, Header("��C02�̉�]�͈�")]
    [MinMaxSlider(-50, 50)] private Vector2 _largeTurret02Range;

    [Foldout("�G")]
    [SerializeField, Header("�G�̃g�����X�t�H�[��")]
    private Transform _enemyTransform;

    // Update is called once per frame
    void FixedUpdate() {
        // _largeTurret01�̉�]
        RotateTurretY(_largeTurret01, _largeTurret01Range, _enemyTransform);

        // _largeTurret02�̉�]
        RotateTurretX(_largeTurret01, _largeTurret02, _largeTurret02Range, _enemyTransform);
    }

    private void RotateTurretY(Transform turretTransform, Vector2 rotationRange, Transform target) {
        Vector3 direction = target.position - turretTransform.position;
        direction.y = 0; // Y���̉�]�������l�����邽�߂ɁAY������0�ɂ���

        if (direction != Vector3.zero) {
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // ���݂̉�]�p�x��Y���ɐ���
            float targetYRotation = targetRotation.eulerAngles.y;

            // ��]�͈͓��ɐ���
            targetYRotation = Mathf.Clamp(targetYRotation, rotationRange.x, rotationRange.y);

            // �V������]�p�x��Y���ɐݒ�
            Quaternion limitedRotation = Quaternion.Euler(0, targetYRotation, 0);

            // ���ʐ��`��Ԃ��g���ĉ�]�����X�Ƀ^�[�Q�b�g�Ɍ�����
            turretTransform.rotation = Quaternion.Lerp(turretTransform.rotation, limitedRotation, 0.05f);
        }
    }

    private void RotateTurretX(Transform parentTransform, Transform turretTransform, Vector2 rotationRange, Transform target) {
        Vector3 direction = target.position - parentTransform.position;

        if (direction != Vector3.zero) {
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // ���݂�X����]�p�x�𐧌�
            float targetXRotation = targetRotation.eulerAngles.x;
            if (targetXRotation > 180)
                targetXRotation -= 360; // X����]�p�x�̐��K��
            targetXRotation = Mathf.Clamp(targetXRotation, rotationRange.x, rotationRange.y);

            // �V������]�p�x��ݒ�
            Quaternion limitedRotation = Quaternion.Euler(targetXRotation, parentTransform.eulerAngles.y, 0);

            // ���ʐ��`��Ԃ��g���ĉ�]�����X�Ƀ^�[�Q�b�g�Ɍ�����
            turretTransform.rotation = Quaternion.Lerp(turretTransform.rotation, limitedRotation, 0.05f);
        }
    }
}
