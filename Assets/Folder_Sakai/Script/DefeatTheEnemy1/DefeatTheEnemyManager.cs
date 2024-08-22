using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatTheEnemyManager : MonoBehaviour {
    #region 変数

    //public enum Proposition {
    //    定義を必要に応じて追加
    //}

    [SerializeField] private List<GameObject> _firstGroupaircraft; // 撃破目標機体リスト第一群
    [SerializeField] private List<GameObject> _secondGroupaircraft; // 撃破目標機体リスト第二群
    [SerializeField] private float _firstGroupmoveSpeed; // 第一群の移動速度
    [SerializeField] private float _secondGroupmoveSpeed; // 第二群の移動速度
    [SerializeField] private CountTheNumberOfDefeats _countTheNumberOfDefeats;

    private int _score;
    #endregion

    #region メソッド

    /// <summary>
    /// 撃破目標機体第一群をアクティブ化してスタートさせるメソッド
    /// </summary>
    public void FirstGroupStartMoving() {
        foreach (GameObject aircraft in _firstGroupaircraft) {
            // 機体をアクティブ化
            aircraft.SetActive(true);

            // 各機体のMoveSplineコンポーネントを取得
            MoveSpline moveSpline = aircraft.GetComponent<MoveSpline>();

            if (moveSpline != null) {
                // 取得したMoveSplineコンポーネントで速度を変更する
                moveSpline.ChageSpeed(_firstGroupmoveSpeed);
            }
        }
    }

    public void SecondGroupStartMoving() {

        foreach (GameObject aircraft in _secondGroupaircraft) {
            //// 機体をアクティブ化
            //aircraft.SetActive(true);

            // 各機体のMoveSplineコンポーネントを取得
            MoveSpline moveSpline = aircraft.GetComponent<MoveSpline>();

            if (moveSpline != null) {
                // 取得したMoveSplineコンポーネントで速度を変更する
                moveSpline.ChageSpeed(_secondGroupmoveSpeed);
            }
        }
    }

    public void ScoreCalculation() {

        _score = _countTheNumberOfDefeats.NumberOfDefeats();
    }
    #endregion
}
