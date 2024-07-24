using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPassingByManager : MonoBehaviour {
    [SerializeField] private GameObject _enemyPassingByRight;
    [SerializeField] private GameObject _enemyPassingByLeft;
    [SerializeField] private float[] _timeToPopEnemy;
    private EnemiesPassingThroughMoveSpline _enemiesPassingThroughMoveSplineRight;
    private EnemiesPassingThroughMoveSpline _enemiesPassingThroughMoveSplineLeft;

    private int _index = 0;
    private float _coutTime = 0f;

    // Start is called before the first frame update
    void Start() {
        _enemiesPassingThroughMoveSplineRight = _enemyPassingByRight.GetComponent<EnemiesPassingThroughMoveSpline>();
        _enemiesPassingThroughMoveSplineLeft = _enemyPassingByLeft.GetComponent<EnemiesPassingThroughMoveSpline>();
    }

    // Update is called once per frame
    void Update() {
        _coutTime += Time.deltaTime;

        if (_index < _timeToPopEnemy.Length && _coutTime >= _timeToPopEnemy[_index]) {
            if (!_enemyPassingByRight.activeSelf) {
                _enemyPassingByRight.SetActive(true);
            }

            if (!_enemyPassingByLeft.activeSelf) {
                _enemyPassingByLeft.SetActive(true);
            }

            _enemiesPassingThroughMoveSplineRight.StartMoving();
            _enemiesPassingThroughMoveSplineLeft.StartMoving();
            _index++;
        }
    }
}
