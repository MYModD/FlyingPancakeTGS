using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankManagementWayPoint : MonoBehaviour
{
    #region �ϐ�
    [SerializeField] private int _wayPointIndex; // �E�F�C�|�C���g�̃C���f�b�N�X
    #endregion

    #region ���\�b�h
    public int WayPointIndex() {

        return _wayPointIndex;
    }
    #endregion
}
