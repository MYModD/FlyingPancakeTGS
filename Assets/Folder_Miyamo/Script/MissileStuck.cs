using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using UnityEngine;

public class MissileStuck : MonoBehaviour
{
    [ReadOnly,Header("���������s��")]
    public Transform _enemyTarget;

    [Header("���b�N�I���ł��鎞��")]
    public float _timer = 5f;

    [Header("���ˉ\��")]
    public bool _canFire = false;

    [Header("��������")]
    public bool _isFired = false;


    private void Start() {
        // ������
        Initialize();
    }


    //���b�N�I���ł��邩�ƒl�����ł��邩��2��bool�l���K�v�Ȃ̂ŉ��P����K�v������

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
    /// �������A
    /// </summary>
    public void Initialize() {
        _canFire = false;
        _isFired = false;
        _enemyTarget = null;

    }



}
