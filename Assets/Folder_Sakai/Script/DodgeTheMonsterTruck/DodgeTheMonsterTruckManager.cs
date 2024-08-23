using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeTheMonsterTruckManager : MonoBehaviour {
    #region 変数

    [SerializeField] private List<GameObject> _monsterTrucks;

    #endregion

    #region メソッド

    /// <summary>
    /// 指定されたモンスタートラックがリスト内に存在する場合、スピードを設定する
    /// </summary>
    /// <param name="monsterTruck">対象のモンスタートラック</param>
    /// <param name="speed">設定するスピード</param>
    public void StartMoving(GameObject monsterTruck, float speed) {
        foreach (GameObject truck in _monsterTrucks) {
            // monsterTruckと同じオブジェクトかどうかをチェック
            if (truck == monsterTruck) {
                // コンポーネントを取得して速度を設定する
                MoveSpline moveSpline = truck.GetComponent<MoveSpline>();

                if (moveSpline != null) {
                    moveSpline.ChageSpeed(speed);
                } 

                break; // 一致するトラックが見つかったらループを終了
            }
        }
    }


    #endregion
}
