using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LikePearentChild : MonoBehaviour
{

    [SerializeField, Header("親子関係になりたい親")]
    private Transform _parentTransform;



    void Start()
    {
        

    }

    private void FixedUpdate() {


        transform.position = _parentTransform.position;
        

    }
}
