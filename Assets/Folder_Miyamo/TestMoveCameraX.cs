using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMoveCameraX : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W)) {

            transform.position += new Vector3(10, 0, 0);
        
        }
    }
}
