using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Splines;

public class GameController : MonoBehaviour
{
    [Foldout("ミサイル係")]
    [SerializeField,Header("発射位置")]
    private Transform _firePostion;

    [Foldout("ミサイル係")]
    [SerializeField] 
    private MissilePoolManager _missilePoolManger;

    [Foldout("ロックオン系")]
    [SerializeField]
    private  TestLockOnManager _testLockOnManager;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))     //ここをinputsystemに移行するとき直す
        {
            var enemy = _testLockOnManager._targetsInCone;

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
