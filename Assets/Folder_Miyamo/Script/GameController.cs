using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Splines;

public class GameController : MonoBehaviour
{
    [Foldout("�~�T�C���W")]
    [SerializeField,Header("���ˈʒu")]
    private Transform _firePostion;

    [Foldout("�~�T�C���W")]
    [SerializeField] 
    private MissilePoolManager _missilePoolManger;

    [Foldout("���b�N�I���n")]
    [SerializeField]
    private  TestLockOnManager _testLockOnManager;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))     //������inputsystem�Ɉڍs����Ƃ�����
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
