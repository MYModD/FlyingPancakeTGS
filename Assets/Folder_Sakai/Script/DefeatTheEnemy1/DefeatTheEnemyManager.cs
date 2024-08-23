using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatTheEnemyManager : MonoBehaviour {
    #region �ϐ�

    //public enum Proposition {
    //    ��`��K�v�ɉ����Ēǉ�
    //}

    [SerializeField] private List<GameObject> _firstGroupaircraft; // ���j�ڕW�@�̃��X�g���Q
    [SerializeField] private List<GameObject> _secondGroupaircraft; // ���j�ڕW�@�̃��X�g���Q
    [SerializeField] private float _firstGroupmoveSpeed; // ���Q�̈ړ����x
    [SerializeField] private float _secondGroupmoveSpeed; // ���Q�̈ړ����x
    [SerializeField] private CountTheNumberOfDefeats _countTheNumberOfDefeats;

    private int _score;
    #endregion

    #region ���\�b�h

    /// <summary>
    /// ���j�ڕW�@�̑��Q���A�N�e�B�u�����ăX�^�[�g�����郁�\�b�h
    /// </summary>
    public void FirstGroupStartMoving() {
        foreach (GameObject aircraft in _firstGroupaircraft) {
            // �@�̂��A�N�e�B�u��
            aircraft.SetActive(true);

            // �e�@�̂�MoveSpline�R���|�[�l���g���擾
            MoveSpline moveSpline = aircraft.GetComponent<MoveSpline>();

            if (moveSpline != null) {
                // �擾����MoveSpline�R���|�[�l���g�ő��x��ύX����
                moveSpline.ChageSpeed(_firstGroupmoveSpeed);
            }
        }
    }

    public void SecondGroupStartMoving() {

        foreach (GameObject aircraft in _secondGroupaircraft) {
            //// �@�̂��A�N�e�B�u��
            //aircraft.SetActive(true);

            // �e�@�̂�MoveSpline�R���|�[�l���g���擾
            MoveSpline moveSpline = aircraft.GetComponent<MoveSpline>();

            if (moveSpline != null) {
                // �擾����MoveSpline�R���|�[�l���g�ő��x��ύX����
                moveSpline.ChageSpeed(_secondGroupmoveSpeed);
            }
        }
    }

    public void ScoreCalculation() {

        _score = _countTheNumberOfDefeats.NumberOfDefeats();
    }
    #endregion
}
