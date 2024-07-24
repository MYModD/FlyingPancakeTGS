using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Explosion : MonoBehaviour, IPooledObject<Explosion>
{
    [SerializeField, Header("�p������")]
    private float _duration; // ���O��__timer����_duration�ɕύX

    public IObjectPool<Explosion> ObjectPool { get; set; }

    /// <summary>
    /// ������
    /// </summary>
    public void Initialize()
    {
        StartCoroutine(DeactivateAfterDuration());
    }

    /// <summary>
    /// �v�[���ɖ߂�����
    /// </summary>
    public void ReturnToPool()
    {
        ObjectPool.Release(this);
    }

    /// <summary>
    /// �w�莞�Ԍo�ߌ�Ƀv�[���ɖ߂�
    /// </summary>
    private IEnumerator DeactivateAfterDuration()
    {
        yield return new WaitForSeconds(_duration);
        ReturnToPool();
    }
}
