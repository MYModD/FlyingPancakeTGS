using NaughtyAttributes;
using UnityEngine;

public class GameController : MonoBehaviour {

    #region �ϐ�

    [Foldout("���b�N�I���n")]
    [SerializeField]
    private TestLockOnManager _testLockOnManager;

    [Foldout("�~�T�C������")]
    [SerializeField, Header("�~�T�C�����ˈʒu")]
    private Transform _missileLaunchPosition;

    [Foldout("�~�T�C������")]
    [SerializeField]
    private MissilePoolManager _missilePoolManager;

    [Foldout("�~�T�C������")]
    [SerializeField, Header("�~�T�C�����ˊԊu")]
    [Range(0,3f)]
    private float _missileFireInterval = 0.5f;

    [Foldout("�e�۔���")]
    [SerializeField]
    private BulletPoolManager _bulletPoolManager;

    [Foldout("�e�۔���")]
    [SerializeField, Header("�e�۔��ˈʒu")]
    private Transform _bulletLaunchPosition;

    [Foldout("�e�۔���")]
    [SerializeField, Header("�e�ۑ��x�{��")]
    private float _bulletSpeedMultiplier = 1000f;

    [Foldout("�e�۔���")]
    [SerializeField, Header("�e�۔��ˊԊu")]
    [Range(0, 1f)]
    private float _bulletFireInterval  = 0.5f;

    private float _missileCooldownTimer; // �~�T�C�����˂܂ł̎c�莞��
    private float _bulletCooldownTimer;   // �e�۔��˂܂ł̎c�莞��

    #endregion

    #region ���\�b�h

    void Update() {
        // �~�T�C�����˃N�[���^�C���̍X�V
        _missileCooldownTimer -= Time.deltaTime;

        // �~�T�C�����ˏ�������
        bool canFireMissile = (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Submit"))
                              && _missileCooldownTimer <= 0;

        if (canFireMissile) {
            _missilePoolManager.FireMissiles(_missileLaunchPosition);
            _missileCooldownTimer = _missileFireInterval;
            Debug.Log("�~�T�C������");
        }

        // �e�۔��˃N�[���^�C���̍X�V
        _bulletCooldownTimer -= Time.deltaTime;

        // �e�۔��ˏ�������
        bool canFireBullet = (Input.GetKeyDown(KeyCode.K) || Input.GetButton("Fire1"))
                              && _bulletCooldownTimer <= 0;

        if (canFireBullet) {
            _bulletPoolManager.FireBullet(_bulletLaunchPosition, _bulletSpeedMultiplier);
            _bulletCooldownTimer = _bulletFireInterval;
        }
    }
    #endregion
}