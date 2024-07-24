using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove_SD : MonoBehaviour
{
    [Header("�v���C���[�i�[�ꏊ")]
    [SerializeField] private GameObject _player = default;

    [Header("�e�̑���")]
    [SerializeField] private float _bulletShot = default;

    [Header("�e�̏�������")]
    [SerializeField] private float _bulletfalseTime = default;

    [Header("���˂���I�u�W�F�N�g")]
    [SerializeField]
    private GameObject _firingObject = default;

    private string _firingObjTag = default; //���˂���I�u�W�F�N�g�̃^�O���i�[

    private bool _lostBulletSw = true;//1�񂾂��Ăяo������
    void Start()
    {
        this.transform.position = _player.transform.position;//�������ˈʒu
        _firingObjTag = _firingObject.tag;
    }

    void Update()
    {
        Shotmove();
    }
    /// <summary>
    /// �e�̔��˂̑���
    /// </summary>
    void Shotmove()
    {
        if (_firingObjTag == "Player") {

            transform.Translate(Vector3.back * _bulletShot * Time.deltaTime);//�e�I�u�W�F�N�g�����邩��΍��ł��܂�
            if (_lostBulletSw) {
                StartCoroutine("Activefalsecooroutine");//�e���g���܂킷����Ăяo��
                _lostBulletSw = false;
            }

        } else if (_firingObjTag == "Enemy") {

            transform.Translate(Vector3.forward * _bulletShot * Time.deltaTime);//�e�I�u�W�F�N�g�����邩��΍��ł��܂�
            if (_lostBulletSw) {
                StartCoroutine("Activefalsecooroutine");//�e���g���܂킷����Ăяo��
                _lostBulletSw = false;
            }
        }
       
    }
    /// <summary>
    /// �e�̏���
    /// </summary>
    IEnumerator Activefalsecooroutine()
    {
        yield return new WaitForSeconds(_bulletfalseTime);

        _lostBulletSw=true;
        this.gameObject.SetActive(false);
    }

}
