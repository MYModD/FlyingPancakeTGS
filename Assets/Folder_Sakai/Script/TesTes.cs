using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesTes : MonoBehaviour {
    [SerializeField] private GameObject _as; // 対象のオブジェクト

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Y)) {
            // _as がアクティブなら非アクティブに、非アクティブならアクティブに切り替える
            bool isActive = _as.activeSelf;
            _as.SetActive(!isActive);
        }
    }
}
