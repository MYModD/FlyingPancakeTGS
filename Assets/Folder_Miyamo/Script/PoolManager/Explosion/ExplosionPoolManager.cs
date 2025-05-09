using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ExplosionPoolManager : PoolManager<Explosion>
{
   

    /// <summary>
    /// 指定された位置で爆発を開始
    /// </summary>
    /// <param name="startPosition">爆発を開始位置</param>
    public void StartExplosion(Transform startPosition)
    {
        if (ObjectPool == null)
        {
            Debug.LogError("オブジェクトがアタッチされてないよ");
            return;
        }

        Explosion explosion = ObjectPool.Get();                   // オブジェクトから取得
        explosion.Initialize();                                    // 初期化
        explosion.transform.position = startPosition.position;     // 爆発の位置を引数の位置にする
    }


    public void StartExplosionScale(Transform startPosition,float scale ) {
        if (ObjectPool == null) {
            Debug.LogError("オブジェクトがアタッチされてないよ");
            return;
        }

        Explosion explosion = ObjectPool.Get();                   // オブジェクトから取得
        explosion.Initialize();                                    // 初期化
        explosion.transform.position = startPosition.position;     // 爆発の位置を引数の位置にする
        explosion.transform.localScale = new Vector3(scale, scale, scale);
    
    }

}
