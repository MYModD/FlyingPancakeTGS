using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStartAppearingTriggerPoint : MonoBehaviour
{
    #region �ϐ�
    [Header("�G�o���Ǘ��C���f�b�N�X"),SerializeField] private int _occurrenceManagementIndex;
    [SerializeField, Tag] string _player = default;
    [SerializeField] EnemyAppearanceManager _enemyAppearanceManager;
    #endregion

    #region ��������

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
