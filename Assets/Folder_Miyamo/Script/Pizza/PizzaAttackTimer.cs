using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaAttackTimer : MonoBehaviour {
    public PizzaWanima _pizzaWanima;
    public FirePizzaMissileToPlayer _firePizzaMissileToPlayer;
    public PizzaCoinInstance _pizzaCoinInstance;

    public float _delay = 3f; // 遅延時間（秒）、インスペクターで調整可能

    private void OnEnable() {
        StartCoroutine(EnableAttackComponents());
    }

    private IEnumerator EnableAttackComponents() {
        // 指定された遅延時間だけ待機
        yield return new WaitForSeconds(_delay);

        // PizzaWanimaを有効化
        if (_pizzaWanima != null) {
            _pizzaWanima.enabled = true;
        } else {
            Debug.LogWarning("PizzaWanima is not assigned!");
        }

        // FirePizzaMissileToPlayerを有効化
        if (_firePizzaMissileToPlayer != null) {
            _firePizzaMissileToPlayer.enabled = true;
        } else {
            Debug.LogWarning("FirePizzaMissileToPlayer is not assigned!");
        }

        if (_pizzaCoinInstance.enabled == true) {
            Debug.LogError("FalseからTrueになるべき処理が最初からTrueになっている");

        } else {
            
            _pizzaCoinInstance.enabled = true;

        }



    }
}