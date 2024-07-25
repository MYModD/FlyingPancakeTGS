using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPassingByManager : MonoBehaviour {
    [SerializeField] private GameObject _enemyPassingByRight;
    [SerializeField] private GameObject _enemyPassingByLeft;
    [SerializeField] private float[] _timeToPopEnemy;
    private EnemyMoveSpline _enemyMoveSplineRigft;
    private EnemyMoveSpline _enemyMoveSplineLeft;

    private int _index = 0;
    private float _coutTime = 0f;

    // Start is called before the first frame update
    void Start() {
        _enemyMoveSplineRigft = _enemyPassingByRight.GetComponent<EnemyMoveSpline>();
        _enemyMoveSplineLeft = _enemyPassingByLeft.GetComponent<EnemyMoveSpline>();
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

            _enemyMoveSplineRigft.StartMoving();
            _enemyMoveSplineLeft.StartMoving();
            _index++;
        }
    }
}
