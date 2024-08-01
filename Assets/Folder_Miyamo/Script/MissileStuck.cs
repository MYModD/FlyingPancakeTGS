using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class MissileStuck : MonoBehaviour
{
    
    public Transform _enemyTarget {get;private set;
    }

    [Header("���b�N�I���ł��鎞��")]
    public float _timer = 5f;

    [Header("���ˉ\��")]
    public bool _canFire = true;

    [Header("��������")]
    public bool _isFired = false;


    private void Start() {
        // ������
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
    /// �������A
    /// </summary>
    public void Initialize() {
        _canFire = true;
        _isFired = false;
        _enemyTarget = null;

    }



}
