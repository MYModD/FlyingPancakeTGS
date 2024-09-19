using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaCoinInstance : MonoBehaviour {
    // �z�u�ʒu�̔z��
    public Transform _instantiateLeftPosition;
    public Transform _instantiateRightPosition;
    // �R�C���I�u�W�F�N�g�̔z��
    [Header("�Z����ƒ��������")]
    public GameObject[] _leftCoins;
    [Header("�Z��������Ȃ���")]
    public GameObject _rightCoin;


    public Transform _instancePearent;
    // �R�C�������Ԋu
    public float _leftCoinDuration;
    public float _rightCoinDuration;
    [Header("���~�G�����猩�č����o��m��  ��������70")]
    public float _leftProbability = 70;
    // �^�C�}�[
    private float _leftTimer;
    private float _rightTimer;

    private void OnEnable() {
        _leftTimer = _leftCoinDuration;
        _rightTimer = _rightCoinDuration;
    }

    void Update() {
        _leftTimer -= Time.deltaTime;
        _rightTimer -= Time.deltaTime;

        // ���̃R�C������
        if (_leftTimer <= 0) {
            if (Random.Range(0, 100) < _leftProbability) {
                GameObject obj = Instantiate(_leftCoins[Random.Range(0, _leftCoins.Length)], _instancePearent);
                obj.transform.position = _instantiateLeftPosition.position;
            }
            _leftTimer = _leftCoinDuration;
        }

        // �E�̃R�C������
        if (_rightTimer <= 0) {
            GameObject obj = Instantiate(_rightCoin, _instancePearent);
            obj.transform.position = _instantiateRightPosition.position;
            _rightTimer = _rightCoinDuration;
        }
    }
}