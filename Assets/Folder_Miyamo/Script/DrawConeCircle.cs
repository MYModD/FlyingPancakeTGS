using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawConeCircle : MonoBehaviour
{

    [SerializeField, Header("CameraManger")]
    private TestLockOnManager _testLockOnManager;

    

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

        Quaternion hoge = _testLockOnManager._circleRotation;

        transform.rotation = hoge;
        transform.position = _testLockOnManager._circleCenterPostion;


    }
}
