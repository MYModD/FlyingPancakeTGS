using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testpizzaopen : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator _anim;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J)) {

            _anim.SetTrigger("Start");
        }
    }
}
