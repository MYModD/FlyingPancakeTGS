using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using UnityEngine;

public class MissileStuck : MonoBehaviour
{
    [ReadOnly,Header("書き換え不可")]
    public Transform _enemyTarget = default;

    [Header("ロックオンできる時間")]
    public float _timer = 5f;

    [Header("代入可能か")]
    public bool _isValueAssignable = default;

    
    private void Start() {
        // 初期化
        Initialize();
    }


    //ロックオンできるかと値を代入できるかの2つのbool値が必要なので改善する必要がある
    //撃ったあとに再び撃てるようにするプログラムが必要なのだ
    public void TargetLockOn(Transform targetEnemy) {
        _isValueAssignable = false;
        _enemyTarget = targetEnemy;

        StartCoroutine(nameof(StartTimer));
    }



    /// <summary>
    /// 一定時間後に初期化するコルーチン
    /// </summary>
    IEnumerator StartTimer() {
        yield return new WaitForSeconds(_timer);
        Initialize();
    }



    /// <summary>
    /// 初期化、
    /// </summary>
    public void Initialize() {

        _isValueAssignable = true;
        _enemyTarget = null;

    }



}
