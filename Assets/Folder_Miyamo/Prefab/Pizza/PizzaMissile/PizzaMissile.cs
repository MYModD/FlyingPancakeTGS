using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaMissile : MonoBehaviour {
    [SerializeField]
    public Transform _player;

    [SerializeField]
    [Header("初期速度")]
    private float _initialSpeed = 10f;

    [SerializeField]
    [Header("最小速度")]
    private float _minSpeed = 2f;

    [SerializeField]
    [Header("ミサイルの速度変化が開始される最大距離")]
    private float _maxDistance = 20f;

    [SerializeField]
    [Range(0.1f, 1f)]
    [Header("減速率")]
    private float _deceleration = 0.5f;

    public float _explosionScale;

    [SerializeField, Header("プレイヤーのミサイルタグ")]
    [Tag]
    private string _playerMissileTag;

    [ReadOnly]
    public ExplosionPoolManager _explosionPool;

    private Vector3 _startLocalPosition;
    private float _currentSpeed;
    private Transform _parentTransform;

    private void OnEnable() {
        if (_player == null) {
            Debug.LogWarning("プレイヤーが設定されていません！");
        }
        _parentTransform = transform.parent;
        _startLocalPosition = transform.localPosition;
        _currentSpeed = _initialSpeed;
    }

    private void Update() {
        if (_player == null || _parentTransform == null) {
            Debug.LogWarning("プレイヤーまたは親オブジェクトが設定されていません！");
            return;
        }

        // プレイヤーの方向へのローカルベクトルを計算
        Vector3 localDirectionToPlayer = _parentTransform.InverseTransformPoint(_player.position) - transform.localPosition;
        localDirectionToPlayer.Normalize();

        // 現在のローカル距離に基づいて速度を調整
        float localDistanceToPlayer = Vector3.Distance(transform.localPosition, _parentTransform.InverseTransformPoint(_player.position));
        //Debug.Log($"プレイヤーの距離 : {localDistanceToPlayer}");
        float t = Mathf.Clamp01(localDistanceToPlayer / _maxDistance);
        _currentSpeed = Mathf.Lerp(_minSpeed, _initialSpeed, t);

        // ミサイルをローカル座標で移動
        transform.localPosition += localDirectionToPlayer * _currentSpeed * Time.deltaTime;

        // プレイヤーの方を向く（ワールド座標系で）
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