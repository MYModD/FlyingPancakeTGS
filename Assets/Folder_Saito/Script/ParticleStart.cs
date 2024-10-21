// ---------------------------------------------------------
// ParticleStart.cs
//
// 作成日:
// 作成者:
// ---------------------------------------------------------
using UnityEngine;
using System.Collections;
public class ParticleStart : MonoBehaviour
{
    #region 変数
    [SerializeField]
    private ParticleSystem _particleSystem;
    #endregion
    #region プロパティ
    #endregion
    #region メソッド

/// <summary>
/// 更新処理
/// </summary>
void Update ()
{
}
    private void OnEnable() {
        _particleSystem.Play();
    }
    #endregion
}