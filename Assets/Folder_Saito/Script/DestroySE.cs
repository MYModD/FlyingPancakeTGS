// ---------------------------------------------------------
// DestroySE.cs
//
// 作成日:
// 作成者:
// ---------------------------------------------------------
using UnityEngine;
using System.Collections;
public class DestroySE : MonoBehaviour {
    #region 変数
    [SerializeField]
    private AudioClip _boom;
    [SerializeField]
    private AudioSource _source;
    private ControllerBuruBuru _buruBuru;
    #endregion
    #region プロパティ
    #endregion
    #region メソッド
    private void OnDisable() {
        if (_buruBuru == null) {
            _buruBuru=ControllerBuruBuru.Instance;
        }
        _buruBuru.StartVibration();
        _source.Stop();
        _source.PlayOneShot(_boom);
    }
    #endregion
}