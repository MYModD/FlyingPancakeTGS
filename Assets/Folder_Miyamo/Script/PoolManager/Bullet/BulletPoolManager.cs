using UnityEngine;

public class BulletPoolManager :PoolManager<Bullet>
{


    public void FireBullet(Transform firePosition, float multiplyValue) {
        // �I�u�W�F�N�g�v�[������e���擾
        var bullet = _objectPool.Get();
        bullet.Initialize();

        // �e�̈ʒu�Ɖ�]�𔭎ˈʒu�Ɠ����ɐݒ�
        bullet.transform.SetPositionAndRotation(firePosition.position, firePosition.rotation);

        // �e�ɗ͂������Ĕ���
        bullet.GetComponent<Rigidbody>().velocity = firePosition.forward * multiplyValue;
    }
    

}
