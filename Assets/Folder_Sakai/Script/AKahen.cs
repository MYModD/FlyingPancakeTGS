using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AKahen : MonoBehaviour
{
    [SerializeField] private AircraftAnimation _aircraftAnimation1;
    [SerializeField] private AircraftAnimation _aircraftAnimation2;
    [SerializeField, Tag] private string _tag;
    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag(_tag)){
            print("colll");
            _aircraftAnimation1.Variable();
            _aircraftAnimation2.Variable();
        }
    }
}
