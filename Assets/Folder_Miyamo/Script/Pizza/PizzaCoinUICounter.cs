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

    [Header("���̃R�C������")]
    public int _currentValue;


    [Header("�_���[�W�󂯂��Ƃ��̌����")]
    public int _degreeValue = 5;


    [Header("���ȏ�Ń��~�G���̃^�O�������邩")]
    [SerializeField]
    private int _chengeTagValue = 60;



    // Update is called once per frame
    void Update() {
        if (_currentValue > _chengeTagValue) {


            _pizzaMan.tag = _pizzaEnemyTag;//��萔�B������s�U�}���̃^�O���G�ɕς��X�N���v�g


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
