using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBattleship : MonoBehaviour {
    public Transform _testTransform1;
    [MinMaxSlider(0, 360f)] public Vector2 _testTransform1Range;
    public Transform _testTransform2;
    [MinMaxSlider(-50, 50)] public Vector2 _testTransform2Range;
    public Transform _enemyTransform;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void FixedUpdate() {
        // _testTransform1�̉�]
        RotateTransformY(_testTransform1, _testTransform1Range, _enemyTransform);

        // _testTransform2�̉�]
        RotateTransform2(_testTransform1, _testTransform2, _testTransform2Range, _enemyTransform);
    }

    private void RotateTransformY(Transform targetTransform, Vector2 rotationRange, Transform target) {
        Vector3 direction = target.position - targetTransform.position;
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
            targetTransform.rotation = Quaternion.Lerp(targetTransform.rotation, limitedRotation, 0.05f);
        }
    }

    private void RotateTransform2(Transform parentTransform, Transform targetTransform, Vector2 rotationRange, Transform enemyTransform) {
        Vector3 direction = enemyTransform.position - targetTransform.position;

        if (direction != Vector3.zero) {
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // ���݂�X����]�p�x�𐧌�
            float targetXRotation = targetRotation.eulerAngles.x;
            targetXRotation = Mathf.Clamp(targetXRotation, rotationRange.x, rotationRange.y);

            // �V������]�p�x��ݒ�
            Quaternion limitedRotation = Quaternion.Euler(targetXRotation, parentTransform.eulerAngles.y, 0);

            // ���ʐ��`��Ԃ��g���ĉ�]�����X�Ƀ^�[�Q�b�g�Ɍ�����
            targetTransform.rotation = Quaternion.Lerp(targetTransform.rotation, limitedRotation, 0.05f);
        }
    }
}
