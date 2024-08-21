using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatTheEnemyManager : MonoBehaviour
{

    #region 変数
    public enum Proposition {


    }

    [SerializeField] private List<GameObject> _firstGroupaircraft; // 撃破目標機体リスト第一群
    [SerializeField] private List<GameObject> _secondGroupaircraft; // 撃破目標機体リスト第二群
    [SerializeField] float _firstGroupmoveSpeed;
    #endregion
    #region　メソッド
    public void FirstGroupStartMoving() {
        foreach (GameObject aircraft in _firstGroupaircraft) {
            // 各機体のMoveSplineコンポーネントを取得
            MoveSpline moveSpline = aircraft.GetComponent<MoveSpline>();

            if (moveSpline != null) {
                // 取得したMoveSplineコンポーネントで何らかの操作を行う
                moveSpline.ChageSpeed(_firstGroupmoveSpeed);
            }
        }
    }
    #endregion
}
