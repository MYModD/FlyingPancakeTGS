using UnityEngine;
using Cysharp.Threading.Tasks; // UniTaskを使用するために必要
using System.Threading;
using System;

public class PizzaWaniWaniMissile : MonoBehaviour {
    [SerializeField] private Transform _startTransform;  // 開始位置
    [SerializeField] private Transform _endTransform;    // 目標位置
    [SerializeField, Range(0, 1f)] private float _lerpTForward = 0.5f;  // 行くときの速度
    [SerializeField, Range(0, 1f)] private float _lerpTBackward = 0.3f; // 戻るときの速度
    [SerializeField] private float _epsilon = 0.01f;     // 目標位置との誤差の許容範囲
    [SerializeField] private float _waitDuration = 2f;   // 停止時間
    [SerializeField] private float _damage = 10f;        // プレイヤーに与えるダメージ
    [SerializeField] private Camera _mainCamera;         // メインカメラ
    [SerializeField] private GameObject _damageOverlay;  // カメラを覆うオーバーレイオブジェクト（UIなど）

    private enum MissileState {
        Idle,
        MovingToTarget,
        StayingAtTarget,
        MovingBack
    }

    private MissileState _currentState = MissileState.Idle;

    private void Start() {
        transform.position = _startTransform.position; // 初期位置をスタート位置に設定
        if (_damageOverlay != null)
            _damageOverlay.SetActive(false); // 初期状態ではオーバーレイを非表示に
    }

    private void FixedUpdate() {
        switch (_currentState) {
            case MissileState.MovingToTarget:
                MoveTowards(_endTransform.position, _lerpTForward);
                if (HasReached(_endTransform.position)) {
                    _currentState = MissileState.StayingAtTarget;
                    StayAtTargetAsync().Forget();
                }
                break;

            case MissileState.MovingBack:
                MoveTowards(_startTransform.position, _lerpTBackward);
                if (HasReached(_startTransform.position)) {
                    _currentState = MissileState.Idle;
                }
                break;

            case MissileState.Idle:
            case MissileState.StayingAtTarget:
                // 何もしない
                break;
        }
    }

    /// <summary>
    /// ミサイルの動作を開始するメソッド
    /// </summary>
    public void LaunchPizzaBox() {
        if (_currentState != MissileState.Idle) {
            Debug.LogWarning("ミサイルは既に動作中です。");
            return;
        }

        _currentState = MissileState.MovingToTarget;
    }

    /// <summary>
    /// 指定された位置に向かって移動する
    /// </summary>
    /// <param name="targetPosition">目標位置</param>
    /// <param name="lerpT">移動速度の補間値</param>
    private void MoveTowards(Vector3 targetPosition, float lerpT) {
        transform.position = Vector3.Lerp(transform.position, targetPosition, lerpT * Time.fixedDeltaTime);
    }


    /// <summary>
    /// 目標位置に到達したかを判定する
    /// </summary>
    /// <param name="targetPosition">目標位置</param>
    /// <returns>到達していれば true</returns>
    private bool HasReached(Vector3 targetPosition) {
        return Vector3.Distance(transform.position, targetPosition) < _epsilon;
    }

    /// <summary>
    /// 目標位置に滞在する処理
    /// </summary>
    /// <returns></returns>
    private async UniTaskVoid StayAtTargetAsync() {
        // オーバーレイを表示
        if (_damageOverlay != null)
            _damageOverlay.SetActive(true);

        // プレイヤーにダメージを与える処理（ここでは単純にダメージを適用）
        DealDamage();

        // 指定された時間待機
        await UniTask.Delay(TimeSpan.FromSeconds(_waitDuration));

        // オーバーレイを非表示
        if (_damageOverlay != null)
            _damageOverlay.SetActive(false);

        // 戻る処理を開始
        _currentState = MissileState.MovingBack;
    }

    /// <summary>
    /// プレイヤーにダメージを与える処理
    /// </summary>
    private void DealDamage() {
        // ここではプレイヤーのスクリプトを取得してダメージを適用します。
        // プレイヤーのタグが "Player" と仮定しています。

        //GameObject player = GameObject.FindGameObjectWithTag("Player");
        //if (player != null) {
        //    // プレイヤーにダメージを与えるスクリプトがあると仮定
        //    PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        //    if (playerHealth != null) {
        //        playerHealth.TakeDamage(_damage);
        //        Debug.Log($"プレイヤーに {_damage} のダメージを与えました。");
        //    } else {
        //        Debug.LogWarning("PlayerHealth コンポーネントが見つかりません。");
        //    }
        //} else {
        //    Debug.LogWarning("プレイヤーが見つかりません。");
        //}
    }


    private void Update() {

        if (Input.GetKeyDown(KeyCode.Y)){
            LaunchPizzaBox();
        }


    }
}
