using NaughtyAttributes;
using UnityEngine;

public class GameController : MonoBehaviour {

    #region 変数

    [Foldout("ロックオン系")]
    [SerializeField]
    private TestLockOnManager _testLockOnManager;

    [Foldout("ミサイル発射")]
    [SerializeField, Header("ミサイル発射位置")]
    private Transform _missileLaunchPosition;

    [Foldout("ミサイル発射")]
    [SerializeField]
    private MissilePoolManager _missilePoolManager;

    [Foldout("ミサイル発射")]
    [SerializeField, Header("ミサイル発射間隔")]
    [Range(0,3f)]
    private float _missileFireInterval = 0.5f;

    [Foldout("弾丸発射")]
    [SerializeField]
    private BulletPoolManager _bulletPoolManager;

    [Foldout("弾丸発射")]
    [SerializeField, Header("弾丸発射位置")]
    private Transform _bulletLaunchPosition;

    [Foldout("弾丸発射")]
    [SerializeField, Header("弾丸速度倍率")]
    private float _bulletSpeedMultiplier = 1000f;

    [Foldout("弾丸発射")]
    [SerializeField, Header("弾丸発射間隔")]
    [Range(0, 1f)]
    private float _bulletFireInterval  = 0.5f;

    private float _missileCooldownTimer; // ミサイル発射までの残り時間
    private float _bulletCooldownTimer;   // 弾丸発射までの残り時間

    #endregion

    #region メソッド

    void Update() {
        // ミサイル発射クールタイムの更新
        _missileCooldownTimer -= Time.deltaTime;

        // ミサイル発射条件判定
        bool canFireMissile = (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Submit"))
                              && _missileCooldownTimer <= 0;

        if (canFireMissile) {
            _missilePoolManager.FireMissiles(_missileLaunchPosition);
            _missileCooldownTimer = _missileFireInterval;
            Debug.Log("ミサイル発射");
        }

        // 弾丸発射クールタイムの更新
        _bulletCooldownTimer -= Time.deltaTime;

        // 弾丸発射条件判定
        bool canFireBullet = (Input.GetKeyDown(KeyCode.K) || Input.GetButton("Fire1"))
                              && _bulletCooldownTimer <= 0;

        if (canFireBullet) {
            _bulletPoolManager.FireBullet(_bulletLaunchPosition, _bulletSpeedMultiplier);
            _bulletCooldownTimer = _bulletFireInterval;
        }
    }
    #endregion
}