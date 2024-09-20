// ---------------------------------------------------------
// RamdamBom.cs
//
// 作成日:
// 作成者:
// ---------------------------------------------------------
using UnityEngine;
using System.Collections;
public class RamdamBom : MonoBehaviour {
    #region 変数
    [SerializeField]
    private AudioSource _source;
    [SerializeField]
    private AudioClip[] _clip;
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
        _source.clip =_clip[Random.Range(0, _clip.Length)];
}
/// <summary>
/// 更新処理
/// </summary>
void Update ()
{
}
    private void OnEnable() {
        _source.clip = _clip[Random.Range(0, _clip.Length)];
    }
    #endregion
}