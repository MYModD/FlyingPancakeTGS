using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawConeCircle : MonoBehaviour
{

    [SerializeField, Header("CameraManger")]
    private TestLockOnManager _testLockOnManager;

    public bool _debugIsON = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    private void LateUpdate() {


        Quaternion hoge   = _testLockOnManager._circleRotation;

        transform.rotation = hoge;
        transform.position = _testLockOnManager._circleCenterPostion;

    }

    private void OnDrawGizmos() {

        if (!_debugIsON) {

            Quaternion hoge = _testLockOnManager._circleRotation;

            transform.rotation = hoge;
            transform.position = _testLockOnManager._circleCenterPostion;

            float scale = _testLockOnManager._circleRadius * 0.01487f;
            transform.localScale = new Vector3(scale, scale, scale);

        }
       
    }
}
