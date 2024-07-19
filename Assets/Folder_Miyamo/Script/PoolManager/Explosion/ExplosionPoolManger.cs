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
            Debug.Log(_objectPool.ToString()+"�ʂ邾��");
        }
        else
        {
            Debug.Log("�����");
        }
    }



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
        var explosion = _objectPool.Get();  
        explosion.Initialize(); // ������
        explosion.transform.position = startPosition.position;
    }


}
