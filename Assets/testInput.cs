using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class testInput : MonoBehaviour
{
    public PlayerInput _playerInput;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var inputAction = _playerInput.actions["Move"];
    }
}
