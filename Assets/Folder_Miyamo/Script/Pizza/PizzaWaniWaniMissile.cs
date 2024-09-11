using UnityEngine;
using Cysharp.Threading.Tasks;
using NaughtyAttributes;

public class EnemyAttack : MonoBehaviour {

    [SerializeField] private Transform _startTransform;  // 開始位置
    [SerializeField] private Transform[] _attackTransform; // 攻撃位置
    [SerializeField, Range(0f, 1f)] private float _moveSpeedToAttack = 0.5f; // 攻撃位置に行くスピード
    [SerializeField, Range(0f, 1f)] private float _moveSpeedToStart = 0.3f;  // 帰るスピード
    [SerializeField] private float _epsilon = 0.01f; // 位置誤差の許容範囲
    [SerializeField] private float _stayDuration = 2f; // 攻撃位置で滞在する時間

    [MinMaxSlider(0,5f)]
    public Vector2 _durationRange = new ();

    private bool _isMoving = false;
    private float _timerValue;


    [SerializeField, Header("今の攻撃目標")]
    private int _attackPos;

    // メソッドで位置移動、滞在、戻るを実行
    public async UniTaskVoid MoveToAttackPosition() {
        // すでに移動中の場合は処理をスキップ
        if (_isMoving) {
            Debug.Log("移動中です。新しいアクションは無視されます。");
            return;
        }

        _isMoving = true;  // 移動を開始する

        // 攻撃位置に移動

        
        await MoveToPosition(_attackTransform[_attackPos].localPosition, _moveSpeedToAttack);




        // 滞在する
        await UniTask.Delay((int)(_stayDuration * 1000)); // milliseconds

        // 元の位置に戻る
        await MoveToPosition(_startTransform.localPosition, _moveSpeedToStart);

        _isMoving = false; // 移動終了
        _attackPos = Random.Range(0, 2);

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
            Debug.Log($"タイマーは{_timerValue}に設定されました");

        }



    }
    private void OnEnable() {


        _timerValue = Random.Range(_durationRange.x, _durationRange.y);
        _attackPos = Random.Range(0, 2);


    }
}
