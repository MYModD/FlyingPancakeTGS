using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLimit : MonoBehaviour
{

    [Header("��������")]
    public float _limitTime = 60f;



    private float _hohgoehgoehogh;
    public bool _isStart = false;
    // ��Œ���

    public void LimitTimerStart() {
        _isStart = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isStart) {

            _limitTime -= Time.deltaTime;
            if (_limitTime <= 0f) {
                End3rdGame();
            }
        
        }
    }


    /// <summary>
    /// �^�C�}�[��0�ɂȂ����������́A1�ʂɂȂ����G�����j���ꂽ����s����
    /// </summary>
    public  void End3rdGame() {

        _isStart = false;
        float cashTimer = _limitTime;
        if (cashTimer <= 0) {
            cashTimer = 0;
        }

        Debug.Log($"3rd�}�b�v�̃^�C�}�[��  {cashTimer}  �ł���!!");

        //�����ɏ���


    }
}
