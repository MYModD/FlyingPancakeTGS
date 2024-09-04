using UnityEngine;

public class BulletPoolManager : PoolManager<Bullet> {
    /// <summary>
    /// �e�۔��� player����firepostion�̌����ɂ�����firepostion���甭�˂���
    /// </summary>
    /// <param name="playerPosition">player�̈ʒu</param>
    /// <param name="firePosition">�e�۔��˂̈ʒu</param>
    /// <param name="multiplyValue">�e�̑��x,�|����</param>




    public void FireBullet(Transform firePosition, float multiplyValue) {

        // �I�u�W�F�N�g�v�[������e���擾�A������
        Bullet bullet = _objectPool.Get();
        bullet.Initialize();


        // �e�̈ʒu�Ɖ�]�𔭎ˈʒu�Ɠ����ɐݒ� �������̂ق����y���炵��
        bullet.transform.SetPositionAndRotation(firePosition.position, firePosition.rotation);

        // �e�ɗ͂������Ĕ���
        bullet.GetComponent<Rigidbody>().velocity = transform.forward *  multiplyValue;

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
