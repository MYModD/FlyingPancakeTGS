using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissilePoolManager : PoolManager<TestMissile>
{


    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void FireMissile(Transform enemyTarget,Transform firePostion)
    {
        TestMissile missile = _objectPool.Get();
        missile.Initialize();                       //èâä˙âª
        missile.transform.position = firePostion.position;
        missile.target = enemyTarget;

    }

}


