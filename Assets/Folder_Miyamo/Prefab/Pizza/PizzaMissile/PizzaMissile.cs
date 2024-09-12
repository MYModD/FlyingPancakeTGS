using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaMissile : MonoBehaviour {
    [SerializeField]
    public Transform _player;

    [SerializeField]
    //[Range(1f, 20f)]
    [Header("�������x")]
    private float _initialSpeed = 10f;

    [SerializeField]
    //[Range(0.1f, 5f)]
    [Header("�ŏ����x")]
    private float _minSpeed = 2f;

    [SerializeField]
    //[Range(10f, 50f)]
    [Header("�~�T�C���̑��x�ω����J�n�����ő勗��")]
    private float _maxDistance = 20f;

    [SerializeField]
    [Range(0.1f, 1f)]
    [Header("������")]
    private float _deceleration = 0.5f;

    [SerializeField, Header("�v���C���[�̃~�T�C���^�O")]
    [Tag]
    private string _playerMissileTag;
    [ReadOnly]
    public ExplosionPoolManager _explosionPool;

    private Vector3 _startPosition;
    private float _currentSpeed;

    private void OnEnable() {
        if (_player == null) {
            Debug.LogWarning("�v���C���[���ݒ肳��Ă��܂���I");
        }
        _startPosition = transform.position;
        _currentSpeed = _initialSpeed;
    }

    private void Update() {
        if (_player == null) {
            Debug.LogWarning("�v���C���[���ݒ肳��Ă��܂���I");
            return;
        }

        // �v���C���[�̕����ւ̃x�N�g�����v�Z
        Vector3 directionToPlayer = (_player.position - transform.position).normalized;

        // ���݂̋����Ɋ�Â��đ��x�𒲐�
        float distanceToPlayer = Vector3.Distance(transform.position, _player.position);
        float t = Mathf.Clamp01(distanceToPlayer / _maxDistance);
        _currentSpeed = Mathf.Lerp(_minSpeed, _initialSpeed, t);

        // �~�T�C�����ړ�
        transform.position += directionToPlayer * _currentSpeed * Time.deltaTime;

        // �v���C���[�̕�������
        transform.LookAt(_player);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(_playerMissileTag)) {

            _explosionPool.StartExplosion(this.transform);
            gameObject.SetActive(false);

        } 
    }
}