using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LikePearentChild : MonoBehaviour
{

    [SerializeField, Header("�e�q�֌W�ɂȂ肽���e")]
    private Transform _parentTransform;



    void Start()
    {
        
    }

    private void FixedUpdate() {


        transform.position = _parentTransform.position;
        

    }
}
