using NaughtyAttributes;
using UnityEngine;

public class GameController : MonoBehaviour {

    #region �ϐ�

    
    [Foldout("�~�T�C���W")]
    [SerializeField, Header("�~�T�C�����ˈʒu")]
    private Transform _fireMissilePosition;

    [Foldout("�~�T�C���W")]
    [SerializeField]
    private MissilePoolManager _missilePoolManager;

    [Foldout("�~�T�C���W")]
    [SerializeField, Header("�N�[���^�C��")]
    private float _missileCoolTime�@= 1f;

    [Foldout("���b�N�I���n")]
    [SerializeField]
    private TestLockOnManager _testLockOnManager;

    [Foldout("�e��")]
    [SerializeField]
    private BulletPoolManager _bulletPoolManager;

    [Foldout("�e��")]
    [SerializeField, Header("�v���C���[")]
    private Transform _playerPostion;

    [Foldout("�e��")]
    [SerializeField, Header("�e�۔��ˈʒu")]
    private Transform _fireBulletPosition;

    [Foldout("�e��")]
    [SerializeField, Header("�e�ۂ̑��x")]
    private float _bulletSpeedMultiplier;
    
    

    private float _missileCooldownTimer; // �^�C�}�[�v�Z�p


    #endregion



    #region ���\�b�h

    
    void Update() {


        // �~�T�C�����˂̃N�[���^�C���v�Z
        _missileCooldownTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && _missileCooldownTimer <= 0) //������Input System�Ɉڍs����Ƃ�����
        {
            var enemies = _testLockOnManager._targetsInCone;

            foreach (Transform enemy in enemies) {

                _missilePoolManager.FireMissile(enemy, _fireMissilePosition);
            }

            // �N�[���^�C�������Z�b�g
            _missileCooldownTimer = _missileCoolTime;
        }

        if (Input.GetKey(KeyCode.K)) {
            _bulletPoolManager.FireBullet(_playerPostion,_fireBulletPosition, _bulletSpeedMultiplier);
        }
    }
    #endregion
}
