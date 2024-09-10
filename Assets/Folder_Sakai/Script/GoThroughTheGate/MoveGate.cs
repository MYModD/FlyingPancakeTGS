using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGate : MonoBehaviour {

    #region 変数
    [SerializeField, Tooltip("目的地となるターゲット")]
    private Transform _target;

    [SerializeField, Tooltip("移動速度")]
    private float _speed = 5f;

    [SerializeField, Tooltip("到達とみなす距離")]
    private float _stoppingDistance = 0.1f;

    private bool _isMoving = false;
    #endregion

    #region メソッド

    private void Start() {
        DetachChild();
    }

    void Update() {
        if (_isMoving) {
            MoveTowardsTarget();
        }
    }

    // 目的地に向かって移動する処理
    private void MoveTowardsTarget() {
        if (_target == null) {
            Debug.LogWarning("ターゲットが設定されていません。");
            return;
        }

        // ターゲットまでの距離を計算
        float distanceToTarget = Vector3.Distance(transform.position, _target.position);

        // ターゲットとの距離が停止距離よりも大きい場合にのみ移動する
        if (distanceToTarget > _stoppingDistance) {
            // 現在の位置とターゲットの位置の差分を計算
            Vector3 direction = (_target.position - transform.position).normalized;

            // フレームごとに少しずつ移動させる
            transform.position += direction * _speed * Time.deltaTime;
        } else {
            // 目的地に非常に近づいた場合、位置をターゲットの位置にスナップして停止
            transform.position = _target.position;
            _isMoving = false;  // 移動を停止
            Debug.Log("目的地に到達しました。");
        }
    }

    // 指定した秒数待機後に移動を開始するメソッド
    public void StartMovingWithDelay(float delaySeconds) {
        StartCoroutine(DelayedStartMoving(delaySeconds));
    }

    // コルーチンで指定した秒数待つ処理
    private IEnumerator DelayedStartMoving(float delaySeconds) {
        yield return new WaitForSeconds(delaySeconds);
        _isMoving = true;
    }

    // 目的地を設定するメソッド
    public void SetTarget(Transform newTarget) {
        _target = newTarget;
        _isMoving = false;  // 新しいターゲットを設定したときは停止状態にしておく
    }

    private void DetachChild() {
        // 子オブジェクトの親子関係を解除
        if (_target != null) {
            _target.SetParent(null);
        }
    }

    #endregion
}
