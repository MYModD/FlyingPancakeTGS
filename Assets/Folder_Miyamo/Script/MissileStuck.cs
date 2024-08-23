using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using UnityEngine;

public class MissileStuck : MonoBehaviour
{
    [ReadOnly,Header("���������s��")]
    public Transform _enemyTarget = default;

    [Header("���b�N�I���ł��鎞��")]
    public float _timer = 5f;

    [Header("����\��")]
    public bool _isValueAssignable = default;

    
    private void Start() {
        // ������
        Initialize();
    }


    //���b�N�I���ł��邩�ƒl�����ł��邩��2��bool�l���K�v�Ȃ̂ŉ��P����K�v������
    //���������ƂɍĂь��Ă�悤�ɂ���v���O�������K�v�Ȃ̂�
    public void TargetLockOn(Transform targetEnemy) {
        _isValueAssignable = false;
        _enemyTarget = targetEnemy;

        StartCoroutine(nameof(StartTimer));
    }



    /// <summary>
    /// ��莞�Ԍ�ɏ���������R���[�`��
    /// </summary>
    IEnumerator StartTimer() {
        yield return new WaitForSeconds(_timer);
        Initialize();
    }



    /// <summary>
    /// �������A
    /// </summary>
    public void Initialize() {

        _isValueAssignable = true;
        _enemyTarget = null;

    }



}
