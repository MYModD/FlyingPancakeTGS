using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteEnemyManager : MonoBehaviour {
    [SerializeField] private GameObject[] _eliteEnemys;
    private int _managedEnemyIndex;
    private float _stopTime;

    public void SetStoppedAircraftAndTime(int index, float stopTime) {
        _managedEnemyIndex = index;
        _stopTime = stopTime;
        StopEnemyObj(); // Call the method to stop the enemy
    }

    private void StopEnemyObj() {
        if (_managedEnemyIndex < 0 || _managedEnemyIndex >= _eliteEnemys.Length) {
            Debug.LogError("Invalid enemy index.");
            return;
        }

        StartCoroutine(StopAndResumeEnemy());
    }

    private IEnumerator StopAndResumeEnemy() {
        _eliteEnemys[_managedEnemyIndex].SetActive(false);
        yield return new WaitForSeconds(_stopTime);
        _eliteEnemys[_managedEnemyIndex].SetActive(true);
    }
}
