using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankManagementWayPoint : MonoBehaviour
{
    #region 変数
    [SerializeField] private int _wayPointIndex; // ウェイポイントのインデックス
    #endregion

    #region メソッド
    public int WayPointIndex() {

        return _wayPointIndex;
    }
    #endregion
}
