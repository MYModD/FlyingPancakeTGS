using UnityEngine;

public class BulletPoolManager :PoolManager<Bullet>
{


    public void FireBullet(Transform firePosition, float multiplyValue) {
        // オブジェクトプールから弾を取得
        var bullet = _objectPool.Get();
        bullet.Initialize();

        // 弾の位置と回転を発射位置と同じに設定
        bullet.transform.SetPositionAndRotation(firePosition.position, firePosition.rotation);

        // 弾に力を加えて発射
        bullet.GetComponent<Rigidbody>().velocity = firePosition.forward * multiplyValue;
    }
    

}
