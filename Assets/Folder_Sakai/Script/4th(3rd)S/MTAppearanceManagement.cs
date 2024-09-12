using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MTAppearanceManagement : MonoBehaviour {
    // �X�|�[���|�C���g
    [SerializeField] private Transform[] _spawnPoint;
    // �X�|�[�����邩�̔��� (true�ŃX�|�[���\�Afalse�ŃX�|�[���ς�)
    [SerializeField] private bool[] _spawnJudge;
    // �������郂���X�^�[�g���b�N�̃v���n�u�i�����̃I�u�W�F�N�g�j
    [SerializeField] private GameObject[] _monsterTruck;
    [SerializeField] private ExplosionPoolManager _explosionPoolManager;

    // �X�|�[�����Ǘ����郁�\�b�h
    public void MTSpawn(int numberOfGeneration) {

        print(numberOfGeneration);
        for (int i = 0; i < numberOfGeneration; i++) {
            for (int j = 0; j < _spawnJudge.Length; j++) {
                if (_spawnJudge[j]) {
                    _monsterTruck[j].transform.position = _spawnPoint[j].position;
                    _monsterTruck[j].transform.rotation = _spawnPoint[j].rotation;
                    _monsterTruck[j].SetActive(true);
                    _spawnJudge[j] = false;
                    break;
                }
            }
        }
    }

    // ��Q���ɐڐG����MT�̏ꏊ�𐶐��ɂ��郁�\�b�h
    public void MTTriggerObstacles(int index) {
        _spawnJudge[index] = true;
    }

    // �����珇�� _spawnJudge �� false �� true �ɂ��āA�Ή����郂���X�^�[�g���b�N���A�N�e�B�u��
    public void MTReduce(int numberOfReduce) {

        int numberOfTimesToReduce = Mathf.Abs(numberOfReduce);
        int reducedCount = 0;
        // �z��̉��i�傫���C���f�b�N�X�j���烋�[�v����
        for (int i = _spawnJudge.Length - 1; i >= 0; i--) {
            // _spawnJudge �� false �Ȃ�
            if (!_spawnJudge[i]) {

                // _spawnJudge �� true �ɕύX
                _spawnJudge[i] = true;

                _explosionPoolManager.StartExplosion(_spawnPoint[i].transform);
                // �Ή����郂���X�^�[�g���b�N���A�N�e�B�u��
                _monsterTruck[i].SetActive(false);

                // ���_�N�V�����������𑝉�
                reducedCount++;

                // �w�肳�ꂽ���ɒB������I��
                if (reducedCount >= numberOfTimesToReduce) {
                    break;
                }
            }
        }
    }
}
