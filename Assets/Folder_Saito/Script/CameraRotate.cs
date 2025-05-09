// ---------------------------------------------------------
// CameraRotate.cs
//
// 作成日:
// 作成者:
// ---------------------------------------------------------
using UnityEngine;
using System.Collections;

public class CameraRotate : MonoBehaviour {
    #region 変数
    [SerializeField, Header("向きたいオブジェクト")] private Transform _targetPos;
    [SerializeField, Header("メインのオブジェクト")] private Transform _mainGamePos;
    [SerializeField, Header("リザルトのオブジェクト")] private Transform _resultPos;

    [SerializeField, Header("移動速度")] private float _moveSpeed = 1.0f;
    [SerializeField, Header("停止する距離")] private float _stopDistance = 0.1f; // リザルトポジションまでの最小距離

    private bool _isResult = false;
    private bool _isMainGame = true;
    #endregion

    #region プロパティ
    #endregion

    #region メソッド
    /// <summary>
    /// 更新処理
    /// </summary>
    void Update() {
        if (_isResult ) {
            MoveToResultPosition();
        } else {
            MainGamePosition();
        }
        this.transform.LookAt(_targetPos);
    }

    /// <summary>
    /// リザルトのポジションにだんだん向かう処理
    /// </summary>
    private void MoveToResultPosition() {
        // リザルトポジションとの距離を測定
        float distanceToResult = Vector3.Distance(this.transform.position, _resultPos.position);

        // 指定した距離内に到達したら移動を停止
        if (distanceToResult <= _stopDistance) {
            return;
        }

        // カメラの位置をリザルトポジションへ徐々に移動させる
        this.transform.position = Vector3.Lerp(this.transform.position, _resultPos.position, Time.deltaTime * _moveSpeed);
    }
    /// <summary>
    /// ゲーム中のカメラのポジションにする処理
    /// </summary>
    private void MainGamePosition() {
        if (_isMainGame) {
            //指定したポジションに移動させる
            this.transform.position = _mainGamePos.position;
            _isMainGame=false;
        }

    }
    /// <summary>
    /// リザルトのポジションに向かわせたいときに呼んでね
    /// </summary>
    public void IsResultMoveSwitch() {
        _isResult = true;
    }
    /// <summary>
    /// メインゲームのポジションに戻したいときに呼んでね
    /// </summary>
    public void IsMainGameSwitch() {
        _isResult = false;
        _isMainGame = true;
    }

    #endregion
}

