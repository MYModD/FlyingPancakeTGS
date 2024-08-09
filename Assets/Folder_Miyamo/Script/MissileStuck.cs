using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using UnityEngine;

public class MissileStuck : MonoBehaviour
{
    [ReadOnly,Header("書き換え不可")]
    public Transform _enemyTarget;

    [Header("ロックオンできる時間")]
    public float _timer = 5f;

    [Header("発射可能か")]
    public bool _canFire = false;

    [Header("撃ったか")]
    public bool _isFired = false;


    private void Start() {
        // 初期化
        Initialize();
    }


    //ロックオンできるかと値を代入できるかの2つのbool値が必要なので改善する必要がある

    public void TargetLockOn(Transform targetEnemy) {
        _canFire = true;
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
        _canFire = false;
        _isFired = false;
        _enemyTarget = null;

    }



}
