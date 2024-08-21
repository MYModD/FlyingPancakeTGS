using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvertakeManager : MonoBehaviour {
    #region �ϐ�

    [SerializeField] private List<GameObject> _aircraft; // ���[�X�ɎQ������@�̃��X�g
    [SerializeField] float _moveSpeed;
    #endregion

    #region ���\�b�h

    public void StartMoving() {
        foreach (GameObject aircraft in _aircraft) {
            // �e�@�̂�MoveSpline�R���|�[�l���g���擾
            MoveSpline moveSpline = aircraft.GetComponent<MoveSpline>();

            if (moveSpline != null) {
                // �擾����MoveSpline�R���|�[�l���g�ŉ��炩�̑�����s��
                moveSpline.ChageSpeed(_moveSpeed);
            }
        }
    }

    #endregion
}
