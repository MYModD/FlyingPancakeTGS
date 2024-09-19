using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnableOtherOBJON : MonoBehaviour
{
    public GameObject[] _offToOnObj;

    

    private void OnEnable() {


        foreach (GameObject item in _offToOnObj) {

            item.SetActive(true);
        }
    }
}
