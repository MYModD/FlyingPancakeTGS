using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaCoinCount : MonoBehaviour
{
    [SerializeField, Tag]
    private string _pizzaTag;

    [SerializeField]
    private int _pizzaCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) {


        if (other.CompareTag(_pizzaTag)) {

            _pizzaCount++;
            other.gameObject.SetActive(false);
        }
    }
}
