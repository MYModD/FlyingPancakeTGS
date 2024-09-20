using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AKahen : MonoBehaviour
{
    [SerializeField] private AircraftAnimation _aircraftAnimation;
    [SerializeField, Tag] private string _tag;
    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag(_tag)){

            _aircraftAnimation.Variable();
        }
    }
}
