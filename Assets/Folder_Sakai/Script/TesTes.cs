using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesTes : MonoBehaviour {
    [SerializeField] private GameObject _as; // 対象のオブジェクト

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Y)) {
            GetComponent<ParticleSystem>().Play();
        }
    }
}
