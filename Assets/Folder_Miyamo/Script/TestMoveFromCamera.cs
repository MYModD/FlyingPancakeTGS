using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMoveFromCamera : MonoBehaviour
{
    public TestLockOnManager _lockOnManager;
        // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _lockOnManager._circleCenterPostion;
    }
}
