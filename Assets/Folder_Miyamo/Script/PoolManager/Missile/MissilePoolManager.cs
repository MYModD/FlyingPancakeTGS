using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���ۃN���X���p�����Ă��āAT��IPoolObject�Ƃ����C���^�[�t�F�[�X���K�v
/// </summary>
public class MissilePoolManager : PoolManager<TestMissile> {

    [SerializeField, Header("missile�ɂ��锚���v�[��")]
    public ExplosionPoolManager _explosionPoolManager;

    [SerializeField, Header("lockOnManger")]
    private TestLockOnManager _testLockOnManager;

    // testmissile��ExplosionPoolManager�ݒ肷���
    // �G���[���o������Create()�����Ƃ��ɐݒ肷��    
    protected override TestMissile Create() {
        TestMissile missile = base.Create();
        missile._explosionPoolManager = _explosionPoolManager;
        return missile;
    }

    public void FireMissiles(Transform firePosition) {

        int missileNum = 0;

        Debug.Log("fire");
        foreach (Transform target in _testLockOnManager._targetsInCone) {

            // _isValueAssignable�����łɑ������Ă�����̂������˂��Ăق�������false

            missileNum++;
            Debug.Log($"{missileNum}���ڔ���");

            TestMissile missile = _objectPool.Get();
            missile.Initialize();                       //������
            missile.transform.SetPositionAndRotation(firePosition.position, firePosition.rotation);
            missile._enemyTarget = target;

            missile._testLockOnManager = _testLockOnManager;


        }
        _testLockOnManager.ClearConeTargetAndAddBlackList();

    }




    /*  gamepad����foreach�����肱������foreach�������������߃R�����g��
    /// <summary>
    /// �O��������s����� �I�u�W�F�N�g�v�[������擾����
    /// </summary>
    public void FireMissile(Transform enemyTarget, Transform firePosition) {
        TestMissile missile = _objectPool.Get();
        missile.Initialize();                       //������
        missile.transform.SetPositionAndRotation(firePosition.position, firePosition.rotation);
        missile._enemyTarget = enemyTarget;
    }
    */




}