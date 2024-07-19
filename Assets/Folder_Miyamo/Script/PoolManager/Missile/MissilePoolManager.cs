using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissilePoolManager : PoolManager<TestMissile>
{
   
   
    public void FireMissile(Transform enemyTarget,Transform firePostion)
    {
        TestMissile missile = _objectPool.Get();

        missile.Initialize();                       //‰Šú‰»
        missile.transform.position = firePostion.position;
        missile._enemyTarget = enemyTarget;

    }

}


