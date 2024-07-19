using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ExplosionPoolManger : PoolManager<Explosion>
{
    private void Start()
    {
        Debug.Log(_objectPool);
        if(_objectPool == null)
        {
            Debug.Log(_objectPool.ToString()+"ぬるだよ");
        }
        else
        {
            Debug.Log("あるよ");
        }
    }



    /// <summary>
    /// 指定された位置で爆発を開始
    /// </summary>
    /// <param name="startPosition">爆発を開始位置</param>
    public void StartExplosion(Transform startPosition)
    {
        if (_objectPool == null)
        {
            Debug.LogError("_objectPool is not initialized.");
            return;
        }
        var explosion = _objectPool.Get();  
        explosion.Initialize(); // 初期化
        explosion.transform.position = startPosition.position;
    }


}
