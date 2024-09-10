using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LikePearentChild : MonoBehaviour
{

    [SerializeField, Header("êeéqä÷åWÇ…Ç»ÇËÇΩÇ¢êe")]
    private Transform _parentTransform;



    void Start()
    {
        
    }

    private void FixedUpdate() {


        transform.position = _parentTransform.position;
        

    }
}
