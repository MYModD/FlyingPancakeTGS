using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 抽象クラスを継承していて、TはIPoolObjectというインターフェースが必要
/// </summary>
public class MissilePoolManager : PoolManager<TestMissile> {

    [SerializeField,Header("missileにつける爆発プール")]
    public ExplosionPoolManager _explosionPoolManager;

    [SerializeField, Header("ミサイルスタックの配列")]
    private MissileStuck[] _missileStucks;
    
    // testmissileでExplosionPoolManager設定すると
    // エラーが出たためCreate()したときに設定する    
    protected override TestMissile Create() {
        TestMissile missile = base.Create();
        missile._explosionPoolManager = _explosionPoolManager;
        return missile;
    }

    public void FireMissiles(Transform firePosition) {

        int missileNum = 0;

        Debug.Log("fire");
        foreach (MissileStuck item in _missileStucks) {

            // _isValueAssignableがすでに代入されているものだけ発射してほしいためfalse
            bool canFire = item._enemyTarget != null && item._isValueAssignable == false;
            if (canFire) {

                missileNum++;
                Debug.Log($"{missileNum}発目発射");


                TestMissile missile = _objectPool.Get();
                missile.Initialize();                       //初期化
                
                missile.transform.SetPositionAndRotation(firePosition.position, firePosition.rotation);
                missile._enemyTarget = item._enemyTarget;

                item._enemyTarget = null;

            }

        }
    }




    /*  gamepad側でforeachするよりこっちでforeachしたかったためコメント化
    /// <summary>
    /// 外部から実行される オブジェクトプールから取得する
    /// </summary>
    public void FireMissile(Transform enemyTarget, Transform firePosition) {
        TestMissile missile = _objectPool.Get();
        missile.Initialize();                       //初期化
        missile.transform.SetPositionAndRotation(firePosition.position, firePosition.rotation);
        missile._enemyTarget = enemyTarget;
    }
    */




}