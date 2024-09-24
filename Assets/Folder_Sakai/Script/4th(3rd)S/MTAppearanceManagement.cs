using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class MTAppearanceManagement : MonoBehaviour {
    // �X�|�[���|�C���g
    [SerializeField] private Transform[] _spawnPoint;
    // �X�|�[�����邩�̔��� (true�ŃX�|�[���\�Afalse�ŃX�|�[���ς�)
    [SerializeField] private bool[] _spawnJudge;
    // �������郂���X�^�[�g���b�N�̃v���n�u�i�����̃I�u�W�F�N�g�j
    [SerializeField] private GameObject[] _monsterTruck;
    [SerializeField] private ExplosionPoolManager _explosionPoolManager;

    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _audioClip;
    [SerializeField]
    private AudienceGaugeManager _audienceGaugeManager;
    [SerializeField]
    private GameObject[] _starUI;
    [SerializeField]
    private TextMeshProUGUI _text;
    [SerializeField]
    private TextMeshProUGUI _title;

    private ControllerBuruBuru _controller;

    private void Update() {
        NumberOfUnitsCounted();
    }
    // �X�|�[�����Ǘ����郁�\�b�h
    public void MTSpawn(int numberOfGeneration) {
        print(numberOfGeneration);
        StartCoroutine(StarUP(numberOfGeneration));
        for (int i = 0; i < numberOfGeneration; i++) {
            SEPlay();
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
    private IEnumerator StarUP(int starCount) {
        for (int i = 0; i < starCount; i++) {
            int ramdam = Random.Range(0, _starUI.Length);
            if (_starUI[ramdam].activeSelf) {
                i--;
            } else {
                yield return new WaitForSeconds(0.1f);  // 0.1�b�҂�
                _starUI[ramdam].SetActive(true);
            }
        }
    }

    // �������g�p���������X�^�[�g���b�N�����Ǘ�
    public void MultiplicationMTSpawn(int numberOfGeneration) {
        int activeCount = 0;

        // _spawnJudge �̒��� false�i�A�N�e�B�u�ȃI�u�W�F�N�g�j�̐��𐔂���
        foreach (bool judge in _spawnJudge) {
            if (!judge) {
                activeCount++;
            }
        }

        // �����ƃA�N�e�B�u�I�u�W�F�N�g�̐����|��������
        int multiplicationResult = numberOfGeneration * activeCount;

        // �|�������ʂ���A�ēx�A�N�e�B�u�ȃI�u�W�F�N�g�̐�������������
        int finalResult = multiplicationResult - activeCount;

        Debug.Log("�|��������: " + multiplicationResult + ", ��������̍ŏI����: " + finalResult);

        MTSpawn(finalResult);
    }

    // DivisionMTReduce ���\�b�h
    public void DivisionMTReduce(int numberOfReduce) {
        // _spawnJudge �̒��� false�i�A�N�e�B�u�ȃI�u�W�F�N�g�j�̐��𐔂���
        int activeCount = 0;
        foreach (bool judge in _spawnJudge) {
            if (!judge) {
                activeCount++;
            }
        }

        // �A�N�e�B�u�ȃI�u�W�F�N�g�̐������� numberOfReduce �Ŋ��� (�ϐ�A)
        if (numberOfReduce != 0) { // 0�Ŋ��邱�Ƃ�h�����߂̃`�F�b�N
            int variableA = (int)activeCount / numberOfReduce;
            Debug.Log("�ϐ�A: " + variableA);
          
            int finalResult = activeCount - variableA;

            MTReduce(finalResult);

            Debug.Log("�ŏI����: " + finalResult);
        } else {
            Debug.LogWarning("numberOfReduce �� 0 �ł��B0 �Ŋ��邱�Ƃ͂ł��܂���B");
        }
    }

    // ��Q���ɐڐG����MT�̏ꏊ�𐶐��ɂ��郁�\�b�h
    public void MTTriggerObstacles(int index) {
        _spawnJudge[index] = true;
    }

    // �����珇�� _spawnJudge �� false �� true �ɂ��āA�Ή����郂���X�^�[�g���b�N���A�N�e�B�u��
    public void MTReduce(int numberOfReduce) {

        print("��΂ꂽ");
        int numberOfTimesToReduce = Mathf.Abs(numberOfReduce);
        int reducedCount = 0;

        // �z��̉��i�傫���C���f�b�N�X�j���烋�[�v����
        for (int i = _spawnJudge.Length - 1; i >= 0; i--) {
            if (!_spawnJudge[i]) {
                _spawnJudge[i] = true;
                _explosionPoolManager.StartExplosion(_spawnPoint[i].transform);
                if (_controller == null) {
                    _controller = ControllerBuruBuru.Instance;
                }
                _controller.StartVibration();
                _monsterTruck[i].SetActive(false);
                reducedCount++;

                if (reducedCount >= numberOfTimesToReduce) {
                    break;
                }
            }
        }
    }
    private void SEPlay() {
        _audioSource.Stop();
        _audioSource.PlayOneShot(_audioClip);
    }

    private void NumberOfUnitsCounted() {

        int activeCount = 0;

        // _spawnJudge �̒��� false�i�A�N�e�B�u�ȃI�u�W�F�N�g�j�̐��𐔂���
        foreach (bool judge in _spawnJudge) {
            if (!judge) {
                activeCount++;
                print(activeCount);
            }
        }
        _scoreManager.InputGetStarScore(activeCount, 49);
        _text.text = activeCount.ToString()+" / "+49;
        _title.text = "Count";
        _audienceGaugeManager.SetScoreValue(activeCount, 49, "Count");
    }
}
