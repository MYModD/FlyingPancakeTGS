using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PizzaCoinUI : MonoBehaviour, IPooledObject<PizzaCoinUI> {

    [Header("UI�ɐڑ�����X�N���v�g")]
    public PizzaCoinUICounter _pizzaCoinUICounter;

    [Header("UI�̈ʒu")]
    public RectTransform _targetTransform; // UI�̃^�[�Q�b�g�ʒu

    [Header("�ړ��X�s�[�h")]
    public float _attractionSpeed = 1000f;



    [Tag]
    public string _pizzaCoinUI;
    public IObjectPool<PizzaCoinUI> ObjectPool {
        get; set;
    }

    public RectTransform _rectTransform;



    public void Hoge() {

        Debug.Log(ObjectPool);
    }
    private void Awake() {
        _rectTransform = GetComponent<RectTransform>();
      
    }
    public void Initialize() {
        if (_rectTransform == null) {
            _rectTransform = GetComponent<RectTransform>();
        }

        if (_targetTransform == null) {
            Debug.LogError("TargetTransform���ݒ肳��Ă��܂���");
        }


    }

    public void ReturnToPool() {
        ObjectPool.Release(this);
    }

    // Update is called once per frame
    void FixedUpdate() {


        if (_rectTransform == null) {
            Debug.LogError("�����ĂȂ���I�I");
            return;
        }

        // UI�̈ʒu���^�[�Q�b�g�Ɍ������Ĉړ�
        Vector2 direction = (_targetTransform.position - _rectTransform.position).normalized;
        _rectTransform.position += (Vector3)direction * _attractionSpeed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag(_pizzaCoinUI)) {
            _pizzaCoinUICounter.TouchMyField();
            ReturnToPool();
        }
    }
}
