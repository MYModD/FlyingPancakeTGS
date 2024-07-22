using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ExplosionPoolManager : PoolManager<Explosion>
{

    
    /// <summary>
    /// �w�肳�ꂽ�ʒu�Ŕ������J�n
    /// </summary>
    /// <param name="startPosition">�������J�n�ʒu</param>
    public void StartExplosion(Transform startPosition)
    {
        if (_objectPool == null)
        {
            Debug.LogError("_objectPool is not initialized.");
            return;
        }
        Explosion explosion = _objectPool.Get();  
        explosion.Initialize(); // ������
        explosion.transform.position = startPosition.position;
    }


}
