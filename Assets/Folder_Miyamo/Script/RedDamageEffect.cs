using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System;  // UniTask���g�p���邽�߂̃��C�u������ǉ�

public class RedDamageEffect : MonoBehaviour {
    [SerializeField] private Image _redDamage;  // _redDamage Image���V���A���C�Y
    public bool _isDamageActive = false;       // �_���[�W�G�t�F�N�g���A�N�e�B�u���ǂ���
    public float _duration;
    private void Start() {
        _redDamage.enabled = false;// �ŏ��͔�\���ɐݒ�
    }

    // �_���[�W���ɌĂяo����郁�\�b�h
    public void PlayerDamage() {
        if (_isDamageActive) {
        
            return;  // ���ɃG�t�F�N�g���\������Ă���ꍇ�͉������Ȃ�

        }

        // �_���[�W�G�t�F�N�g��\��
        _redDamage.enabled = true;
        _isDamageActive = true;

        // ��莞�Ԍ�ɃG�t�F�N�g���\���ɂ���
        HideRedDamageAsync().Forget();
    }

    /// <summary>
    /// ��莞�Ԍ��Image���\���ɂ���񓯊�����
    /// </summary>
    /// <returns></returns>
    private async UniTaskVoid HideRedDamageAsync() {
        // �w�肵�����ԁi��: 2�b�ԁj�ҋ@
        await UniTask.Delay(TimeSpan.FromSeconds(_duration));

        // �G�t�F�N�g���\���ɂ��A�������I��������Ƃ������t���O�����Z�b�g
        _redDamage.enabled = false;
        _isDamageActive = false;
    }
}
