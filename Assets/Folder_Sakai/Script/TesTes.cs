using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesTes : MonoBehaviour {
    [SerializeField] private GameObject _as; // �Ώۂ̃I�u�W�F�N�g

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Y)) {
            // _as ���A�N�e�B�u�Ȃ��A�N�e�B�u�ɁA��A�N�e�B�u�Ȃ�A�N�e�B�u�ɐ؂�ւ���
            bool isActive = _as.activeSelf;
            _as.SetActive(!isActive);
        }
    }
}
