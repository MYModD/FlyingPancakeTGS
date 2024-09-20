using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PizzaCoinUICounter : MonoBehaviour
{

    public TextMeshProUGUI _text;

    public int _currentValue;

    public PizzaMan _pizzaMan;
    [Tag]
    public string _pizzaCoinUITag;

    [Tag]
    public string _pizzaEnemyTag;

    [Header("何個以上でラミエルのタグをかえるか")]
    [SerializeField]
    private int _chengeTagValue = 60;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        if (_currentValue > _chengeTagValue) {


            _pizzaMan.tag = _pizzaEnemyTag;//一定数達したらピザマンのタグが敵に変わるスクリプト
            

        }


    }

    private void OnTriggerEnter2D(Collider2D collision) {


       
    }

    public void TouchMyField() {

        _currentValue++;
        _text.text = _currentValue.ToString();

    }


}
