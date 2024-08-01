using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class MissileStuck : MonoBehaviour
{
    
    public Transform _enemyTarget {get;private set;
    }

    [Header("ロックオンできる時間")]
    public float _timer = 5f;

    [Header("発射可能か")]
    public bool _canFire = true;

    [Header("撃ったか")]
    public bool _isFired = false;


    private void Start() {
        // 初期化
        Initialize();
    }




    public void TargetLockOn(Transform targetEnemy) {
        _canFire = false;
        _enemyTarget = targetEnemy;

        StartCoroutine(nameof(StartTimer));
    }


    IEnumerator StartTimer() {
        yield return new WaitForSeconds(_timer);
        Initialize();
    }





    public void FireMissile() {
         _isFired = true;


    }






    /// <summary>
    /// 初期化、
    /// </summary>
    public void Initialize() {
        _canFire = true;
        _isFired = false;
        _enemyTarget = null;

    }



}
