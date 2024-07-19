using NaughtyAttributes;
using UnityEngine;

public class GameController : MonoBehaviour
{



    [Foldout("ミサイル係")]
    [SerializeField,Header("発射位置")] Transform _firePostion;
    [Foldout("ミサイル係")]
    [SerializeField, Header("プールマネージャー")] MissilePoolManager _missilePoolManger;

    [Foldout("ロックオン系")]
    [SerializeField] TestLockOnManager _testLockOnManager;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))     //ここをinputsystemに移行するとき直せる
        {
            var enemy = _testLockOnManager.targetsInCone;

            foreach (Transform item in enemy)
            {
                _missilePoolManger.FireMissile(item, _firePostion);
            }

        }


        if (Input.GetKeyDown(KeyCode.K))
        {

        }
    }
}
