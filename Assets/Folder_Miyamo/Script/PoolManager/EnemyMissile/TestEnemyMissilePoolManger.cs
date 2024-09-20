using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class TestEnemyMissilePoolManger : PoolManager<EnemyMissile>
{
    

    [SerializeField, Header("�v���C���[")]
    private Transform _player;

    [SerializeField, Header("�W���ڕW")]
    private Transform _playerTarget;


    [SerializeField, Header("���ˈʒu")]
    private Transform _firePostion;



    





    /// <summary>
    /// �O��������s����� �I�u�W�F�N�g�v�[������擾����
    /// </summary>
    public void EnemyFireMissile(Transform firePodtion) {

        _firePostion = firePodtion;
        EnemyMissile missile = ObjectPool.Get();   
        missile.Initialize();                       
        missile.transform.SetPositionAndRotation(_firePostion.position, _firePostion.rotation);
        missile._enemyTarget = _playerTarget;
    }

   
}
