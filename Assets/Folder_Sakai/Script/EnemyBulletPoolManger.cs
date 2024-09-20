using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletPoolManger : PoolManager<EnemyBullet> {
    [SerializeField, Header("���ˈʒu")]
    private Transform _firePostion;

    /// <summary>
    /// �O��������s����� �I�u�W�F�N�g�v�[������擾����
    /// </summary>
    public void EnemyFireBullet() {
        EnemyBullet enemyBullet = ObjectPool.Get();
        enemyBullet.Initialize();
        enemyBullet.transform.SetPositionAndRotation(_firePostion.position, _firePostion.rotation);
    }
}
