using UnityEngine;
using Cysharp.Threading.Tasks;

public class EnemyAttack : MonoBehaviour {

    [SerializeField] private Transform _startTransform;  // 開始位置
    [SerializeField] private Transform _attackTransform; // 攻撃位置
    [SerializeField, Range(0f, 1f)] private float _moveSpeedToAttack = 0.5f; // 攻撃位置に行くスピード
    [SerializeField, Range(0f, 1f)] private float _moveSpeedToStart = 0.3f;  // 帰るスピード
    [SerializeField] private float _epsilon = 0.01f; // 位置誤差の許容範囲
    [SerializeField] private float _stayDuration = 2f; // 攻撃位置で滞在する時間

    private bool _isMoving = false;

    private void Start() {
        MoveToAttackPosition().Forget();
    }

    // メソッドで位置移動、滞在、戻るを実行
    public async UniTaskVoid MoveToAttackPosition() {
        // 移動する
        await MoveToPosition(_attackTransform.position, _moveSpeedToAttack);

        // 滞在する
        await UniTask.Delay((int)(_stayDuration * 1000)); // milliseconds

        // 帰る
        await MoveToPosition(_startTransform.position, _moveSpeedToStart);
    }

    // 徐々に位置を移動する処理
    private async UniTask MoveToPosition(Vector3 targetPosition, float speed) {
        _isMoving = true;

        while (Vector3.Distance(transform.position, targetPosition) > _epsilon) {
            transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.fixedDeltaTime);
            await UniTask.Yield(); // 次のフレームまで待機
        }

        // 最終的にぴったり合わせる
        transform.position = targetPosition;
        _isMoving = false;
    }
}
