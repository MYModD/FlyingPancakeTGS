using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStartAppearingTriggerPoint : MonoBehaviour
{
    #region 変数
    [Header("敵出現管理インデックス"),SerializeField] private int _occurrenceManagementIndex;
    [SerializeField, Tag] string _player = default;
    [SerializeField] EnemyAppearanceManager _enemyAppearanceManager;
    #endregion

    #region 処理部分

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(_player))
        {
            print("col");
            print(_occurrenceManagementIndex);
            _enemyAppearanceManager.GetEnemyAppearanceManagementValue(_occurrenceManagementIndex);
        }
    }
    #endregion
}
