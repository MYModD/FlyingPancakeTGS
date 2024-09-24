// ---------------------------------------------------------
// AfterburnerParticles.cs
//
// 作成日:
// 作成者:
// ---------------------------------------------------------
using UnityEngine;
using System.Collections;
public class AfterburnerParticles : MonoBehaviour
{
    #region 変数

    [SerializeField] private ParticleSystem _boosterRigft;
    [SerializeField] private ParticleSystem _boosterLeft;

    #endregion
    #region プロパティ
    #endregion
    #region メソッド
    /// <summary>
    /// 初期化処理 使わないなら消す
    /// </summary>
    void Awake()
{
        _boosterLeft.Play();
        _boosterRigft.Play();
}
#endregion
}