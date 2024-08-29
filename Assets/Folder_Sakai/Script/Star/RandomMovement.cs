using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    #region 変数
    [SerializeField, Header("Center Object")]
    private GameObject _centerObject; // 中心となるオブジェクト

    [SerializeField, Header("Vertical Move Limit")]
    private float _moveLimitVertical;

    [SerializeField, Header("Horizontal Move Limit")]
    private float _movementLimitHorizontal;

    [SerializeField, Header("Move Speed")]
    private float _moveSpeed = 1.0f;

    private Vector3 _targetPosition;
    #endregion

    #region メソッド
    /// <summary>
    /// 初期化処理
    /// </summary>
    void Awake() {
        SetNewTargetPosition();
    }

    /// <summary>
    /// 更新前処理
    /// </summary>
    void Start() {
        // 初期化は特に必要ない
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update() {
        Move();
    }

    /// <summary>
    /// 指定範囲内を移動する処理
    /// </summary>
    void Move() {
        float step = _moveSpeed * Time.deltaTime;
        Vector3 newPosition = Vector3.MoveTowards(this.gameObject.transform.position, _targetPosition, step);
        newPosition.z = this.gameObject.transform.position.z; // Z座標を固定
        this.gameObject.transform.position = newPosition;

        if (Vector3.Distance(this.gameObject.transform.position, _targetPosition) < 0.001f) {
            SetNewTargetPosition();
        }
    }

    /// <summary>
    /// 新しい移動先を設定する処理
    /// </summary>
    void SetNewTargetPosition() {
        float centerX = _centerObject.transform.position.x;
        float centerY = _centerObject.transform.position.y;
        float fixedZ = this.gameObject.transform.position.z; // 固定されたZ座標

        float targetX = Random.Range(centerX - _movementLimitHorizontal, centerX + _movementLimitHorizontal);
        float targetY = Random.Range(centerY - _moveLimitVertical, centerY + _moveLimitVertical);

        _targetPosition = new Vector3(targetX, targetY, fixedZ);
    }
    #endregion
}
