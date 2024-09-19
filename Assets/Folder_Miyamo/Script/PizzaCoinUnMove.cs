using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaCoinUnMove : MonoBehaviour
{
    [Header("ë≈Çøè¡Ç∑íl")]
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
