using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour, IPooledObject<Bullet>
{
    public float timedegerreeee;
    public IObjectPool<Bullet> ObjectPool { get; set; }




    public void Initialize()
    {
        
    }

    public void Deactivate()
    {
        // 弾丸の非アクティブ化コード
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 衝突時の処理
    }

    private void OnEnable()
    {
        Initialize();
        StartCoroutine(nameof(js));
    }
    IEnumerator js()
    {
        yield return new WaitForSeconds(5f);
        ObjectPool.Release(this);
    }
}
