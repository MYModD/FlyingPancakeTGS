using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaMissile : MonoBehaviour {
    [SerializeField]
    private Transform _player;

    [SerializeField]
    //[Range(1f, 20f)]
    [Header("初期速度")]
    private float _initialSpeed = 10f;

    [SerializeField]
    //[Range(0.1f, 5f)]
    [Header("最小速度")]
    private float _minSpeed = 2f;

    [SerializeField]
    //[Range(10f, 50f)]
    [Header("最大距離")]
    private float _maxDistance = 20f;

    [SerializeField]
    [Range(0.1f, 1f)]
    [Header("減速率")]
    private float _deceleration = 0.5f;

    private Vector3 _startPosition;
    private float _currentSpeed;

    private void OnEnable() {
        if (_player == null) {
            Debug.LogWarning("プレイヤーが設定されていません！");
        }
        _startPosition = transform.position;
        _currentSpeed = _initialSpeed;
    }

    private void Update() {
        if (_player == null) {
            Debug.LogWarning("プレイヤーが設定されていません！");
            return;
        }

        // プレイヤーの方向へのベクトルを計算
        Vector3 directionToPlayer = (_player.position - transform.position).normalized;

        // 現在の距離に基づいて速度を調整
        float distanceToPlayer = Vector3.Distance(transform.position, _player.position);
        float t = Mathf.Clamp01(distanceToPlayer / _maxDistance);
        _currentSpeed = Mathf.Lerp(_minSpeed, _initialSpeed, t);

        // ミサイルを移動
        transform.position += directionToPlayer * _currentSpeed * Time.deltaTime;

        // プレイヤーの方を向く
        transform.LookAt(_player);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("PlayerMissile")) {
            // プレイヤーのミサイルに当たったら破壊
            Destroy(gameObject);
            Destroy(other.gameObject);
        } else if (other.CompareTag("Player")) {
            // プレイヤーに当たったらダメージ処理などを行う
            // ここにプレイヤーへのダメージ処理を追加
            Destroy(gameObject);
        }
    }
}