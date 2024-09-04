using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaunchManagement : MonoBehaviour {
    [SerializeField] private TestEnemyMissilePoolManger _testEnemyMissilePoolManger;

    [SerializeField] private EnemyBulletPoolManger[] _enemyBulletPoolManger;

    [SerializeField] private float _time = 5f;  // 5�b���Ƃ�print���鎞�ԊԊu
    [SerializeField] private float _time2 = 0.1f;  // 5�b���Ƃ�print���鎞�ԊԊu

    private float _elapsedTime = 0f;  // �o�ߎ��Ԃ�ǐՂ��邽�߂̃t�B�[���h
    private float _elapsedTime2 = 0f;  // �o�ߎ��Ԃ�ǐՂ��邽�߂̃t�B�[���h
    private int _index = 0;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void FixedUpdate() {
        //// �~�T�C���𔭎˂��邽�߂̓��̓`�F�b�N
        //if (Input.GetKeyDown(KeyCode.P)) {
        //    _testEnemyMissilePoolManger.EnemyFireMissile();
        //}

        // �o�ߎ��Ԃ��X�V
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

            // �o�ߎ��Ԃ����Z�b�g
            _elapsedTime2 = 0f;
        }
        
        // �o�ߎ��Ԃ��X�V
        _elapsedTime += Time.deltaTime;

        // �w�肳�ꂽ���ԊԊu�ɒB�������ǂ������`�F�b�N
        if (_elapsedTime >= _time) {

            _index += 2;
            if (_index == 6) {
                _index = 0;
            }

            // �o�ߎ��Ԃ����Z�b�g
            _elapsedTime = 0f;
        }
    }
}
