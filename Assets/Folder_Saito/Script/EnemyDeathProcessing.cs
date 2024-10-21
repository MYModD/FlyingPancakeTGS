using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathProcessing : MonoBehaviour {
    //倒した敵計算スクリプト
    [SerializeField] private CountTheNumberOfDefeats _countTheNumberOfDefeats;
    //ミサイルのタグ
    [SerializeField, Tag] private string _missileTag;
    //ダメージアニメーションさせるスクリプト
    [SerializeField] private Planeee _planeee;
    //重力
    [SerializeField] private Rigidbody _rigidbody;
    //ぶるぶる
    private ControllerBuruBuru _controller;
    //死んでるか
    private bool _isDeath = true;


    private void OnTriggerEnter(Collider other) {
        if (_controller == null) {
            _controller = ControllerBuruBuru.Instance;
        }
        if (other.gameObject.CompareTag(_missileTag) && _isDeath) {
            //死んでいる状態に
            _planeee.hp = -100;
            //死んだことを報告
            _countTheNumberOfDefeats.AdditionOfNumberOfDefeats();
            //ぶるぶる
            _controller.StartVibration();
            //重力起動
            _rigidbody.useGravity = true;
            //親子の縁を切る
            this.gameObject.transform.parent = null;
        }
    }
}