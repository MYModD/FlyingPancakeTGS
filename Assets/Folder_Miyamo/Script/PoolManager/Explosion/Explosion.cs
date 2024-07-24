using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Explosion : MonoBehaviour, IPooledObject<Explosion>
{
    [SerializeField, Header("継続時間")]
    private float _duration; // 名前を__timerから_durationに変更

    public IObjectPool<Explosion> ObjectPool { get; set; }

    /// <summary>
    /// 初期化
    /// </summary>
    public void Initialize()
    {
        StartCoroutine(DeactivateAfterDuration());
    }

    /// <summary>
    /// プールに戻す処理
    /// </summary>
    public void ReturnToPool()
    {
        ObjectPool.Release(this);
    }

    /// <summary>
    /// 指定時間経過後にプールに戻す
    /// </summary>
    private IEnumerator DeactivateAfterDuration()
    {
        yield return new WaitForSeconds(_duration);
        ReturnToPool();
    }
}
