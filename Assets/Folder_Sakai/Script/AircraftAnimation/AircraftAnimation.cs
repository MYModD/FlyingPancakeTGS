using UnityEngine;

public class AircraftAnimation : MonoBehaviour {
    // 回転の軸 (X, Y, Z のいずれかを指定)
    [SerializeField] private Vector3 _rotationAxis = Vector3.up;

    // 回転させたい目標角度
    [SerializeField] private float _targetAngle1 = 90f;

    // 元の角度
    [SerializeField] private float _targetAngle2 = 0f;

    // 回転速度
    [SerializeField] private float _rotationSpeed = 2f;

    // 現在の角度
    private float _currentAngle = 0f;

    // 回転させるかどうか
    [SerializeField] private bool _shouldRotate = true;

    private bool _finishRotate = false;   // 回転が完了したかどうか
    private bool _isRotating = false;     // 回転中かどうか
    private bool _isTargetAngle1 = false;  // 現在の目標角度がターゲット1かどうか
    private float _rotationThreshold = 0.1f; // 目標角度との許容差

    private float _startAngle;  // 回転の開始角度
    private float _rotationProgress = 0f;  // 回転の進行度

    void Start() {
        _currentAngle = _targetAngle2; // 初期角度を設定
        Variable();
    }

    void Update() {

        // 回転処理
        if (_isRotating && !_finishRotate) {
            // 回転進行度を増加させる（Lerpの進行度）
            _rotationProgress += Time.deltaTime * _rotationSpeed;

            // 現在の目標角度を決定
            float targetAngle = _isTargetAngle1 ? _targetAngle1 : _targetAngle2;

            // 現在の角度を滑らかに目標角度に向かって更新 (LerpAngleでスムーズに回転)
            _currentAngle = Mathf.LerpAngle(_startAngle, targetAngle, _rotationProgress);

            // 指定された軸に沿ってオブジェクトをローカル座標系で回転
            transform.localRotation = Quaternion.AngleAxis(_currentAngle, _rotationAxis);

            // 回転が目標角度に近づいたら、回転終了とする
            if (Mathf.Abs(_currentAngle - targetAngle) < _rotationThreshold) {
                _isRotating = false;
                _finishRotate = true; // 回転終了を記録
                _currentAngle = targetAngle; // 最後に目標角度に正確に合わせる
            }
        }
    }

    public void Variable() {

        print("いばれわよ00");
        if (!_isRotating) {
            _isRotating = true;
            _finishRotate = false; // 回転を始める前にリセット

            // 回転を始める角度を記録
            _startAngle = _currentAngle;

            // 目標角度を切り替え
            _isTargetAngle1 = !_isTargetAngle1;

            print(_isTargetAngle1);

            // 回転進行度をリセット
            _rotationProgress = 0f;
        }
    }
}
