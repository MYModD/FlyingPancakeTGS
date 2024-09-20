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

    [Header("���ȏ�Ń��~�G���̃^�O�������邩")]
    [SerializeField]
    private int _chengeTagValue = 60;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        if (_currentValue > _chengeTagValue) {


            _pizzaMan.tag = _pizzaEnemyTag;//��萔�B������s�U�}���̃^�O���G�ɕς��X�N���v�g
            

        }


    }

    private void OnTriggerEnter2D(Collider2D collision) {


       
    }

    public void TouchMyField() {

        _currentValue++;
        _text.text = _currentValue.ToString();

    }


}
