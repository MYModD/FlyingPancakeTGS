using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using System;
using System.Threading;
using UnityEngine;

public class PizzaWanima : MonoBehaviour {
    [Header("移動設定")]
    [SerializeField] private Transform _startTransform;  // 開始位置
    [SerializeField] private Transform _attackTransform; // 攻撃位置
    [SerializeField, Range(0f, 1f)] private float _moveSpeedToAttack = 0.5f; // 攻撃位置に行くスピード
    [SerializeField, Range(0f, 1f)] private float _moveSpeedToStart = 0.3f;  // 帰るスピード
    [SerializeField] private float _epsilon = 0.01f; // 位置誤差の許容範囲

    [Header("攻撃設定")]
    [SerializeField] private float _stayDuration = 2f; // 攻撃位置で滞在する時間 

    [SerializeField, Tag]
    private string _pizzaCoinTag;

    [Header("タイミング設定")]
    [MinMaxSlider(0, 5f)]
    public Vector2 _durationRange = new Vector2();

    [Header("アニメーション")]
    public Animator _animator;

    private bool _isMoving = false;
    private float _timerValue;
    private CancellationTokenSource _cts;

    private void OnEnable() {
        _timerValue = UnityEngine.Random.Range(_durationRange.x, _durationRange.y);
        _cts = new CancellationTokenSource();
    }

    private void OnDisable() {
        _cts?.Cancel();
        _cts?.Dispose();
    }

    private void Update() {
        _timerValue -= Time.deltaTime;
        _timerValue = Mathf.Max(0, _timerValue);
        if (_timerValue <= 0) {
            MoveToAttackPosition().Forget();
            _timerValue = UnityEngine.Random.Range(_durationRange.x, _durationRange.y);
            Debug.Log($"タイマーは{_timerValue}");
        }
    }

    public async UniTaskVoid MoveToAttackPosition() {
        if (_isMoving) {
            Debug.Log("移動中です。新しいアクションは無視されます。");
            return;
        }

        _isMoving = true;

        try {
            Debug.Log("起動しました");
            _animator.SetTrigger("Start");

            await MoveToPosition(_attackTransform.localPosition, _moveSpeedToAttack);
            await UniTask.Delay(TimeSpan.FromSeconds(_stayDuration), cancellationToken: _cts.Token);
            await MoveToPosition(_startTransform.localPosition, _moveSpeedToStart);
        } catch (OperationCanceledException) {
            Debug.Log("移動がキャンセルされました");
        } catch (Exception ex) {
            Debug.LogError($"移動中にエラーが発生しました: {ex.Message}");
        } finally {
            _isMoving = false;
        }
    }

    private async UniTask MoveToPosition(Vector3 targetPosition, float speed) {
        while (Vector3.Distance(transform.localPosition, targetPosition) > _epsilon) {
            _cts.Token.ThrowIfCancellationRequested();
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, speed * Time.fixedDeltaTime);
            await UniTask.Yield(_cts.Token);
        }

        Debug.Log("停止します");
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(_pizzaCoinTag)) {
            other.gameObject.SetActive(false);
        }
    }
}