using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEnemyMissileDebug : MonoBehaviour
{
    public TestEnemyMissilePoolManger _testEnemyMissilePool;

    public Transform _fireMissilePostion;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)) {

            _testEnemyMissilePool.EnemyFireMissile(_fireMissilePostion);
        }
    }
}
