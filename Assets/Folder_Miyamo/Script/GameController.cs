using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Splines;

public class GameController : MonoBehaviour
{
    [Foldout("ミサイル係")]
    [SerializeField,Header("ミサイル発射位置")]
    private Transform _fireMissilePostion;

    [Foldout("ミサイル係")]
    [SerializeField] 
    private MissilePoolManager _missilePoolManger;

    [Foldout("ロックオン系")]
    [SerializeField]
    private  TestLockOnManager _testLockOnManager;

    [Foldout("弾丸")]
    [SerializeField]
    private BulletPoolManager _bulletPoolManager;
    [Foldout("弾丸")]
    [SerializeField, Header("弾丸発射位置")]
    private Transform _fireBulletPostion;
    [Foldout("弾丸")]
    [SerializeField, Header("弾丸の速度")]
    private float _bulletMultiply;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))     //ここをinputsystemに移行するとき直す
        {
            var enemy = _testLockOnManager._targetsInCone;

            foreach (Transform item in enemy)
            {
                _missilePoolManger.FireMissile(item, _fireMissilePostion);
            }

        }


        if (Input.GetKey(KeyCode.K))
        {
            _bulletPoolManager.FireBullet(_fireBulletPostion,_bulletMultiply);
        }
    }
}
