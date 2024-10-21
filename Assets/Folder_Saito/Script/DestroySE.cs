// ---------------------------------------------------------
// DestroySE.cs
//
// 作成日:
// 作成者:G2A118齊藤大志
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
        _buruBuru.StartVibration();//ぶっるぶる
        _source.Stop();
        _source.PlayOneShot(_boom);//爆破音
    }
    #endregion
}