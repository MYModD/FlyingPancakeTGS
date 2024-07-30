using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class MissileStuck : MonoBehaviour
{
    [Header("ターゲット目標")]
    public Transform _enemyTarget;

    [Header("クールタイム")]
    public float _coolTime = 0.3f;

    [Header("発射可能か")]
    public bool _isFire = true;


    private void Start() {
        _isFire = true;
    }

    public void Hit() {
        _isFire = false;
        Debug.Log("はじまった");
        StartCoroutine(nameof(WaitForSecond));
    }

    IEnumerator WaitForSecond() {

        yield return new WaitForSeconds(_coolTime);
        _isFire = true;

    }


}
