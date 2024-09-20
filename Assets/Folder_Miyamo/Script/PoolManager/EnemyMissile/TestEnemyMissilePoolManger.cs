using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class TestEnemyMissilePoolManger : PoolManager<EnemyMissile>
{
    

    [SerializeField, Header("プレイヤー")]
    private Transform _player;

    [SerializeField, Header("標準目標")]
    private Transform _playerTarget;


    [SerializeField, Header("発射位置")]
    private Transform _firePostion;



    





    /// <summary>
    /// 外部から実行される オブジェクトプールから取得する
    /// </summary>
    public void EnemyFireMissile(Transform firePodtion) {

        _firePostion = firePodtion;
        EnemyMissile missile = ObjectPool.Get();   
        missile.Initialize();                       
        missile.transform.SetPositionAndRotation(_firePostion.position, _firePostion.rotation);
        missile._enemyTarget = _playerTarget;
    }

   
}
