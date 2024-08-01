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

        // �X�y�[�X��������GamePad�̉��{�^���������Ă��Ă��N�[���^�C����0�ȉ��̂Ƃ�
        bool canFire = (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Submit"))
             && (_missileCooldownTimer <= 0);


        if (canFire)       
        {
            _missilePoolManager.FireMissiles(_fireMissilePosition);
        }

        if (Input.GetKey(KeyCode.K)) {
            _bulletPoolManager.FireBullet(_playerPostion,_fireBulletPosition, _bulletSpeedMultiplier);
        }
    }
    #endregion
}
