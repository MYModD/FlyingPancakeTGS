using System.Collections.Generic;
using UnityEngine;

public class BulletPoolManager : PoolManager<Bullet> {



    private Dictionary<Bullet, Rigidbody> _bulletRigidbodies = new();





    public void FireBullet(Transform turretFirePosition, Transform toPlayer, float multiplyValue, float spreadAngle) {
        // オブジェクトプールから弾を取得、初期化
        Bullet bullet = _objectPool.Get();
        bullet.Initialize();

        // 弾の位置と回転を発射位置と同じに設定
        bullet.transform.SetPositionAndRotation(turretFirePosition.position, turretFirePosition.rotation);


        // 辞書からRigidbodyを取得
        if (!_bulletRigidbodies.TryGetValue(bullet, out Rigidbody rigidbody)) {
            // 辞書に存在しない場合は、Rigidbodyを取得して登録
            rigidbody = bullet.GetComponent<Rigidbody>();
            _bulletRigidbodies.Add(bullet, rigidbody);
        }

        // ターゲットの方向ベクトルを計算
        Vector3 direction = (toPlayer.position - turretFirePosition.position).normalized;

        // ランダムな回転角を生成
        float randomAngle = Random.Range(-spreadAngle, spreadAngle);
        Quaternion rotation = Quaternion.Euler(0, randomAngle, 0);

        // 発射方向のベクトルを回転させる
        Vector3 newDirection = rotation * direction;

        // 弾に力を加えて発射
        rigidbody.velocity = newDirection * multiplyValue;

    }









    /*public void FireBullet(Transform playerPosition, Transform firePosition, float multiplyValue) {

        // オブジェクトプールから弾を取得、初期化
        Bullet bullet = _objectPool.Get();
        bullet.Initialize();             


        // 弾の位置と回転を発射位置と同じに設定 こっちのほうが軽いらしい
        bullet.transform.SetPositionAndRotation(firePosition.position, firePosition.rotation);


        // playerPositionから見たfirePositionの向きに沿って発射
        Vector3 firingDirection = (firePosition.position - playerPosition.position).normalized;


        // 弾に力を加えて発射
        bullet.GetComponent<Rigidbody>().velocity = firingDirection * multiplyValue;
    }*/
}
