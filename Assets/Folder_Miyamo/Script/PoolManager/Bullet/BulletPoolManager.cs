using System.Collections.Generic;
using UnityEngine;

public class BulletPoolManager : PoolManager<Bullet> {



    private Dictionary<Bullet, Rigidbody> _bulletRigidbodies = new();





    public void FireBullet(Transform turretFirePosition, Transform toPlayer, float multiplyValue, float spreadAngle) {
        // �I�u�W�F�N�g�v�[������e���擾�A������
        Bullet bullet = _objectPool.Get();
        bullet.Initialize();

        // �e�̈ʒu�Ɖ�]�𔭎ˈʒu�Ɠ����ɐݒ�
        bullet.transform.SetPositionAndRotation(turretFirePosition.position, turretFirePosition.rotation);


        // ��������Rigidbody���擾
        if (!_bulletRigidbodies.TryGetValue(bullet, out Rigidbody rigidbody)) {
            // �����ɑ��݂��Ȃ��ꍇ�́ARigidbody���擾���ēo�^
            rigidbody = bullet.GetComponent<Rigidbody>();
            _bulletRigidbodies.Add(bullet, rigidbody);
        }

        // �^�[�Q�b�g�̕����x�N�g�����v�Z
        Vector3 direction = (toPlayer.position - turretFirePosition.position).normalized;

        // �����_���ȉ�]�p�𐶐�
        float randomAngle = Random.Range(-spreadAngle, spreadAngle);
        Quaternion rotation = Quaternion.Euler(0, randomAngle, 0);

        // ���˕����̃x�N�g������]������
        Vector3 newDirection = rotation * direction;

        // �e�ɗ͂������Ĕ���
        rigidbody.velocity = newDirection * multiplyValue;

    }









    /*public void FireBullet(Transform playerPosition, Transform firePosition, float multiplyValue) {

        // �I�u�W�F�N�g�v�[������e���擾�A������
        Bullet bullet = _objectPool.Get();
        bullet.Initialize();             


        // �e�̈ʒu�Ɖ�]�𔭎ˈʒu�Ɠ����ɐݒ� �������̂ق����y���炵��
        bullet.transform.SetPositionAndRotation(firePosition.position, firePosition.rotation);


        // playerPosition���猩��firePosition�̌����ɉ����Ĕ���
        Vector3 firingDirection = (firePosition.position - playerPosition.position).normalized;


        // �e�ɗ͂������Ĕ���
        bullet.GetComponent<Rigidbody>().velocity = firingDirection * multiplyValue;
    }*/
}
