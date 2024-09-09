using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretFiresBullet : MonoBehaviour
{
    public BulletPoolManager _bulletPoolManager;

    [SerializeField, Header("発射位置")]
    private Transform _firePostion;


    [SerializeField, Header("プレイヤー")]
    private Transform _player;

    [SerializeField, Range(0, 1f)]
    private float _bulletCoolTime;

    [SerializeField, Range(0, 10000)]
    private float _bulletSpeed;

    [SerializeField, Range(0, 1)]
    private float _bulletRandom;



    private float _coolTimer;
    private void Update() {

        _coolTimer -= Time.deltaTime;

        _coolTimer = Mathf.Max(0, _coolTimer);
        if (_coolTimer == 0) {

            _bulletPoolManager.FireBullet( _firePostion, _player, _bulletSpeed, _bulletRandom);
            _coolTimer = _bulletCoolTime;
        }





    }
    private void OnEnable() {

        _coolTimer = _bulletCoolTime;

    }
}
