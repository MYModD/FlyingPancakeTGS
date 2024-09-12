using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PizzaCoinCount : MonoBehaviour {
    [SerializeField, Tag]
    private string _pizzaTag;

    [SerializeField, Tag]
    private string _pizzaLeftArmTag;

    [SerializeField]
    private int _pizzaCount = 0;

    [SerializeField, Header("‰½ŒÂ‚ÅŽŸs‚­")]
    private float _maxPizzaCoin;

    public EnemyMissile _missile;
    public TimeLimit _timelimit;
    public TextMeshProUGUI _text;

    [SerializeField,Range(0,3f)]
    public float _decreaseInterval = 1f; // Œ¸­‚·‚éŠÔŠui•bj

    [SerializeField]
    public int _decreaseAmount = 1; // ˆê“x‚ÉŒ¸­‚·‚é—Ê

    private float _lastDecreaseTime;

    void Start() {
        _lastDecreaseTime = Time.time;
    }

    

    private void OnTriggerEnter(Collider other) {
        Debug.Log($"‚Ô‚Â‚©‚Á‚½‚â‚Â : {other.gameObject.name}");

        if (other.CompareTag(_pizzaTag)) {
            _pizzaCount++;
            other.gameObject.SetActive(false);
            UpdatePizzaCountText();

            if (_pizzaCount >= _maxPizzaCoin) {
                // ‚±‚±‚É”j‰óˆ—‘‚­
                Destroy(GameObject.Find("PizzaMan"));
                _timelimit.End3rdGame();
            }
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag(_pizzaLeftArmTag)) {
            if (Time.time - _lastDecreaseTime >= _decreaseInterval) {
                _pizzaCount = Mathf.Max(0, _pizzaCount - _decreaseAmount);
                _lastDecreaseTime = Time.time;
                UpdatePizzaCountText();
            }
        }
    }

    private void UpdatePizzaCountText() {
        _text.text = _pizzaCount.ToString();
    }
}