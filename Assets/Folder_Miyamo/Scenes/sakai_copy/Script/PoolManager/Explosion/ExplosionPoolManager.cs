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
            Debug.LogError("�I�u�W�F�N�g���A�^�b�`����ĂȂ���");
            return;
        }

        Explosion explosion = _objectPool.Get();                   // �I�u�W�F�N�g����擾
        explosion.Initialize();                                    // ������
        explosion.transform.position = startPosition.position;     // �����̈ʒu�������̈ʒu�ɂ���
    }


}
