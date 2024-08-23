// ---------------------------------------------------------
// HomingGauge.cs
//
// 作成日:
// 作成者:
// ---------------------------------------------------------
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
public class HomingGauge : MonoBehaviour {
    #region 変数
    [SerializeField] private Sprite _emptyImage;
    [SerializeField] private Sprite _fullImage;

    [SerializeField] private Image[] _images;

    private int _index;
    #endregion
    #region プロパティ
    #endregion
    #region メソッド
    /// <summary>
    /// 初期化処理 使わないなら消す
    /// </summary>
    void Awake() {
    }
    /// <summary>
    /// 更新前処理
    /// </summary>
    void Start() {
        _index = _images.Length - 1;
    }
    /// <summary>
    /// 更新処理
    /// </summary>
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            UseGauge();
        }
        if (Input.GetKeyUp(KeyCode.Space)) {
            ReuseGauge();
        }
    }
    public void UseGauge() {
        _images[_index].sprite = _emptyImage;
        _index--;
    }
    public void ReuseGauge() {
        _index++;
        _images[_index].sprite = _fullImage;
    }
    #endregion
}