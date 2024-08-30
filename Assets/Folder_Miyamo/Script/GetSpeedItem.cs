using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class GetSpeedItem : MonoBehaviour
{

    [SerializeField]
    private float _addSpeedValue;

    [SerializeField,Header("ç°ÇÃë¨ìx"),ReadOnly]
    private float _currentSpeed;



    [SerializeField]
    private SplineAnimate _splineAnimate;

    [SerializeField, Tag]
    private string _speedItemTag;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other) {


        if (other.CompareTag(_speedItemTag)) {

            Debug.Log("sppedItmeÇ§ÇØÇ∆ÇËÇ‹ÇµÇΩ");  

            _splineAnimate.ElapsedTime += _addSpeedValue;
            _currentSpeed = _splineAnimate.ElapsedTime;
            other.gameObject.SetActive(false);

        }



    }
}
