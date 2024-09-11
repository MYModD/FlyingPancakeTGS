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



    protected override EnemyMissile Create() {
        EnemyMissile enemyMissile =  base.Create();
        Rigidbody playerRigidbody = _player.GetComponent<Rigidbody>();
        enemyMissile._playerRigidbody = playerRigidbody;
        return enemyMissile;

    }





    /// <summary>
    /// �O��������s����� �I�u�W�F�N�g�v�[������擾����
    /// </summary>
    public void EnemyFireMissile(Transform firePodtion) {

        _firePostion = firePodtion;
        EnemyMissile missile = _objectPool.Get();   
        missile.Initialize();                       
        missile.transform.SetPositionAndRotation(_firePostion.position, _firePostion.rotation);
        missile._playerTarget = _playerTarget;
    }

   
}
