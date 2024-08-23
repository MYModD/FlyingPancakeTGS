// ---------------------------------------------------------
// AircraftProgress.cs
//
// 作成日:
// 作成者:
// ---------------------------------------------------------
using UnityEngine;
using System.Collections;
public class AircraftProgress : MonoBehaviour {
    #region 変数
    //[SerializeField] private RankManager_RS _raceManager;

    private int _currentWaypoint = 0; // 現在のウェイポイントのインデックス
    private int _goalIndex = default; // ゴールのインデックス
    private bool _isJustOnce = false; // 1度だけ呼び出す

    #endregion
    #region メソッド
    /// <summary>
    /// 更新前処理
    /// </summary>
    void Start() {
        //ゴール地点のインデックスを0オリジンを考慮して設定
        //_goalIndex = _raceManager.GetWayPointCount() - 1;
    }


    /// <summary>
    /// 各機体のチェックポイントの進捗管理
    /// </summary>
    void OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag("RankCheckpoint")) {

            RankManagementWayPoint rankManagementWayPoint = other.GetComponent<RankManagementWayPoint>();

            if (rankManagementWayPoint != null) {

                //各ウェイポイントが保持しているインデックスを取得し現在の_currentWaypointで保持している
                //値と同じだった場合インクリメント処理をして次の目標を更新
                int wayPointIndex = rankManagementWayPoint.WayPointIndex();

                if (wayPointIndex == _currentWaypoint) {

                    _currentWaypoint++;
                }

                ////wayPointIndexがゴール判定をしているオブジェクトのインデックスに達した場合
                ////順位の確定処理を行う
                //if (wayPointIndex == _goalIndex) {

                //    if (!_isJustOnce) {
                //        _currentWaypoint += _raceManager.RankingDetermination();
                //        _isJustOnce = true;
                //    }

                //    //ゴールしたオブジェクトがプレイヤーオブジェクトの場合リザルト表示用のランキングを
                //    //作成する
                //    if (this.gameObject.tag == "Player") {
                //        _raceManager.RankingGeneration();
                //    }
                //}
            }
        }

    }

    /// <summary>
    /// 現在通過し終えた最後のウェイポイントのインデックス
    /// </summary>
    public int CurrentWaypoint() {

        return _currentWaypoint;
    }

    #endregion
}