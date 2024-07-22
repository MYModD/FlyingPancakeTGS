using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// �ėp�v�[���}�l�[�W���[�N���X �I�u�W�F�N�g�v�[�����Ǘ�
/// </summary>
/// <typeparam name="T">�v�[�������I�u�W�F�N�g�̌^�BMonoBehaviour���p�����AIPooledObject�C���^�[�t�F�[�X����������K�v������</typeparam>
public abstract class PoolManager<T> : MonoBehaviour where T : MonoBehaviour, IPooledObject<T>
{
    
    [Header("�v�[�������I�u�W�F�N�g")]
    [SerializeField] private T _pooledPrefab; 

    protected IObjectPool<T> _objectPool; // �I�u�W�F�N�g�v�[���̊Ǘ��C���X�^���X

    [Header("�v�[���̏���������")]
    [SerializeField] private int _defaultCapacity = 32; 

    [Header("�v�[���̍ő�l")]
    [SerializeField] private int _maxSize = 100; 

    private const bool COLLECTION_CHECK = true; // �R���N�V�����`�F�b�N�̃t���O�B���ɈӖ����Ȃ��̂�true

  
    
    
    
    /// <summary>
    /// �v�[���}�l�[�W���[�̏����� _defaultCapacity���ŏ��ɐ������� �d��������R���[�`���������
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


        // �������I�u�W�F�N�g�𐶐��A�v�[���ɒǉ�
        for (int i = 0; i < _defaultCapacity; i++)
        {
            T game = Create();
            _objectPool.Release(game);
        }
    }

    /// <summary>
    /// �V�����C���X�^���X�𐶐����郁�\�b�h�B
    /// </summary>   
    protected virtual T Create()
    {
        var instance = Instantiate(_pooledPrefab, transform.position, Quaternion.identity, transform);
        instance.ObjectPool = _objectPool;
        return instance;
    }


    /// <summary>
    /// �I�u�W�F�N�g���v�[���ɖ߂��ۂɌĂяo����郁�\�b�h _objectPool.Release()����Ƃ��ɌĂ΂��
    /// </summary>
    protected virtual void OnReleaseToPool(T pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
    }

    /// <summary>
    /// �v�[������I�u�W�F�N�g���擾����ۂɌĂяo����郁�\�b�h _objectPool.Get()����Ƃ��ɌĂ΂��
    /// </summary>
    protected virtual void OnGetFromPool(T pooledObject)
    {
        pooledObject.gameObject.SetActive(true);
    }



    /// <summary>
    /// �v�[�����ꂽ�I�u�W�F�N�g��j������ۂɌĂяo����郁�\�b�h�B
    /// </summary>
    protected virtual void OnDestroyPooledObject(T pooledObject)
    {
        pooledObject.ReturnToPool();
        Destroy(pooledObject.gameObject);
    }
}
