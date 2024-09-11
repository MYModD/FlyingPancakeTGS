using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotator : MonoBehaviour {
    #region 変数

    [SerializeField, Header("回転速度")]
    private float _rotationSpeed = 100f; // 回転速度

    [SerializeField, Header("車輪のタグ"),Tag]
    private string _wheelTag; // 車輪オブジェクトのタグ
    [SerializeField, Header("モンスタートラック共")]
    private GameObject[] _monstarTrack;

    [SerializeField]
    private List<Transform> _wheels = new List<Transform>(); // 車輪を格納するリスト

    #endregion

    private void Start() {
        FindWheelsByTag();
    }

    private void Update() {
        RotateWheels();
    }

    /// <summary>
    /// タグに基づいて車輪を取得するメソッド
    /// </summary>
    private void FindWheelsByTag() {
        GameObject[] wheelObjects = GameObject.FindGameObjectsWithTag(_wheelTag);
        foreach (GameObject wheelObject in wheelObjects) {
            _wheels.Add(wheelObject.transform); // 車輪のTransformをリストに追加
        }
        foreach (GameObject mT in _monstarTrack) {
            mT.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 車輪を回転させるメソッド
    /// </summary>
    private void RotateWheels() {
        foreach (Transform wheel in _wheels) {
            wheel.Rotate(Vector3.right * _rotationSpeed * Time.deltaTime);
        }
    }
}
