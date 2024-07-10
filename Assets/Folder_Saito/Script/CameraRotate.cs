// ---------------------------------------------------------
// CameraRotate.cs
//
// 作成日:
// 作成者:
// ---------------------------------------------------------
using UnityEngine;
using System.Collections;
public class CameraRotate : MonoBehaviour
{
    #region 変数
    [SerializeField, Header("向きたいオブジェクト")] private Transform _targetPos;  
#endregion
#region プロパティ
#endregion
#region メソッド
/// <summary>
/// 初期化処理 使わないなら消す
/// </summary>
void Awake()
{
}
/// <summary>
/// 更新前処理
/// </summary>
void Start ()
{
}
/// <summary>
/// 更新処理
/// </summary>
void Update ()
{
        this.transform.LookAt(_targetPos);
}
#endregion
}