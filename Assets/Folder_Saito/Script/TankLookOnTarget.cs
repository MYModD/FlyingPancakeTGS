// ---------------------------------------------------------
// TankLookOnTarget.cs
//
// 作成日:
// 作成者:
// ---------------------------------------------------------
using UnityEngine;
using System.Collections;
public class TankLookOnTarget : MonoBehaviour {
    #region 変数
    [SerializeField, Header("基本的にプレイヤー")]
    private GameObject _target;

    [SerializeField, Header("上下したい発射口")] private GameObject _muzzle;
    [SerializeField, Header("回転したい発射口がついている土台")] private GameObject _turret;
    [SerializeField, Header("ターゲットとの距離がどのくらいの範囲になったらロックオンするか")]
    private float _distance;

    #endregion
    #region プロパティ
    #endregion
    #region メソッド
    /// <summary>
    /// 更新処理
    /// </summary>
    void Update() {
        //規定値よりも近くなったら
        if (_distance > Vector3.Distance(_target.transform.position, this.transform.position)) {
            //ターゲットを見る
            _muzzle.transform.LookAt(_target.transform);
            //高さだけ変えて見る
            Vector3 targetEnemy = new Vector3(_target.transform.position.x,
                this.transform.position.y + 1.5f, _target.transform.position.z);
            //ターゲット見る
            _turret.transform.LookAt(targetEnemy);
        }

    }
    #endregion
}