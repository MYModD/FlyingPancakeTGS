using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePizzaMissileToPlayer : MonoBehaviour
{
    public PizzaMissile _pizzaMissileToPlayer;
    public Transform _player;

    public Transform[] _firePostion;
    public Transform _pearentPostion;
    public ExplosionPoolManager _explosionPoolManager;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F) && Input.GetKeyDown(KeyCode.J)) {

            int i = Random.Range(0, _firePostion.Length);

            PizzaMissile missile = Instantiate(_pizzaMissileToPlayer,_pearentPostion);
            missile._player = _player;
            missile._explosionPool = _explosionPoolManager;
            missile.gameObject.transform.SetPositionAndRotation(_firePostion[i].position, _firePostion[i].rotation);
        }
    }
}
