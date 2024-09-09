using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStartAppearingTriggerPoint : MonoBehaviour
{
    #region 変数
    [Header("敵出現管理インデックス"),SerializeField] private int _occurrenceManagementIndex;
    [SerializeField, Tag] private string _player = default;
    [SerializeField] private EnemyMoveSpline _enemyMoveSpline;
    [SerializeField] private EnemyAppearanceManager _enemyAppearanceManager;
    [SerializeField] private bool _isFrontEnemy = false;
    [SerializeField] private float _stopTime;
    #endregion

    #region 処理部分

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
