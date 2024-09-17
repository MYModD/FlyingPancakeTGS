using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyChangeSpeed : MonoBehaviour
{

    #region 変数
    [SerializeField] private Transform _player;
    [SerializeField] private float _changeSpeedDistance;
    [SerializeField] private EnemyMoveSpline _enemyMoveSpline;
    #endregion
    #region メソッド

    private void Update() {
       
            CalculateDistance();
        
    }

    private void CalculateDistance() {
        if (this.gameObject != null && _player != null) {
            // 距離を計算
            float distance = Vector3.Distance(this.gameObject.transform.position,_player.position);

            //// 距離をログに表示
            //Debug.Log($"プレイヤーと敵機の距離: {distance}{this.gameObject.name}");

            if (_changeSpeedDistance <= distance) {
                _enemyMoveSpline.ChangeSpeed();
            }
        }
    }
    #endregion
}
