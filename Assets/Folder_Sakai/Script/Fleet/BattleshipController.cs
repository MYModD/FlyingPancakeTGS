using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleshipController : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    public void ChageSpeed(float chageSpeed) {

        _speed = chageSpeed;
    }
}
