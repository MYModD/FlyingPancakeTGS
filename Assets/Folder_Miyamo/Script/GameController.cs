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


    [Header("4thMapのMemo")]
    [SerializeField]
    private Memo _memo;

    [Header("4thのミサイルスピード")]
    [SerializeField]
    private float _4thMissileSpeed;


    [Foldout("ミサイル発射")]
    [SerializeField, Header("ミサイル発射間隔")]
    [Range(0, 3f)]
    private float _missileFireInterval = 0.5f;

    [Header("クールタイムの割合"), ReadOnly]
    public float _coolTimeRatio;

    private float _missileCooldownTimer; // ミサイル発射までの残り時間
    private float _bulletCooldownTimer;   // 弾丸発射までの残り時間
    #endregion

    #region メソッド
    void Update() {
        // ミサイル発射クールタイムの更新
        _missileCooldownTimer -= Time.deltaTime;
        _missileCooldownTimer = Mathf.Max(_missileCooldownTimer, 0f); // 0未満にならないようにする


        // ミサイル発射条件判定
        bool canFireMissile = (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Submit"))
                              && _missileCooldownTimer <= 0;

        if (canFireMissile) {

            if (_memo.gameObject.activeSelf == true) {

                // 4thマップ ピザラミエルがONの場合

                _missilePoolManager.FireMissilesSpeed4th(_missileLaunchPosition, _4thMissileSpeed);
                _missileCooldownTimer = _missileFireInterval;
                Debug.Log("4thVerのミサイル発射");



            } else {

                _missilePoolManager.FireMissiles(_missileLaunchPosition);
                _missileCooldownTimer = _missileFireInterval;
                Debug.Log("ミサイル発射");

            }






            
        }

        // クールタイムの割合を計算（0~1の範囲に収める）
        _coolTimeRatio = 1f - Mathf.Clamp01(_missileCooldownTimer / _missileFireInterval);
    }

    public float CoolTime() {
        return _missileCooldownTimer;
    }
    #endregion
}