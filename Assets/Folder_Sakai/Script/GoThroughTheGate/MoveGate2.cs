using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGate2 : MonoBehaviour
{
    #region 変数
    [SerializeField, Tooltip("目的地となるターゲット1")]
    private Transform _target1;

    [SerializeField, Tooltip("目的地となるターゲット2")]
    private Transform _target2;

    [SerializeField, Tooltip("移動速度")]
    private float _speed = 150f;

    [SerializeField, Tooltip("到達とみなす距離")]
    private float _stoppingDistance = 5f;

    private bool _isMoving1 = false;
    private bool _isMoving2 = false;
    #endregion

    #region メソッド

    private void Start() {
        DetachChild();
    }

    void Update() {

        if (_isMoving1) {
            MoveTowardsTarget1();

        } else if (!_isMoving1 && _isMoving2) {
            MoveTowardsTarget2();
        }
    }

    // 目的地に向かって移動する処理
    private void MoveTowardsTarget1() {
        if (_target1 == null) {
            Debug.LogWarning("ターゲットが設定されていません。");
            return;
        }

        // ターゲットまでの距離を計算
        float distanceToTarget = Vector3.Distance(transform.position, _target1.position);

        // ターゲットとの距離が停止距離よりも大きい場合にのみ移動する
        if (distanceToTarget > _stoppingDistance) {
            // 現在の位置とターゲットの位置の差分を計算
            Vector3 direction = (_target1.position - transform.position).normalized;

            // フレームごとに少しずつ移動させる
            transform.position += direction * _speed * Time.deltaTime;
        } else {
            // 目的地に非常に近づいた場合、位置をターゲットの位置にスナップして停止
            print("ついたぜよ");
            transform.position = _target1.position;
            _isMoving1 = false;
            _isMoving2 = true;
        }
    }

    private void MoveTowardsTarget2() {
        if (_target2 == null) {
            Debug.LogWarning("ターゲットが設定されていません。");
            return;
        }

        // ターゲットまでの距離を計算
        float distanceToTarget = Vector3.Distance(transform.position, _target2.position);

        // ターゲットとの距離が停止距離よりも大きい場合にのみ移動する
        if (distanceToTarget > _stoppingDistance) {
            // 現在の位置とターゲットの位置の差分を計算
            Vector3 direction = (_target2.position - transform.position).normalized;

            // フレームごとに少しずつ移動させる
            transform.position += direction * _speed * Time.deltaTime;
        } else {
            // 目的地に非常に近づいた場合、位置をターゲットの位置にスナップして停止
            transform.position = _target2.position;
 
            _isMoving2 = false;
        }
    }

    // 指定した秒数待機後に移動を開始するメソッド
    public void StartMovingWithDelay(float delaySeconds) {
        StartCoroutine(DelayedStartMoving(delaySeconds));
    }

    // コルーチンで指定した秒数待つ処理
    private IEnumerator DelayedStartMoving(float delaySeconds) {
        yield return new WaitForSeconds(delaySeconds);
        _isMoving1 = true;
    }

    // 目的地を設定するメソッド
    public void SetTarget(Transform newTarget) {
        _target1 = newTarget;
        _isMoving1 = false;  // 新しいターゲットを設定したときは停止状態にしておく
    }

    private void DetachChild() {

        // 子オブジェクトの親子関係を解除
        if (_target1 != null) {
            _target1.SetParent(null);
        }
        if (_target2 != null) {
            _target2.SetParent(null);
        }
    }

    #endregion
}
