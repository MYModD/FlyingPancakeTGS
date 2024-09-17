// ---------------------------------------------------------
// EnableParticle.cs
//
// 作成日:
// 作成者:
// ---------------------------------------------------------
using UnityEngine;
using System.Collections;
public class EnableParticle : MonoBehaviour
{
    #region 変数
    [SerializeField]
    private GameObject _particle;
    #endregion
    #region プロパティ
    #endregion
    #region メソッド
    private void OnEnable() {
        _particle=this.transform.GetChild(1).gameObject;
        StartCoroutine(PlayParticle());
    }
    private IEnumerator PlayParticle() {
        _particle.SetActive(true);
        yield return new WaitForSeconds(1f);
        _particle.SetActive(false);
    }
    #endregion
}