using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesTes : MonoBehaviour {
    [SerializeField] private GameObject _as; 

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Y)) {

            bool isActive = _as.activeSelf;
            _as.SetActive(!isActive);
        }
    }
}
