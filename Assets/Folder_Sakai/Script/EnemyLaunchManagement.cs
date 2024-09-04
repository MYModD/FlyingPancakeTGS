using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaunchManagement : MonoBehaviour {
    [SerializeField] private TestEnemyMissilePoolManger _testEnemyMissilePoolManger;

    [SerializeField] private EnemyBulletPoolManger[] _enemyBulletPoolManger;

    [SerializeField] private float _time = 5f;  // 5秒ごとにprintする時間間隔
    [SerializeField] private float _time2 = 0.1f;  // 5秒ごとにprintする時間間隔

    private float _elapsedTime = 0f;  // 経過時間を追跡するためのフィールド
    private float _elapsedTime2 = 0f;  // 経過時間を追跡するためのフィールド
    private int _index = 0;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void FixedUpdate() {
        //// ミサイルを発射するための入力チェック
        //if (Input.GetKeyDown(KeyCode.P)) {
        //    _testEnemyMissilePoolManger.EnemyFireMissile();
        //}

        // 経過時間を更新
        _elapsedTime2 += Time.deltaTime;

        if (_elapsedTime2 >= _time2) {

            switch (_index) {
                case 0:
                    _enemyBulletPoolManger[_index].EnemyFireBullet();
                    _enemyBulletPoolManger[_index + 1].EnemyFireBullet();
                    break;
                case 2:
                    _enemyBulletPoolManger[_index].EnemyFireBullet();
                    _enemyBulletPoolManger[_index + 1].EnemyFireBullet();
                    break;
                case 4:
                    _enemyBulletPoolManger[_index].EnemyFireBullet();
                    _enemyBulletPoolManger[_index + 1].EnemyFireBullet();
                    _enemyBulletPoolManger[_index + 2].EnemyFireBullet();
                    break;
            }

            // 経過時間をリセット
            _elapsedTime2 = 0f;
        }
        
        // 経過時間を更新
        _elapsedTime += Time.deltaTime;

        // 指定された時間間隔に達したかどうかをチェック
        if (_elapsedTime >= _time) {

            _index += 2;
            if (_index == 6) {
                _index = 0;
            }

            // 経過時間をリセット
            _elapsedTime = 0f;
        }
    }
}
