using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaCoinInstance : MonoBehaviour {
    // 配置位置の配列
    public Transform[] _instantiatePosition;

    // コインオブジェクトの配列
    public GameObject[] _coin;

    // コイン生成間隔
    public float _duration;

    // タイマー
    private float _calucurate;

    private void OnEnable() {
        _calucurate = _duration;
    }

    // Update is called once per frame
    void Update() {
        _calucurate -= Time.deltaTime; // 経過時間を減らす
        if (_calucurate <= 0) {
            // 配列のサイズに応じてランダムな位置を決定
            int i = Random.Range(0, _instantiatePosition.Length);
            Debug.Log($"配列番号は {i}");

            // コインの種類をランダムに決定
            int j = Random.Range(0, _coin.Length);
            GameObject obj = Instantiate(_coin[j]);
            obj.transform.position = _instantiatePosition[i].position;

            // タイマーをリセット
            _calucurate = _duration;
        }

        // 任意のキー入力で処理を行いたい場合
        if (Input.GetKey(KeyCode.F) && Input.GetKeyDown(KeyCode.J)) {
            // ここに処理を追加
            Debug.Log("FキーとJキーが同時に押されました");
        }
    }
}
