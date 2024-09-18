// ---------------------------------------------------------
// AutoZero.cs
//
// 作成日:
// 作成者:
// ---------------------------------------------------------
using UnityEngine;
using System.Collections;
public class AutoZero : MonoBehaviour
{
#region 変数
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
        this.transform.localPosition =new Vector3(0,0.1f,0);
}
#endregion
}