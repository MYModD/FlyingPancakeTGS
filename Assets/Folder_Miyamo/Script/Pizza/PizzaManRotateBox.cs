using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaManRotateBox : MonoBehaviour
{

    public Transform[] _rotateBoxes;
    [Range(0,1f)]
    public float _offset;
    [Range(0, 10f)]
    public float _flameEuler;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float value = 0;
        for (int i = 0; i < _rotateBoxes.Length; i++) {

            _rotateBoxes[i].Rotate(new Vector3(0, _flameEuler+value, 0));
            value += _offset;

        }
    }
}
