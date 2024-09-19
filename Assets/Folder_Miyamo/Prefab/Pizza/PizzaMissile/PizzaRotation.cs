using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaRotation : MonoBehaviour
{


    public float _rotation;
    


    private void FixedUpdate() {

        transform.Rotate(new Vector3(0, 0, _rotation));

        
    }


   
}
