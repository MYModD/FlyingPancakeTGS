using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class GameController3rd : MonoBehaviour
{
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
    [Range(0, 3f)]
    private float _missileFireInterval = 0.5f;

    [SerializeField, Header("ミサイル残弾数")]
    private int _missileRoundsRemaining = 2;


    private float _missileCooldownTimer; // ミサイル発射までの残り時間
    private float _bulletCooldownTimer;   // 弾丸発射までの残り時間

    #endregion

    #region メソッド

    void Update() {
        print("残弾数" + _missileRoundsRemaining);

        // ミサイル発射クールタイムの更新
        _missileCooldownTimer -= Time.deltaTime;

        // ミサイル発射条件判定
        bool canFireMissile = (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Submit"))
                              && _missileCooldownTimer <= 0 && _missileRoundsRemaining >= 1;

        if (canFireMissile) {
            _missileRoundsRemaining--;
            print(_missileRoundsRemaining);
            _missilePoolManager.FireMissiles(_missileLaunchPosition);
            _missileCooldownTimer = _missileFireInterval;
            Debug.Log("ミサイル発射");
        }

    }

    public void RoundsRemainingIncrease(int increaseValue) {

        _missileRoundsRemaining += increaseValue;

        print("残弾数" + _missileRoundsRemaining);
    }
    #endregion
}
