// ---------------------------------------------------------
// ScoreManager.cs
//
// 作成日:
// 作成者:
// ---------------------------------------------------------
using UnityEngine;
using System.Collections;
public class ScoreManager : MonoBehaviour {
    #region 変数
    private int _score;

    #endregion
    #region プロパティ
    #endregion
    #region メソッド
    public void UpdateScore(int addScore) {
        _score += addScore;
        //後でテキスト追加
    }
    #endregion
}