using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_AK : MonoBehaviour
{
    //[SerializeField] GameObject particlePrefab; // �p�[�e�B�N���̃v���n�u
    //[SerializeField] float particleLifetime = 2f; // �p�[�e�B�N���̎���

    public float _moveSpeed = 5f; // �ړ����x

    private void Update()
    {

        // �I�u�W�F�N�g��O���Ɉړ�������
        transform.Translate(Vector3.forward * _moveSpeed * Time.deltaTime);
        // �I�u�W�F�N�g���ʂ�߂�����A�p�[�e�B�N���𐶐�����

        //GenerateParticle(transform.position);

        if (Input.GetKeyDown(KeyCode.A))
        {
            
        }
            
    }

    // �I�u�W�F�N�g���ʂ�߂���Ƃ��ɌĂ΂�郁�\�b�h
    public void ObjectPassed()
    {
        
    }

    private void GenerateParticle(Vector3 position)
    {
        // �p�[�e�B�N���𐶐����Ďw�肵���ʒu�ɔz�u
       // GameObject particle = Instantiate(particlePrefab, position, Quaternion.identity);

        // �p�[�e�B�N���̎�����ݒ�
       // Destroy(particle, particleLifetime);
    }
}
