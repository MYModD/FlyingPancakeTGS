using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Splines;

public class GameController : MonoBehaviour
{
    [Foldout("�~�T�C���W")]
    [SerializeField,Header("�~�T�C�����ˈʒu")]
    private Transform _fireMissilePostion;

    [Foldout("�~�T�C���W")]
    [SerializeField] 
    private MissilePoolManager _missilePoolManger;

    [Foldout("���b�N�I���n")]
    [SerializeField]
    private  TestLockOnManager _testLockOnManager;

    [Foldout("�e��")]
    [SerializeField]
    private BulletPoolManager _bulletPoolManager;
    [Foldout("�e��")]
    [SerializeField, Header("�e�۔��ˈʒu")]
    private Transform _fireBulletPostion;
    [Foldout("�e��")]
    [SerializeField, Header("�e�ۂ̑��x")]
    private float _bulletMultiply;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))     //������inputsystem�Ɉڍs����Ƃ�����
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
