using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaCoinInstance : MonoBehaviour {
    // 配置位置の配列
    public Transform _instantiateLeftPosition;
    public Transform _instantiateRightPosition;

    // コインオブジェクトの配列
    [Header("短いやつと長いやつある")]
    public GameObject[] _leftCoin;

    [Header("短いやつしかないよ")]
    public GameObject _rightCoin;

    // コイン生成間隔
    public float _durationTime;

    [Header("ラミエルから見て左が出る確率  だいたい70")]
    public float _leftProbability = 70;

    // タイマー
    private float _calucurate;

    private void OnEnable() {
        _calucurate = _durationTime;
    }

    // Update is called once per frame
    void Update() {
        _calucurate -= Time.deltaTime; // 経過時間を減らす
        if (_calucurate <= 0) {


            int i = Random.Range(0, 101);
            Debug.Log($"ランダム値 : {i}");
            // 7割の確率で左（ラミエルから見て)コインが生成される
            if (i < _leftProbability) {

                //左の場合
                bool random = Random.Range(0, 2) == 0;
                if (random) {
                    GameObject obj = Instantiate(_leftCoin[0]);
                    obj.transform.position = _instantiateLeftPosition.position;
                } else {
                    GameObject obj = Instantiate(_leftCoin[1]);
                    obj.transform.position = _instantiateLeftPosition.position;
                }




            } else {


                //右の場合
                GameObject obj = Instantiate(_rightCoin);
                obj.transform.position = _instantiateRightPosition.position;
            }


            // タイマーをリセット
            _calucurate = _durationTime;
        }

        // 任意のキー入力で処理を行いたい場合
        if (Input.GetKey(KeyCode.F) && Input.GetKeyDown(KeyCode.J)) {
            // ここに処理を追加
            Debug.Log("FキーとJキーが同時に押されました");
        }
    }
}
