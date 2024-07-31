using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class MissileStuck : MonoBehaviour
{
    [SerializeField,Header("ターゲット目標")]
    private  Transform _enemyTarget;

    [Header("クールタイム")]
    public float _coolTime = 0.3f;

    [Header("発射可能か")]
    public bool _canFire = true;

    [Header("撃ったか")]
    public bool _isFired = false;


    private void Start() {
        _canFire = true;
    }

    public void LockOnReady(Transform targetEnemy) {
        _canFire = false;
        _enemyTarget = targetEnemy;
    }



    


}
