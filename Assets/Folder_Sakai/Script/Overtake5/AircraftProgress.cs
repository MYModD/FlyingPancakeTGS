// ---------------------------------------------------------
// AircraftProgress.cs
//
// �쐬��:
// �쐬��:
// ---------------------------------------------------------
using UnityEngine;
using System.Collections;
public class AircraftProgress : MonoBehaviour {
    #region �ϐ�
    //[SerializeField] private RankManager_RS _raceManager;

    private int _currentWaypoint = 0; // ���݂̃E�F�C�|�C���g�̃C���f�b�N�X
    private int _goalIndex = default; // �S�[���̃C���f�b�N�X
    private bool _isJustOnce = false; // 1�x�����Ăяo��

    #endregion
    #region ���\�b�h
    /// <summary>
    /// �X�V�O����
    /// </summary>
    void Start() {
        //�S�[���n�_�̃C���f�b�N�X��0�I���W�����l�����Đݒ�
        //_goalIndex = _raceManager.GetWayPointCount() - 1;
    }


    /// <summary>
    /// �e�@�̂̃`�F�b�N�|�C���g�̐i���Ǘ�
    /// </summary>
    void OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag("RankCheckpoint")) {

            RankManagementWayPoint rankManagementWayPoint = other.GetComponent<RankManagementWayPoint>();

            if (rankManagementWayPoint != null) {

                //�e�E�F�C�|�C���g���ێ����Ă���C���f�b�N�X���擾�����݂�_currentWaypoint�ŕێ����Ă���
                //�l�Ɠ����������ꍇ�C���N�������g���������Ď��̖ڕW���X�V
                int wayPointIndex = rankManagementWayPoint.WayPointIndex();

                if (wayPointIndex == _currentWaypoint) {

                    _currentWaypoint++;
                }

                ////wayPointIndex���S�[����������Ă���I�u�W�F�N�g�̃C���f�b�N�X�ɒB�����ꍇ
                ////���ʂ̊m�菈�����s��
                //if (wayPointIndex == _goalIndex) {

                //    if (!_isJustOnce) {
                //        _currentWaypoint += _raceManager.RankingDetermination();
                //        _isJustOnce = true;
                //    }

                //    //�S�[�������I�u�W�F�N�g���v���C���[�I�u�W�F�N�g�̏ꍇ���U���g�\���p�̃����L���O��
                //    //�쐬����
                //    if (this.gameObject.tag == "Player") {
                //        _raceManager.RankingGeneration();
                //    }
                //}
            }
        }

    }

    /// <summary>
    /// ���ݒʉ߂��I�����Ō�̃E�F�C�|�C���g�̃C���f�b�N�X
    /// </summary>
    public int CurrentWaypoint() {

        return _currentWaypoint;
    }

    #endregion
}