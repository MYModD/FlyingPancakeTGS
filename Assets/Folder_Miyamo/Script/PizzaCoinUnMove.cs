using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaCoinUnMove : MonoBehaviour
{
    [Header("打ち消す値")]
    public float _moveUnZ;

    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.localPosition -= new Vector3(0, 0, _moveUnZ);
    }
}
