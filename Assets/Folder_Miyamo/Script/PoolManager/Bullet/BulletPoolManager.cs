using UnityEngine;

public class BulletPoolManager : PoolManager<Bullet> {
    /// <summary>
    /// 弾丸発射 playerからfirepostionの向きにそってfirepostionから発射する
    /// </summary>
    /// <param name="playerPosition">playerの位置</param>
    /// <param name="firePosition">弾丸発射の位置</param>
    /// <param name="multiplyValue">弾の速度,掛ける</param>
    public void FireBullet(Transform playerPosition, Transform firePosition, float multiplyValue) {
        // オブジェクトプールから弾を取得
        var bullet = _objectPool.Get();
        bullet.Initialize();

        // 弾の位置と回転を発射位置と同じに設定
        bullet.transform.SetPositionAndRotation(firePosition.position, firePosition.rotation);

        // playerPositionから見たfirePositionの向きに沿って発射
        Vector3 firingDirection = (firePosition.position - playerPosition.position).normalized;

        // 弾に力を加えて発射
        bullet.GetComponent<Rigidbody>().velocity = firingDirection * multiplyValue;
    }
}
