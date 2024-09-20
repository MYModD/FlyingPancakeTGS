using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaMissile : MonoBehaviour {
    [SerializeField]
    public Transform _player;

    [SerializeField]
    [Header("�������x")]
    private float _initialSpeed = 10f;

    [SerializeField]
    [Header("�ŏ����x")]
    private float _minSpeed = 2f;

    [SerializeField]
    [Header("�~�T�C���̑��x�ω����J�n�����ő勗��")]
    private float _maxDistance = 20f;

    [SerializeField]
    [Range(0.1f, 1f)]
    [Header("������")]
    private float _deceleration = 0.5f;

    public float _explosionScale;

    [SerializeField, Header("�v���C���[�̃~�T�C���^�O")]
    [Tag]
    private string _playerMissileTag;

    [ReadOnly]
    public ExplosionPoolManager _explosionPool;

    private Vector3 _startLocalPosition;
    private float _currentSpeed;
    private Transform _parentTransform;

    private void OnEnable() {
        if (_player == null) {
            Debug.LogWarning("�v���C���[���ݒ肳��Ă��܂���I");
        }
        _parentTransform = transform.parent;
        _startLocalPosition = transform.localPosition;
        _currentSpeed = _initialSpeed;
    }

    private void Update() {
        if (_player == null || _parentTransform == null) {
            Debug.LogWarning("�v���C���[�܂��͐e�I�u�W�F�N�g���ݒ肳��Ă��܂���I");
            return;
        }

        // �v���C���[�̕����ւ̃��[�J���x�N�g�����v�Z
        Vector3 localDirectionToPlayer = _parentTransform.InverseTransformPoint(_player.position) - transform.localPosition;
        localDirectionToPlayer.Normalize();

        // ���݂̃��[�J�������Ɋ�Â��đ��x�𒲐�
        float localDistanceToPlayer = Vector3.Distance(transform.localPosition, _parentTransform.InverseTransformPoint(_player.position));
        //Debug.Log($"�v���C���[�̋��� : {localDistanceToPlayer}");
        float t = Mathf.Clamp01(localDistanceToPlayer / _maxDistance);
        _currentSpeed = Mathf.Lerp(_minSpeed, _initialSpeed, t);

        // �~�T�C�������[�J�����W�ňړ�
        transform.localPosition += localDirectionToPlayer * _currentSpeed * Time.deltaTime;

        // �v���C���[�̕��������i���[���h���W�n�Łj
        transform.LookAt(_player);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(_playerMissileTag)) {
            _explosionPool.StartExplosionScale(this.transform, _explosionScale);
            gameObject.SetActive(false);
            other.gameObject.SetActive(false);
        }
    }
}