using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTruckActivation : MonoBehaviour {
    #region �ϐ�

    [SerializeField] private GameObject[] _monsterTrucksGroup1;
    [SerializeField] private GameObject[] _monsterTrucksGroup2;
    [SerializeField] private GameObject[] _monsterTrucksGroup3;
    [SerializeField] private GameObject[] _monsterTrucksGroup4;

    [SerializeField] private float _activationInterval = 2.0f; // �A�N�e�B�u���̊Ԋu�i�b�j

    #endregion

    #region ���\�b�h

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

            // �w�肳�ꂽ�b���ҋ@
            yield return new WaitForSeconds(_activationInterval);
        }
    }

    #endregion
}
