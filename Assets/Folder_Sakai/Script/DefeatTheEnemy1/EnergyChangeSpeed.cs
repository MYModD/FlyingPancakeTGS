using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyChangeSpeed : MonoBehaviour
{

    #region �ϐ�
    [SerializeField] private Transform _player;
    [SerializeField] private float _changeSpeedDistance;
    [SerializeField] private EnemyMoveSpline _enemyMoveSpline;
    #endregion
    #region ���\�b�h

    private void Update() {
       
            CalculateDistance();
        
    }

    private void CalculateDistance() {
        if (this.gameObject != null && _player != null) {
            // �������v�Z
            float distance = Vector3.Distance(this.gameObject.transform.position,_player.position);

            //// ���������O�ɕ\��
            //Debug.Log($"�v���C���[�ƓG�@�̋���: {distance}{this.gameObject.name}");

            if (_changeSpeedDistance <= distance) {
                _enemyMoveSpline.ChangeSpeed();
            }
        }
    }
    #endregion
}
