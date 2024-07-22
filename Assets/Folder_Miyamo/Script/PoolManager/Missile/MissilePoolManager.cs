using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissilePoolManager : PoolManager<TestMissile> {

    [SerializeField,Header("missileにつける爆発プール")]
    public ExplosionPoolManager _explosionPoolManager;


    
    // testmissileでExplosionPoolManager設定すると
    // エラーが出たためCreate()したときに設定する    
    protected override TestMissile Create() {
        TestMissile missile = base.Create();
        missile._explosionPoolManager = this._explosionPoolManager;
        return missile;
    }


    /// <summary>
    /// 外部から実行される オブジェクトプールから取得する
    /// </summary>
    public void FireMissile(Transform enemyTarget, Transform firePosition) {
        TestMissile missile = _objectPool.Get();
        missile.Initialize();                       //初期化
        missile.transform.SetPositionAndRotation(firePosition.position, firePosition.rotation);
        missile._enemyTarget = enemyTarget;
    }
}