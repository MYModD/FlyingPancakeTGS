using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacumeToPlayer : MonoBehaviour
{

    private bool _isVaume = false;
    public GameObject _player;
    public float _vacumeSpeed;



    private void OnEnable() {


        _isVaume = true; 
    }

    private void Update() {
        if (_isVaume == false) {
            return;
        
        }

        transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, _vacumeSpeed * Time.deltaTime);






    }
}
