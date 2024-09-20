// ---------------------------------------------------------
// GetCoolTime.cs
//
// 作成日:
// 作成者:
// ---------------------------------------------------------
using UnityEngine;
using UnityEngine.UI;

public class GetCooltime : MonoBehaviour {
    #region 変数
    [SerializeField] private Slider _coolTimeSlider;

    [Header("Game Controller Reference")]
    [SerializeField] private GameController _gameController;

    [Header("Image to Fade from White to Black")]
    [SerializeField] private Image _coolTimeImage;

    [SerializeField] private float _fadeDuration = 5f; // 色を変化させる時間（秒）
    private float _elapsedTime = 0f;
    #endregion

    #region プロパティ
    #endregion

    #region メソッド

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update() {
        // クールタイムスライダーの更新
        _coolTimeSlider.value = _coolTimeSlider.maxValue - _gameController.CoolTime();

        if (_coolTimeSlider.value != 2) {
            _coolTimeImage.color = Color.grey;
        }
        else {
            _coolTimeImage.color = Color.yellow;
        }

    }

    #endregion
}
