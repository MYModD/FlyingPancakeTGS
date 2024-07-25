using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteEnemyHP : MonoBehaviour {
    // HP
    [SerializeField, Header("HP")]
#pragma warning disable IDE1006 // 命名スタイル
    private int _HP = 3;
#pragma warning restore IDE1006 // 命名スタイル

    // 爆発
    [SerializeField, Header("爆発")]
    private ExplosionPoolManager _explosionPoolManager;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    
    /// <summary>
    /// HPを減らし、0になったら爆発を起こして自身を非アクティブにするメソッド
    /// </summary>
    public void DecreaseHP() {
        _HP--;
        if (_HP == 0) {
            _explosionPoolManager.StartExplosion(this.transform);
            gameObject.SetActive(false);
        }
    }
}
