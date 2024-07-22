using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissilePoolManager : PoolManager<TestMissile> {
    
    public ExplosionPoolManager _explosionPoolManager;

    protected override TestMissile Create() {
        TestMissile missile = base.Create();
        missile._explosionPoolManager = this._explosionPoolManager;
        return missile;
    }

    public void FireMissile(Transform enemyTarget, Transform firePosition) {
        TestMissile missile = _objectPool.Get();
        missile.Initialize();                       //èâä˙âª
        missile.transform.SetPositionAndRotation(firePosition.position, firePosition.rotation);
        missile._enemyTarget = enemyTarget;
    }
}