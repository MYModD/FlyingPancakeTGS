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
public class PlayerMove : MonoBehaviour {
    #region 変数
    [SerializeField, Header("プレイヤーの上下移動最大値")] private float _maxHeight;
    [SerializeField, Header("プレイヤーの左右移動最大値")] private float _maxWidth;
    [SerializeField, Header("プレイヤーの移動速度値")] private float _moveSpeed;
    [SerializeField, Header("角度戻すスピード")] private float _resetSpeed;

    [SerializeField, Header("スピード調整倍率")] private float _speedMagnification;
    private float _maxSpeed;
    private float _minSpeed;



    [SerializeField, Header("プレイヤーの移動角度値")] private float _moveAngle;

    [SerializeField, Header("カメラが見ているオブジェクト")] private GameObject _lookAtObj;
    [SerializeField, Header("カメラ入れて")] private Camera _camera;
    [SerializeField, Header("スプラインを通るオブジェクト")] private SplineAnimate _splineAnimate;
    #endregion
    #region プロパティ
    #endregion
    #region メソッド
    /// <summary>
    /// 初期化処理 使わないなら消す
    /// </summary>
    void Awake() {
    }
    /// <summary>
    /// 更新前処理
    /// </summary>
    void Start() {
        float devide = 1.2f;
        _maxSpeed = _splineAnimate.MaxSpeed*devide;
        _minSpeed = _splineAnimate.MaxSpeed / 0.8f;
    }
    /// <summary>
    /// 更新処理
    /// </summary>
    void Update() {
        MovePosition();
        ChangeSpeed();
    }
    /// <summary>
    /// 動き管理プロセスを実行
    /// </summary>
    private void MovePosition() {
        //縦方向の入力値保存
        float inputVertical = Input.GetAxis("Vertical");
        //横方向の入力値保存
        float inputHorizontal = Input.GetAxis("Horizontal");
        
        //カメラが見ているオブジェクトの位置の調整
        //プレイヤーの１/２のX座標、Y座標の位置に移動させる
        _lookAtObj.transform.localPosition = new Vector3(transform.localPosition.x / 2, transform.localPosition.y / 2, _lookAtObj.transform.localPosition.z);

        //入力値が０だったら何もさせない
        if (inputHorizontal == 0 && inputVertical == 0) {
            //角度を０に戻す処理
            ResetRotation();
            return;
        }
        VerticalProcess(inputVertical,inputHorizontal);
        HorizontalProcess(inputHorizontal);
    }
    /// <summary>
    /// 縦の動き
    /// </summary>
    private void VerticalProcess(float inputVertical,float inputHorizontal) {
        //縦の動き
        if (inputVertical < 0)//下の処理
        {
            //制限値以上に動かさない
            if (this.transform.localPosition.y < -_maxHeight) {
                //角度を０に戻す処理
                ResetRotation();
                //動かなくなってしまわないように
                HorizontalProcess(inputHorizontal);
                return;
            }
            //動きまとめたメソッド
            Vertical_RotateMove(inputVertical);
        } else if (inputVertical > 0)//上の処理
        {
            //制限値以上に動かさない
            if (this.transform.localPosition.y > _maxHeight) {
                //角度を０に戻す処理
                ResetRotation();
                //動かなくなってしまわないように
                HorizontalProcess(inputHorizontal);
                return;
            }
            //動きまとめたメソッド
            Vertical_RotateMove(inputVertical);
        }
    }
    /// <summary>
    /// 横の動き
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
    }
    private void ChangeSpeed() {
        float inputRStick = Input.GetAxis("RStickH");
        print(inputRStick);
        float speed = CalculateSpeed(inputRStick);
        _splineAnimate.ElapsedTime += speed;
        _splineAnimate.enabled = true;
    }
    #region 動きに関するメソッド
    /// <summary>
    /// 縦の行動プロセスを実行
    /// </summary>
    /// <param name="vertical">Verticalの入力値</param>
    private void Vertical_RotateMove(float vertical) {
        transform.Rotate(RotateVertical(vertical));
        transform.localPosition += MoveVertical(vertical);
    }
    /// <summary>
    /// 横の行動プロセスを実行
    /// </summary>
    /// <param name="horizontal">Horizontalの入力値</param>
    private void Horizontal_RotateMove(float horizontal) {
        transform.Rotate(RotateHorizontal(horizontal));
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
        Vector3 rotateIndex = (Vector3.forward * _moveAngle  * Time.deltaTime);
        //入力値によって正負を変える
        if (horizontal > 0) {
            return rotateIndex;
        }
        return -rotateIndex;
    }
    #endregion
    private float CalculateSpeed(float input) {
        input +=2;
        float changePower =Time.deltaTime*input;
        return changePower;
    }

    #endregion
}