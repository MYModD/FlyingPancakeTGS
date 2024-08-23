using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���ۃN���X���p�����Ă��āAT��IPoolObject�Ƃ����C���^�[�t�F�[�X���K�v
/// </summary>
public class MissilePoolManager : PoolManager<TestMissile> {

    [SerializeField,Header("missile�ɂ��锚���v�[��")]
    public ExplosionPoolManager _explosionPoolManager;

    [SerializeField, Header("�~�T�C���X�^�b�N�̔z��")]
    private MissileStuck[] _missileStucks;
    
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
        foreach (MissileStuck item in _missileStucks) {

            // _isValueAssignable�����łɑ������Ă�����̂������˂��Ăق�������false
            bool canFire = item._enemyTarget != null && item._isValueAssignable == false;
            if (canFire) {

                missileNum++;
                Debug.Log($"{missileNum}���ڔ���");


                TestMissile missile = _objectPool.Get();
                missile.Initialize();                       //������
                
                missile.transform.SetPositionAndRotation(firePosition.position, firePosition.rotation);
                missile._enemyTarget = item._enemyTarget;

                item._enemyTarget = null;

            }

        }
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