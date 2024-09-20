using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PizzaIs256MillionColor : MonoBehaviour {
    [Range(0, 3f)]
    public float _value;

    public bool _isRight = false;


    private float _currentValue;
    private Image _image;
    // Start is called before the first frame update
    void Start() {
        _image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update() {



        if (_isRight) {


            _currentValue += _value;

            if (_currentValue > 360f) {

                _currentValue = 0;

            }

            _image.color = Color.HSVToRGB(_currentValue / 360f, 1, 1);


        }
    }
}
