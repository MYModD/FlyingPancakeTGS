using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamegedByLeftPizza : MonoBehaviour
{
    [SerializeField]
    [Header("Player(MovingObj)")]
    public GameObject _playerMovingObj;

    public PizzaWanima _pizzaLeftArm;
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.SqrMagnitude(_playerMovingObj.transform.position - _pizzaLeftArm.transform.position);
        Debug.Log($"ÉvÉåÉCÉÑÅ[Ç∆ç∂òrÇÃãóó£{distance}");

    }
}
