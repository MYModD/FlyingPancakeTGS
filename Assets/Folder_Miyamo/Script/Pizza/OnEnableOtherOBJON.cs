using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnableOtherOBJON : MonoBehaviour
{
    public GameObject[] _offToOnObj;

    public GameObject _fuckU;

    private void OnEnable() {

        Instantiate(_fuckU);

        foreach (GameObject item in _offToOnObj) {

            item.SetActive(true);
        }
    }
}
