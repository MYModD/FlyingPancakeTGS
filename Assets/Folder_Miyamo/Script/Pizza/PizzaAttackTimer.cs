using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaAttackTimer : MonoBehaviour {
    public PizzaWanima _pizzaWanima;
    public FirePizzaMissileToPlayer _firePizzaMissileToPlayer;
    public PizzaCoinInstance _pizzaCoinInstance;

    public float _delay = 3f; // �x�����ԁi�b�j�A�C���X�y�N�^�[�Œ����\

    private void OnEnable() {
        StartCoroutine(EnableAttackComponents());
    }

    private IEnumerator EnableAttackComponents() {
        // �w�肳�ꂽ�x�����Ԃ����ҋ@
        yield return new WaitForSeconds(_delay);

        // PizzaWanima��L����
        if (_pizzaWanima != null) {
            _pizzaWanima.enabled = true;
        } else {
            Debug.LogWarning("PizzaWanima is not assigned!");
        }

        // FirePizzaMissileToPlayer��L����
        if (_firePizzaMissileToPlayer != null) {
            _firePizzaMissileToPlayer.enabled = true;
        } else {
            Debug.LogWarning("FirePizzaMissileToPlayer is not assigned!");
        }

        if (_pizzaCoinInstance.enabled == true) {
            Debug.LogError("False����True�ɂȂ�ׂ��������ŏ�����True�ɂȂ��Ă���");

        } else {
            
            _pizzaCoinInstance.enabled = true;

        }



    }
}