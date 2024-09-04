using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// 汎用プールマネージャークラス オブジェクトプールを管理
/// </summary>
/// <typeparam name="T">プールされるオブジェクトの型。MonoBehaviourを継承し、IPooledObjectインターフェースを実装する必要がある</typeparam>
public abstract class PoolManager<T> : MonoBehaviour where T : MonoBehaviour, IPooledObject<T>
{
    
    [Header("プールされるオブジェクト")]
    [SerializeField] private T _pooledPrefab; 

    protected IObjectPool<T> _objectPool; // オブジェクトプールの管理インスタンス

    [Header("プールの初期生成数")]
    [SerializeField] private int _defaultCapacity = 32; 

    [Header("プールの最大値")]
    [SerializeField] private int _maxSize = 100; 

    private const bool COLLECTION_CHECK = true; // コレクションチェックのフラグ。特に意味がないのでtrue

  
    
    
    
    /// <summary>
    /// プールマネージャーの初期化 _defaultCapacity分最初に生成する 重すぎたらコルーチンをいれる
    /// </summary>

    private void Awake()
    {
        Initialize();
    }
    public virtual void Initialize()
    {
       
        _objectPool = new ObjectPool<T>(
            Create,
            OnGetFromPool,
            OnReleaseToPool,
            OnDestroyPooledObject,
            COLLECTION_CHECK,
            _defaultCapacity,
            _maxSize
        );


        // 初期分オブジェクトを生成、プールに追加
        for (int i = 0; i < _defaultCapacity; i++)
        {
            T game = Create();
            _objectPool.Release(game);
        }
    }

    /// <summary>
    /// 新しいインスタンスを生成するメソッド。
    /// </summary>   
    protected virtual T Create()
    {
        var instance = Instantiate(_pooledPrefab, transform.position, Quaternion.identity, transform);
        instance.ObjectPool = _objectPool;
        return instance;
    }


    /// <summary>
    /// オブジェクトをプールに戻す際に呼び出されるメソッド _objectPool.Release()するときに呼ばれる
    /// </summary>
    protected virtual void OnReleaseToPool(T pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
    }

    /// <summary>
    /// プールからオブジェクトを取得する際に呼び出されるメソッド _objectPool.Get()するときに呼ばれる
    /// </summary>
    protected virtual void OnGetFromPool(T pooledObject)
    {
        pooledObject.gameObject.SetActive(true);
    }



    /// <summary>
    /// プールされたオブジェクトを破棄する際に呼び出されるメソッド。
    /// </summary>
    protected virtual void OnDestroyPooledObject(T pooledObject)
    {
        pooledObject.ReturnToPool();
        Destroy(pooledObject.gameObject);
    }
}
