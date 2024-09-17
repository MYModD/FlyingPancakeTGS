using UnityEngine;
using Cysharp.Threading.Tasks;
using NaughtyAttributes;

public class PizzaWaniWaniMissile : MonoBehaviour {
    [Header("移動設定")]
    [SerializeField] private Transform _startTransform;  // 開始位置
    [SerializeField] private Transform _attackTransform; // 攻撃位置
    [SerializeField, Range(0f, 10f)] private float _moveSpeedToAttack = 2f; // 攻撃位置に行くスピード（単位: units/second）
    [SerializeField, Range(0f, 10f)] private float _moveSpeedToStart = 1f;  // 帰るスピード（単位: units/second）
    [SerializeField] private float _epsilon = 0.01f; // 位置誤差の許容範囲

    [Header("攻撃設定")]
    [SerializeField] private float _stayDuration = 2f; // 攻撃位置で滞在する時間
    [SerializeField, Tag]
    private string _pizzaCoinTag;

    [Header("タイミング設定")]
    [MinMaxSlider(0, 5f)]
    public Vector2 _durationRange = new Vector2(1f, 3f);

    [Header("アニメーション")]
    public Animator _animator;

    private bool _isMoving = false;
    private float _timerValue;

    public async UniTaskVoid MoveToAttackPosition() {
        if (_isMoving) {
            Debug.Log("移動中です。新しいアクションは無視されます。");
            return;
        }

        _isMoving = true;
        Debug.Log("起動しました");
        
        //_animator.SetTrigger("Start");

        await MoveToPosition(_attackTransform.localPosition, _moveSpeedToAttack);
        await UniTask.Delay((int)(_stayDuration * 1000));
        await MoveToPosition(_startTransform.localPosition, _moveSpeedToStart);

        _isMoving = false;
    }

    private async UniTask MoveToPosition(Vector3 targetPosition, float speed) {
        Vector3 startPosition = transform.localPosition;
        float distance = Vector3.Distance(startPosition, targetPosition);
        float duration = distance / speed;
        float elapsedTime = 0f;

        while (elapsedTime < duration) {
            float t = elapsedTime / duration;
            transform.localPosition = Vector3.Lerp(startPosition, targetPosition, t);
            elapsedTime += Time.deltaTime;
            await UniTask.Yield();
        }

        transform.localPosition = targetPosition;
        Debug.Log("停止します");
    }

    private void Update() {
        _timerValue -= Time.deltaTime;
        _timerValue = Mathf.Max(0, _timerValue);

        if (_timerValue <= 0) {
            MoveToAttackPosition().Forget();
            _timerValue = Random.Range(_durationRange.x, _durationRange.y);
            Debug.Log($"タイマーは{_timerValue}");
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(_pizzaCoinTag)) {
            other.gameObject.SetActive(false);
        }
    }

    private void OnEnable() {
        _timerValue = Random.Range(_durationRange.x, _durationRange.y);
    }
}