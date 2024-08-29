using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTruckActivation : MonoBehaviour {
    #region 変数

    [SerializeField] private GameObject[] _monsterTrucksGroup1;
    [SerializeField] private GameObject[] _monsterTrucksGroup2;
    [SerializeField] private GameObject[] _monsterTrucksGroup3;
    [SerializeField] private GameObject[] _monsterTrucksGroup4;

    [SerializeField] private float _activationInterval = 2.0f; // アクティブ化の間隔（秒）

    #endregion

    #region メソッド

    void Start() {
        StartCoroutine(ActivateMonsterTrucks());
    }

    private IEnumerator ActivateMonsterTrucks() {
        int maxCount = Mathf.Max(
            _monsterTrucksGroup1.Length,
            _monsterTrucksGroup2.Length,
            _monsterTrucksGroup3.Length,
            _monsterTrucksGroup4.Length
        );

        for (int i = 0; i < maxCount; i++) {
            if (i < _monsterTrucksGroup1.Length) {
                _monsterTrucksGroup1[i].SetActive(true);
            }

            if (i < _monsterTrucksGroup2.Length) {
                _monsterTrucksGroup2[i].SetActive(true);
            }

            if (i < _monsterTrucksGroup3.Length) {
                _monsterTrucksGroup3[i].SetActive(true);
            }

            if (i < _monsterTrucksGroup4.Length) {
                _monsterTrucksGroup4[i].SetActive(true);
            }

            // 指定された秒数待機
            yield return new WaitForSeconds(_activationInterval);
        }
    }

    #endregion
}
