using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAppearanceManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemies = default;
    private int _enemyAppearanceManagementValue = default;

    public void GetEnemyAppearanceManagementValue(int index)
    {
        _enemyAppearanceManagementValue = index;
        print(_enemyAppearanceManagementValue);
        EnemyAppears();
    }

    private void EnemyAppears()
    {
        _enemies[_enemyAppearanceManagementValue].SetActive(true);
    }
}
