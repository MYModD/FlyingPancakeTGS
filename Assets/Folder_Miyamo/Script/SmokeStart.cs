// ---------------------------------------------------------
// SmokeStart.cs
//
// 作成日:
// 作成者:
// ---------------------------------------------------------
using UnityEngine;
using System.Collections;
public class SmokeStart : MonoBehaviour
{
    #region 変数

    public ParticleSystem _hage;

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
}


    private void OnEnable() {
            

        _hage.Play();


    }

    private void OnDisable() {
        

        _hage.Stop();


    }
    #endregion
}