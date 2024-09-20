using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletPoolManger : PoolManager<EnemyBullet> {
    [SerializeField, Header("発射位置")]
    private Transform _firePostion;

    /// <summary>
    /// 外部から実行される オブジェクトプールから取得する
    /// </summary>
    public void EnemyFireBullet() {
        EnemyBullet enemyBullet = ObjectPool.Get();
        enemyBullet.Initialize();
        enemyBullet.transform.SetPositionAndRotation(_firePostion.position, _firePostion.rotation);
    }
}
