using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAppearanceManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemies = default;
    private int _enemyAppearanceManagementValue = default;

    public void GetEnemyAppearanceManagementValue(int Index)
    {
        _enemyAppearanceManagementValue = Index;
        print(_enemyAppearanceManagementValue);
        EnemyAppears();
    }

    private void EnemyAppears()
    {
        _enemies[_enemyAppearanceManagementValue].SetActive(true);
    }
}
