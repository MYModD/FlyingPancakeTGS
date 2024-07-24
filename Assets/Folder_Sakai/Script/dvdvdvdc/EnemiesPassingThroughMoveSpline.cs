using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class EnemiesPassingThroughMoveSpline : MonoBehaviour
{
    #region 変数

    [SerializeField, Tooltip("スプライン")]
    private SplineContainer _spline;

    [Header("移動関連")]
    [SerializeField, Tooltip("ルート速度")]
    private float _rootSpeed;

    [SerializeField, Tooltip("速度の変更速度")]
    private float _changeingSpeed;

    //移動速度
    private float _moveSpeed;

    //変更後の速度
    private float _changeSpeed;

    //基準速度
    private float _defaultSpeed;

    //スプラインに沿って移動させる対象
    private Transform _moveTarget;

    //補間の割合(0~1の間を始点^終点で移動)
    private float _percentage;

    //前フレームのワールド位置
    private Vector3 _prevPos;

    //スプラインの終点
    private const int ENDSPLINE = 1;

    //スプラインの開始
    private const int STARTSPLINE = 0;

    //停止中
    private bool _isStop = true;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //スプラインに沿って移動させる対象
        _moveTarget = this.gameObject.transform;

        //移動速度をルート速度に設定
        _moveSpeed = _rootSpeed;
        _changeSpeed = _moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isStop) {return;}

        //割合を時間で加算
        _percentage += Time.deltaTime * _moveSpeed;

        MovePosRotate();
    }

    private void MovePosRotate() {

        if (_percentage >= ENDSPLINE) {

            _percentage = STARTSPLINE;
            _isStop = true;
        }

        // 計算した位置（ワールド座標）をターゲットに代入
        _moveTarget.position = _spline.EvaluatePosition(_percentage);

        // 現在フレームのフレーム位置
        Vector3 position = _moveTarget.position;

        // 移動量を計算
        Vector3 moveVolume = position - _prevPos;

        // 次のUpdateで使うための前フレーム位置補完
        _prevPos = position;

        // 静止している状態だと、進行方向を特定できないため回転しない
        if (moveVolume == Vector3.zero) {
            return;
        }

        // 進行方向に角度を変更
        _moveTarget.rotation = Quaternion.LookRotation(moveVolume, Vector3.up);
    }

    public void StartMoving() {

        _percentage = STARTSPLINE;
        _isStop = false;
    }

}