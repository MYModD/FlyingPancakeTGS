using UnityEngine;
using Cysharp.Threading.Tasks;
using NaughtyAttributes;

public class EnemyAttack : MonoBehaviour {
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
    public Vector2 _durationRange = new();

    [Header("アニメーション")]
    public Animator _animator;

    private bool _isMoving = false;
    private float _timerValue;

    // メソッドで位置移動、滞在、戻るを実行
    public async UniTaskVoid MoveToAttackPosition() {
        // すでに移動中の場合は処理をスキップ
        if (_isMoving) {
            Debug.Log("移動中です。新しいアクションは無視されます。");
            return;
        }
        _isMoving = true;  // 移動を開始する
        // 攻撃位置に移動
        Debug.Log("起動しました");
        _animator.SetTrigger("Start");
        await MoveToPosition(_attackTransform.localPosition, _moveSpeedToAttack);
        // 滞在する
        await UniTask.Delay((int)(_stayDuration * 1000)); // milliseconds
        // 元の位置に戻る
        await MoveToPosition(_startTransform.localPosition, _moveSpeedToStart);
        _isMoving = false; // 移動終了
    }

    // 徐々に位置を移動する処理
    private async UniTask MoveToPosition(Vector3 targetPosition, float speed) {
        while (Vector3.Distance(transform.localPosition, targetPosition) > _epsilon) {
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, speed * Time.fixedDeltaTime);
            await UniTask.Yield(); // 次のフレームまで待機
        }
        // 最終的にぴったり合わせる
        //transform.localPosition = targetPosition;
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
        //leftArmがピザにあたったら消える
        if (other.CompareTag(_pizzaCoinTag)) {
            other.gameObject.SetActive(false);
        }
    }

    private void OnEnable() {
        _timerValue = Random.Range(_durationRange.x, _durationRange.y);
    }
}