using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resultet : MonoBehaviour {
    [SerializeField] private CanvasManager _canvas;

    [SerializeField,Header("CutInのアニメーター")] private Animator _animator;
    [SerializeField, Header("1stPlayerのタグ"), Tag] private string _playerTag;
    [SerializeField]
    private AudienceGaugeManager _miniScore;
    [SerializeField] private AircraftAnimation _aircraftAnimation1;
    [SerializeField] private AircraftAnimation _aircraftAnimation2;
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        //ショートカットデバック用
        if (Input.GetKeyDown(KeyCode.F) && Input.GetKey(KeyCode.P)) {
            _animator.Play("CutIN");
        }
    }
    private void OnTriggerEnter(Collider other) {
        //プレイヤーがぶつかったら
        if (other.CompareTag(_playerTag)) {
            //アニメーション開始
            _animator.Play("CutIN");
            //テキスト非表示
            _miniScore.TextTrue(false);
            //なんだこれ👇
            _aircraftAnimation1.Variable();
            _aircraftAnimation2.Variable();
        }
    }
}
