using UnityEngine;

public class FollowTarget : MonoBehaviour {
    // ターゲットのTransformを指定する。追従するオブジェクトをエディターから設定するためにSerializeFieldを使用
    [SerializeField, Header("ターゲット")]
    private Transform _target;

    // 初期化処理。ターゲットが設定されているかどうかを確認
    private void Start() {
        ValidateTarget();
    }

    // 毎フレーム実行される処理。ターゲットの位置に追従する
    private void Update() {
        FollowTargetPosition();
    }

    // ターゲットが設定されているかチェックするメソッド
    // 設定されていない場合はエラーメッセージを出力する
    private void ValidateTarget() {
        if (_target == null) {
            Debug.LogError("ターゲットが設定されていません"); // ターゲットが未設定の場合のエラーログ
        }
    }

    // ターゲットの位置にオブジェクトを移動させるメソッド
    private void FollowTargetPosition() {
        if (_target != null) {
            transform.position = _target.position; // ターゲットの位置にオブジェクトを追従させる
        }
    }
}
