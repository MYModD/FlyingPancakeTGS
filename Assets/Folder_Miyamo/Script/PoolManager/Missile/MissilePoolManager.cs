using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissilePoolManager : PoolManager<TestMissile> {

    [SerializeField,Header("missile�ɂ��锚���v�[��")]
    public ExplosionPoolManager _explosionPoolManager;


    
    // testmissile��ExplosionPoolManager�ݒ肷���
    // �G���[���o������Create()�����Ƃ��ɐݒ肷��    
    protected override TestMissile Create() {
        TestMissile missile = base.Create();
        missile._explosionPoolManager = this._explosionPoolManager;
        return missile;
    }


    /// <summary>
    /// �O��������s����� �I�u�W�F�N�g�v�[������擾����
    /// </summary>
    public void FireMissile(Transform enemyTarget, Transform firePosition) {
        TestMissile missile = _objectPool.Get();
        missile.Initialize();                       //������
        missile.transform.SetPositionAndRotation(firePosition.position, firePosition.rotation);
        missile._enemyTarget = enemyTarget;
    }
}