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
            _missileCooldownTimer = _missileCoolTime;
            Debug.Log("ボタン押した");
        }

        
    }
    #endregion
}
