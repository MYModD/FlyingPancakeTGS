using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePizzaMissileToPlayer : MonoBehaviour
{
    public PizzaMissile _pizzaMissileToPlayer;
    public Transform _player;

    public Transform _firePostion;
    public ExplosionPoolManager _explosionPoolManager;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F) && Input.GetKeyDown(KeyCode.J)) {

            PizzaMissile missile = Instantiate(_pizzaMissileToPlayer);
            missile._player = _player;
            missile._explosionPool = _explosionPoolManager;
            missile.gameObject.transform.SetPositionAndRotation(_firePostion.position, _firePostion.rotation);
        }
    }
}
