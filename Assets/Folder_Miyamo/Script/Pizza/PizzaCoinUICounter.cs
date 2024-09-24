using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using NaughtyAttributes;

public class PizzaCoinUICounter : MonoBehaviour {
    public PizzaMan _pizzaMan;
    [Tag]
    public string _pizzaCoinUITag;
    [Tag]
    public string _pizzaEnemyTag;
    [Header("今のコイン枚数")]
    public int _currentValue;
    [Header("ダメージ受けたときの減る量")]
    public int _degreeValue = 5;
    [Header("何個以上でラミエルのタグをかえるか")]
    [SerializeField]
    private int _chengeTagValue = 60;
    public Image _10Image;
    public Image _01Image;
    public Sprite[] _setImageSource;
    public DegreePizzaEffect _degreePizzaEffect;

    void Update() {
        if (_currentValue > _chengeTagValue) {
            _pizzaMan.tag = _pizzaEnemyTag;
        }
    }

    public void TouchMyField() {
        _currentValue++;
        UpdateTextAndImages();
    }

    public void DegreePizzaCoin() {
        _currentValue = Mathf.Max(0, _currentValue - _degreeValue);
        UpdateTextAndImages();
        _degreePizzaEffect.StartDegreeEffect();
    }

    private void UpdateTextAndImages() {

        // 十の位の処理
        int tensPlace = _currentValue / 10;
        // 一の位の処理
        int onesPlace = _currentValue % 10;

        _10Image.sprite = _setImageSource[Mathf.Min(tensPlace, 9)];
        _01Image.sprite = _setImageSource[onesPlace];
    }

    [Button]
    public void UpdateTextToImage() {
        UpdateTextAndImages();
    }
}