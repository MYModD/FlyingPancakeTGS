using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class MissileStuck : MonoBehaviour
{
    [SerializeField,Header("�^�[�Q�b�g�ڕW")]
    private  Transform _enemyTarget;

    [Header("�N�[���^�C��")]
    public float _coolTime = 0.3f;

    [Header("���ˉ\��")]
    public bool _canFire = true;

    [Header("��������")]
    public bool _isFired = false;


    private void Start() {
        _canFire = true;
    }

    public void LockOnReady(Transform targetEnemy) {
        _canFire = false;
        _enemyTarget = targetEnemy;
    }



    


}
