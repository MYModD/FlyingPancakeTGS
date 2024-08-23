using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeTheMonsterTruckManager : MonoBehaviour {
    #region �ϐ�

    [SerializeField] private List<GameObject> _monsterTrucks;

    #endregion

    #region ���\�b�h

    /// <summary>
    /// �w�肳�ꂽ�����X�^�[�g���b�N�����X�g���ɑ��݂���ꍇ�A�X�s�[�h��ݒ肷��
    /// </summary>
    /// <param name="monsterTruck">�Ώۂ̃����X�^�[�g���b�N</param>
    /// <param name="speed">�ݒ肷��X�s�[�h</param>
    public void StartMoving(GameObject monsterTruck, float speed) {
        foreach (GameObject truck in _monsterTrucks) {
            // monsterTruck�Ɠ����I�u�W�F�N�g���ǂ������`�F�b�N
            if (truck == monsterTruck) {
                // �R���|�[�l���g���擾���đ��x��ݒ肷��
                MoveSpline moveSpline = truck.GetComponent<MoveSpline>();

                if (moveSpline != null) {
                    moveSpline.ChageSpeed(speed);
                } 

                break; // ��v����g���b�N�����������烋�[�v���I��
            }
        }
    }


    #endregion
}
