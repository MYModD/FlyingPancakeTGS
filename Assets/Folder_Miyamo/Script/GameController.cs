using NaughtyAttributes;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Foldout("�~�T�C��")]

    [SerializeField, Header("�~�T�C�����w��")] MissilePoolManager _missilePoolManger;
    [SerializeField, Header("�~�T�C�����ˈʒu")] Transform _firePostion;


    [Foldout("���b�N�I��")]
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
