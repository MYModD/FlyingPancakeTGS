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
    /// 更新前処理
    /// </summary>
    void Start() {
        //ランダムなSE設定
        _source.clip = _clip[Random.Range(0, _clip.Length)];
    }
    /// <summary>
    /// 更新処理
    /// </summary>
    void Update() {
    }
    private void OnEnable() {
        //ランダムなSE設定
        _source.clip = _clip[Random.Range(0, _clip.Length)];
    }
    #endregion
}