// ---------------------------------------------------------
// PlayerMove.cs
//
// 作成日:
// 作成者:
// ---------------------------------------------------------
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.Splines;
using UnityEngine.UIElements;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
public class PlayerMove : MonoBehaviour {
    #region 変数
    [SerializeField, Header("プレイヤーの上下移動最大値")] private float _maxHeight;
    [SerializeField, Header("プレイヤーの左右移動最大値")] private float _maxWidth;
    [SerializeField, Header("プレイヤーの移動速度値")] private float _moveSpeed;
    [SerializeField, Header("角度戻すスピード")] private float _resetSpeed;

    [SerializeField, Header("左右角度の最小値")] private float _minimumAngle = -45f;
    [SerializeField, Header("左右角度の最大値")] private float _maximumAngle = 45;
    [SerializeField, Header("上下角度の最小値")] private float _minimumAngleUp = -45f;
    [SerializeField, Header("上下角度の最大値")] private float _maximumAngleUp = 45;

    [SerializeField, Header("スピード調整倍率")] private float _speedMagnification;
    [SerializeField, Header("プレイヤーの角度倍率")] private float _rotateSpeed = 10;

    [SerializeField, Header("プレイヤーの移動角度値")] private float _moveAngle;

    [SerializeField, Header("カメラが見ているオブジェクト")] private GameObject _lookAtObj;
    [SerializeField, Header("カメラ入れて")] private Camera _camera;
    [SerializeField, Header("スプラインを通るオブジェクト")] private SplineAnimate _splineAnimate1;
    [SerializeField, Header("スプラインを通るオブジェクト")] private SplineAnimate _splineAnimate2;
    [SerializeField, Header("スプラインを通るオブジェクト")] private SplineAnimate _splineAnimate3;
    [SerializeField, Header("スプラインを通るオブジェクト")] private SplineAnimate _splineAnimate4;
    [SerializeField, Header("スプラインを通るオブジェクト")] private SplineAnimate _splineAnimate5;

    [SerializeField, Header("CanvasManagerのオブジェクトをいれ")] private CanvasManager _canvas;
    [SerializeField] private ControllerSelectButton _selectButton;

    [SerializeField] GameObject _aa;
    bool _a = true;
    private float _stopTime;
    private float _nowTime;
    private bool _isStop = false;
    private bool _isVerticalInversion = false;
    private bool _ishorizontalInversion = false;
    private int _verticalIndex = 1;
    private int _horizontalIndex = 1;
    private float _changePower = 0f;
    #endregion
    #region プロパティ
    #endregion
    #region メソッド

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update() {

        if (!_canvas.CanMove()) {

            return;
        }
        StopOrMoving();
        //if (Input.GetKeyDown(KeyCode.Space)) {
        //    StopMoving(5f);
        //}
    }
    /// <summary>
    /// 動いているか止まっているかの分岐
    /// </summary>
    private void StopOrMoving() {
        _isVerticalInversion = _selectButton.VerticalInversionCheak();
        _verticalIndex = _isVerticalInversion ? -1 : 1;
        _ishorizontalInversion = _selectButton.HorizontalInversionCheak();
        _horizontalIndex = _ishorizontalInversion ? -1 : 1;
        if (_isStop) {

            transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, Time.deltaTime * _resetSpeed);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.identity, Time.deltaTime * _resetSpeed);
            _nowTime += Time.deltaTime;
            if (_nowTime >= _stopTime) {
                _isStop = false;
                _nowTime = 0;
            }
        } else {

            //動きと角度
            MovePosition();

        }
        //速度
        ChangeSpeed();
    }
    /// <summary>
    /// 動き管理プロセスを実行
    /// </summary>
    private void MovePosition() {
        //InputはUpdateでまとめて取りたい
        //縦方向の入力値保存
        float inputVertical = Input.GetAxis("Vertical");
        //横方向の入力値保存
        float inputHorizontal = Input.GetAxis("Horizontal");

        // ここで反転処理を行う
        if (_isVerticalInversion) {
            inputVertical *= -1;
        }

        if (_ishorizontalInversion) {
            inputHorizontal *= -1;
        }

        //カメラが見ているオブジェクトの位置の調整
        _lookAtObj.transform.localPosition = new Vector3(transform.localPosition.x / 2, transform.localPosition.y / 2, _lookAtObj.transform.localPosition.z);

        //入力値が０だったら何もさせない
        if (inputHorizontal == 0 && inputVertical == 0) {
            ResetRotation();
            return;
        }

        VerticalProcess(inputVertical, inputHorizontal);
        HorizontalProcess(inputHorizontal);
    }

    /// <summary>
    /// 縦の動き 上下の処理分け
    /// </summary>
    private void VerticalProcess(float inputVertical, float inputHorizontal) {

        // 縦の動き
        if (inputVertical < 0) // 下の処理
        {
            if (this.transform.localPosition.y < -_maxHeight) {
                ResetRotation();
                HorizontalProcess(inputHorizontal);
                return;
            }
            Vertical_RotateMove(inputVertical);
        } else if (inputVertical > 0) // 上の処理
          {
            if (this.transform.localPosition.y > _maxHeight) {
                ResetRotation();
                HorizontalProcess(inputHorizontal);
                return;
            }
            Vertical_RotateMove(inputVertical);
        }
    }

    /// <summary>
    /// 横の動き　左右の処理分け
    /// </summary>
    private void HorizontalProcess(float inputHorizontal) {
        //横の動き
        if (inputHorizontal > 0)//左の処理
        {
            //制限値以上に動かさない
            if (this.transform.localPosition.x < -_maxWidth) {
                //角度を０に戻す処理
                ResetRotation();
                return;
            }
            Horizontal_RotateMove(inputHorizontal);
        } else if (inputHorizontal < 0)//右の処理
          {
            //制限値以上に動かさない
            if (this.transform.localPosition.x > _maxWidth) {
                //角度を０に戻す処理
                ResetRotation();
                return;
            }
            Horizontal_RotateMove(inputHorizontal);
        }
    }/// <summary>
     /// プレイヤーの速度変化
     /// </summary>
    private void ChangeSpeed() {
        //インプットはUpdateでまとめて取りたい
        float inputRStick = Input.GetAxis("RStickV");

        if (_splineAnimate3.enabled) {
           _splineAnimate3.ElapsedTime += _changePower;
        }
        //速度計算した値
        float speed = CalculateSpeed(inputRStick);
        //スプラインを通り終わる時間の設定値を変えて加減速
        if (_splineAnimate1.enabled) {
            _splineAnimate1.ElapsedTime += speed;
        } else if (_splineAnimate2.enabled) {
            _splineAnimate2.ElapsedTime += speed;
        } else if (_splineAnimate4.enabled) {
            _splineAnimate4.ElapsedTime += speed;
        } else {
            _splineAnimate5.ElapsedTime += speed;
        }

    }
    #region 動きに関するメソッド
    /// <summary>
    /// 縦の行動プロセスを実行
    /// </summary>
    /// <param name="vertical">Verticalの入力値</param>
    private void Vertical_RotateMove(float vertical) {
        //transform.Rotate(RotateVertical(vertical));
        transform.localPosition += MoveVertical(vertical);
    }
    /// <summary>
    /// 横の行動プロセスを実行
    /// </summary>
    /// <param name="horizontal">Horizontalの入力値</param>
    private void Horizontal_RotateMove(float horizontal) {
        // 回転の差分を計算
        Vector3 rotateIndex = RotateHorizontal(horizontal);

        // 現在のZ軸の回転角度を取得し、-180度から180度の範囲に変換
        float currentZRotation = transform.localEulerAngles.z;
        float semicircularAngle = 180f;
        float circularAngle = 360f;
        if (currentZRotation > semicircularAngle) {
            currentZRotation -= circularAngle;
        }

        // 新しい回転角度を計算し、設定した最小値から最大値の範囲に制限
        float newZRotation = currentZRotation + rotateIndex.z;
        newZRotation = Mathf.Clamp(newZRotation, _minimumAngle, _maximumAngle);

        // 新しい回転角度の適用
        transform.localRotation = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, newZRotation);

        // 位置を更新
        transform.localPosition += MoveHorizontal(horizontal);
    }
    /// <summary>
    /// ローカルローテーションを０に
    /// </summary>
    private void ResetRotation() {
        // 目標の回転（Quaternion.identityは回転なし）
        Quaternion targetRotation = Quaternion.identity;
        //０に戻す
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * _resetSpeed);
    }
    #endregion
    //--------------ここから返り値あり-------------------------------------------------------------------------------------------------
    #region Vertical関連メソッド
    /// <summary>
    /// 縦の動きの制御
    /// </summary>
    /// <param name="vertical">Verticalの入力値</param>
    /// <returns>１フレームの推進力</returns>
    private Vector3 MoveVertical(float vertical) {
        //後で上下操作反転できるようにする
        //動きの計算
        Vector3 movePower = Vector3.down * vertical * Time.deltaTime * _moveSpeed;
        return -movePower;
    }
    /// <summary>
    /// 縦の角度調整
    /// </summary>
    /// <param name="vertical">Verticalの入力値</param>
    /// <returns>１フレームで加算、減算される角度</returns>
    private Vector3 RotateVertical(float vertical) {
        //割る値
        float divide = 2;
        //角度計算
        Vector3 rotateIndex = (Vector3.right * _moveAngle / divide * Time.deltaTime);
        //入力値によって正負を変える
        if (vertical > 0) {
            return rotateIndex;
        }
        return -rotateIndex;
    }
    #endregion
    #region Horizontal関連メソッド
    /// <summary>
    /// 横の動きの制御
    /// </summary>
    /// <param name="horizontal">Horizontalの入力値</param>
    /// <returns>１フレームの横の推進力</returns>
    private Vector3 MoveHorizontal(float horizontal) {
        //Vector3.leftがないから反転用
        float signIndex = -1;
        //動きの計算
        Vector3 movePower = Vector3.right * horizontal * Time.deltaTime * _moveSpeed * signIndex;
        return movePower;
    }
    /// <summary>
    /// 横の角度の制御
    /// </summary>
    /// <param name="horizontal">Horizontalの入力値</param>
    /// <returns>１フレームで加算、減算される角度</returns>
    private Vector3 RotateHorizontal(float horizontal) {
        //角度計算
        Vector3 rotateIndex = (Vector3.forward * _moveAngle * Time.deltaTime * _rotateSpeed);
        //入力値によって正負を変える
        if (horizontal > 0) {
            return rotateIndex;
        }
        return -rotateIndex;
    }
    #endregion
    /// <summary>
    /// Rスティックで速度変化
    /// </summary>
    /// <param name="input">Rスティックの入力値　-1～1</param>
    /// <returns>速度量の変化した値</returns>
    private float CalculateSpeed(float input) {
        //０をなくすために加算
        input += 2;
        int index = _canvas.CanMove() ? 1 : 0;
        //速度変化させる値の決定
        float changePower = Time.deltaTime * input * _speedMagnification * index;
        return changePower;
    }

    public void CalculateSpeed3rd(float input) {

        int index = _canvas.CanMove() ? 1 : 0;
        //速度変化させる値の決定
        _changePower = Time.deltaTime * input * _speedMagnification * index;
    }
    //-------------------------ここからパブリックメソッド---------------------------------------
    /// <summary>
    /// 指定の秒数停止メソッド
    /// </summary>
    /// <param name="seconds">止めたい秒数</param>
    public void StopMoving(float seconds) {
        _stopTime = seconds;
        _isStop = true;
    }
    #endregion
}