using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class MissileStuck : MonoBehaviour
{
    [Header("�^�[�Q�b�g�ڕW")]
    public Transform _enemyTarget;

    [Header("�N�[���^�C��")]
    public float _coolTime = 0.3f;

    [Header("���ˉ\��")]
    public bool _isFire = true;


    private void Start() {
        _isFire = true;
    }

    public void Hit() {
        _isFire = false;
        Debug.Log("�͂��܂���");
        StartCoroutine(nameof(WaitForSecond));
    }

    IEnumerator WaitForSecond() {

        yield return new WaitForSeconds(_coolTime);
        _isFire = true;

    }


}
