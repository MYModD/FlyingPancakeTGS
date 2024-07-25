using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteEnemyHP : MonoBehaviour {
    // HP
    [SerializeField, Header("HP")]
#pragma warning disable IDE1006 // �����X�^�C��
    private int _HP = 3;
#pragma warning restore IDE1006 // �����X�^�C��

    // ����
    [SerializeField, Header("����")]
    private ExplosionPoolManager _explosionPoolManager;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    
    /// <summary>
    /// HP�����炵�A0�ɂȂ����甚�����N�����Ď��g���A�N�e�B�u�ɂ��郁�\�b�h
    /// </summary>
    public void DecreaseHP() {
        _HP--;
        if (_HP == 0) {
            _explosionPoolManager.StartExplosion(this.transform);
            gameObject.SetActive(false);
        }
    }
}
