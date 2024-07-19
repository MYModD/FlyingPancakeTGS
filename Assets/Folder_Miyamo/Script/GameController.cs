using NaughtyAttributes;
using UnityEngine;

public class GameController : MonoBehaviour
{



    [Foldout("�~�T�C���W")]
    [SerializeField,Header("���ˈʒu")] Transform _firePostion;
    [Foldout("�~�T�C���W")]
    [SerializeField, Header("�v�[���}�l�[�W���[")] MissilePoolManager _missilePoolManger;

    [Foldout("���b�N�I���n")]
    [SerializeField] TestLockOnManager _testLockOnManager;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))     //������inputsystem�Ɉڍs����Ƃ�������
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
