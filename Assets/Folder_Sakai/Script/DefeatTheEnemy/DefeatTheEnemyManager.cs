using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatTheEnemyManager : MonoBehaviour
{

    #region �ϐ�
    public enum Proposition {


    }

    [SerializeField] private List<GameObject> _firstGroupaircraft; // ���j�ڕW�@�̃��X�g���Q
    [SerializeField] private List<GameObject> _secondGroupaircraft; // ���j�ڕW�@�̃��X�g���Q
    [SerializeField] float _firstGroupmoveSpeed;
    #endregion
    #region�@���\�b�h
    public void FirstGroupStartMoving() {
        foreach (GameObject aircraft in _firstGroupaircraft) {
            // �e�@�̂�MoveSpline�R���|�[�l���g���擾
            MoveSpline moveSpline = aircraft.GetComponent<MoveSpline>();

            if (moveSpline != null) {
                // �擾����MoveSpline�R���|�[�l���g�ŉ��炩�̑�����s��
                moveSpline.ChageSpeed(_firstGroupmoveSpeed);
            }
        }
    }
    #endregion
}
