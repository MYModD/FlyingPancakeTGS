using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStartAppearingTriggerPoint : MonoBehaviour
{
    #region �ϐ�
    [Header("�G�o���Ǘ��C���f�b�N�X"),SerializeField] private int _occurrenceManagementIndex;
    [SerializeField, Tag] private string _player = default;
    [SerializeField] private EnemyMoveSpline _enemyMoveSpline;
    [SerializeField] private EnemyAppearanceManager _enemyAppearanceManager;
    [SerializeField] private bool _isFrontEnemy = false;
    [SerializeField] private float _stopTime;
    #endregion

    #region ��������

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(_player))
        {     
            _enemyAppearanceManager.GetEnemyAppearanceManagementValue(_occurrenceManagementIndex);

            if (_isFrontEnemy) {

                _enemyMoveSpline.StartMovingDelay(_stopTime);
            } else {

                _enemyMoveSpline.StartMoving();
            }
            
        }
    }
    #endregion
}
