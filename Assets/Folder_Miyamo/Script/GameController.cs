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

    

    private  float _missileCooldownTimer; // ミサイル発射までの残り時間
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

    }

    public float CoolTime() {
        return _missileCooldownTimer;
    }
    #endregion
}