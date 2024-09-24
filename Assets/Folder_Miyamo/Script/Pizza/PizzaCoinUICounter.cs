using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PizzaCoinUICounter : MonoBehaviour {

    public TextMeshProUGUI _text;


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



    // Update is called once per frame
    void Update() {
        if (_currentValue > _chengeTagValue) {


            _pizzaMan.tag = _pizzaEnemyTag;//一定数達したらピザマンのタグが敵に変わるスクリプト


        }


    }


    public void TouchMyField() {

        _currentValue++;
        _text.text = _currentValue.ToString();

    }

    public void DegreePizzaCoin() {

        _currentValue = Mathf.Max(0, _currentValue - _degreeValue);

        _text.text = _currentValue.ToString();



    }






}
