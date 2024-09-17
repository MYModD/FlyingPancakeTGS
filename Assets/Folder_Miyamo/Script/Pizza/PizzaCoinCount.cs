using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System;
using Unity.Mathematics;

public class PizzaCoinCount : MonoBehaviour {
    [Header("�^�O�ݒ�")]
    [SerializeField, Tag]
    private string _pizzaTag;
    [SerializeField, Tag]
    private string _pizzaLeftArmTag;
    [SerializeField, Tag]
    private string _enemyTag;
    [SerializeField, Tag]
    public string _pizzaManTagEnemy;

    [Header("�s�U�R�C���ݒ�")]
    [SerializeField]
    private int _pizzaCount = 0;
    [SerializeField]
    [Header("���̃X�e�[�W�ɐi�ނ��߂ɕK�v�ȃR�C����")]
    private float _maxPizzaCoin;

    [Header("UI�ݒ�")]
    public TextMeshProUGUI _text;

    [Header("��莞�Ԃ��ƂɌ������邻�̎���")]
    [SerializeField, Range(0, 3f)]
    public float _decreaseInterval = 1f; // ��������Ԋu�i�b�j
    [SerializeField]
    [Header("��x�Ɍ��������")]
    public int _decreaseAmount = 1;

    public AudioSource _audioPizza;

    private float _lastDecreaseTime;

    [Header("�v���C���[�Q��")]
    public PizzaMan _pizzaMan;

    public RedDamageEffect _redDamage;

    public float _damageDuration = 1.1f;

    public TestLockOnManager _lockOn;

    void Start() {
        _lastDecreaseTime = Time.time;
    }
    private void OnEnable() {
        _text.text = ""+0;
    }
    private void OnTriggerEnter(Collider other) {
        Debug.Log($"�Ԃ�������� : {other.gameObject.name}");
        if (other.CompareTag(_pizzaTag)) {
            _pizzaCount++;

            float pitchRandom = UnityEngine.Random.Range(-0.05f, 0.05f);
            Debug.Log($"�����_���l : {pitchRandom}");

            _audioPizza.pitch = 1f -pitchRandom;
            _audioPizza.Play();
            other.gameObject.SetActive(false);
            UpdatePizzaCountText();
            //��萔�B������s�U�}���̃^�O���G�ɕς��X�N���v�g
            if (_pizzaCount >= _maxPizzaCoin) {
                _pizzaMan.tag = _pizzaManTagEnemy;
            }
        }
        if (other.CompareTag(_enemyTag)) {
            // �����Ƀ~�T�C�������������Ƃ����炷
            _pizzaCount = Mathf.Max(0, _pizzaCount - _decreaseAmount);

            // �����v����
            _redDamage.PlayerDamage();
            _lockOn.AddBlackList(other.transform);
            other.gameObject.SetActive(false);

        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag(_pizzaLeftArmTag)) {
            if (Time.time - _lastDecreaseTime >= _decreaseInterval) {
                _pizzaCount = Mathf.Max(0, _pizzaCount - _decreaseAmount);
                _lastDecreaseTime = Time.time;
                UpdatePizzaCountText();
                _redDamage.PlayerDamage();



            }
        }
    }


   

    private void UpdatePizzaCountText() {
        _text.text = _pizzaCount.ToString();
    }
}