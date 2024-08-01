using NaughtyAttributes;
using UnityEngine;

public class GameController : MonoBehaviour {

    #region 変数

    
    [Foldout("ミサイル係")]
    [SerializeField, Header("ミサイル発射位置")]
    private Transform _fireMissilePosition;

    [Foldout("ミサイル係")]
    [SerializeField]
    private MissilePoolManager _missilePoolManager;

    [Foldout("ミサイル係")]
    [SerializeField, Header("クールタイム")]
    private float _missileCoolTime　= 1f;

    [Foldout("ロックオン系")]
    [SerializeField]
    private TestLockOnManager _testLockOnManager;

    [Foldout("弾丸")]
    [SerializeField]
    private BulletPoolManager _bulletPoolManager;

    [Foldout("弾丸")]
    [SerializeField, Header("プレイヤー")]
    private Transform _playerPostion;

    [Foldout("弾丸")]
    [SerializeField, Header("弾丸発射位置")]
    private Transform _fireBulletPosition;

    [Foldout("弾丸")]
    [SerializeField, Header("弾丸の速度")]
    private float _bulletSpeedMultiplier;
    
    

    private float _missileCooldownTimer; // タイマー計算用


    #endregion



    #region メソッド
    
    void Update() {


        // ミサイル発射のクールタイム計算
        _missileCooldownTimer -= Time.deltaTime;

        // スペースもしくはGamePadの下ボタンを押していてかつクールタイムが0以下のとき
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
