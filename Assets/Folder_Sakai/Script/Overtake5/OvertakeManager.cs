using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvertakeManager : MonoBehaviour {
    #region 変数

    [SerializeField] private List<GameObject> _aircraft; // レースに参加する機体リスト
    [SerializeField] float _moveSpeed;
    #endregion

    #region メソッド

    public void StartMoving() {
        foreach (GameObject aircraft in _aircraft) {
            // 各機体のMoveSplineコンポーネントを取得
            MoveSpline moveSpline = aircraft.GetComponent<MoveSpline>();

            if (moveSpline != null) {
                // 取得したMoveSplineコンポーネントで何らかの操作を行う
                moveSpline.ChageSpeed(_moveSpeed);
            }
        }
    }

    #endregion
}
