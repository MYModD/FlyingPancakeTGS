using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PizzaCoinUICounter : MonoBehaviour
{

    public TextMeshProUGUI _text;

    public int _currentValue;
    [Tag]
    public string _pizzaCoinUITag;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {


       
    }

    public void TouchMyField() {

        _currentValue++;
        _text.text = _currentValue.ToString();

    }


}
