using NaughtyAttributes;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Foldout("ミサイル")]

    [SerializeField, Header("ミサイルを指定")] MissilePoolManager _missilePoolManger;
    [SerializeField, Header("ミサイル発射位置")] Transform _firePostion;


    [Foldout("ロックオン")]
    [SerializeField] TestLockOnManager _testLockOnManager;









    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var enemy = _testLockOnManager.targetsInCone;
            foreach (var item in enemy)
            {
                _missilePoolManger.FireMissile(item, _firePostion);
            }

        }


        if (Input.GetKeyDown(KeyCode.K))
        {

        }
    }
}
