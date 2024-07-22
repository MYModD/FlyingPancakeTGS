using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove_SD : MonoBehaviour
{
    [Header("プレイヤー格納場所")]
    [SerializeField] private GameObject _player = default;

    [Header("弾の速さ")]
    [SerializeField] private float _bulletShot = default;

    [Header("弾の消失時間")]
    [SerializeField] private float _bulletfalseTime = default;

    [Header("発射するオブジェクト")]
    [SerializeField]
    private GameObject _firingObject = default;

    private string _firingObjTag = default; //発射するオブジェクトのタグを格納

    private bool _lostBulletSw = true;//1回だけ呼び出すため
    void Start()
    {
        this.transform.position = _player.transform.position;//初期発射位置
        _firingObjTag = _firingObject.tag;
    }

    void Update()
    {
        Shotmove();
    }
    /// <summary>
    /// 弾の発射の速さ
    /// </summary>
    void Shotmove()
    {
        if (_firingObjTag == "Player") {

            transform.Translate(Vector3.back * _bulletShot * Time.deltaTime);//親オブジェクトがいるから偏差できます
            if (_lostBulletSw) {
                StartCoroutine("Activefalsecooroutine");//弾を使いまわすから呼び出し
                _lostBulletSw = false;
            }

        } else if (_firingObjTag == "Enemy") {

            transform.Translate(Vector3.forward * _bulletShot * Time.deltaTime);//親オブジェクトがいるから偏差できます
            if (_lostBulletSw) {
                StartCoroutine("Activefalsecooroutine");//弾を使いまわすから呼び出し
                _lostBulletSw = false;
            }
        }
       
    }
    /// <summary>
    /// 弾の消失
    /// </summary>
    IEnumerator Activefalsecooroutine()
    {
        yield return new WaitForSeconds(_bulletfalseTime);

        _lostBulletSw=true;
        this.gameObject.SetActive(false);
    }

}
