using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class As : MonoBehaviour
{
    [SerializeField] GameObject _a;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _a.SetActive(true);
    }
}
