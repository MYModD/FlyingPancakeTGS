using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resultet : MonoBehaviour {
    [SerializeField] private CanvasManager _canvas;

    [Header("1stStage")]
    [SerializeField, Header("1stPlayerのタグ"), Tag] private string _tag1st;
    [SerializeField, Header("1stのプレイヤー")] private GameObject _player1st;
    [SerializeField, Header("1stのステージ")] private GameObject _game1st;
    private bool _isGame1st = true;

    [Header("2stStage")]
    [SerializeField, Header("2stPlayerのタグ"), Tag] private string _tag2st;
    [SerializeField, Header("2stのプレイヤー")] private GameObject _player2st;
    [SerializeField, Header("2stのステージ")] private GameObject _game2st;
    private bool _isGame2st = true;

    [Header("3stStage")]
    [SerializeField, Header("3stPlayerのタグ"), Tag] private string _tag3st;
    [SerializeField, Header("3stのプレイヤー")] private GameObject _player3st;
    [SerializeField, Header("3stのステージ")] private GameObject _game3st;
    private bool _isGame3st = true;

    [Header("4stStage")]
    [SerializeField, Header("4stPlayerのタグ"), Tag] private string _tag4st;
    [SerializeField, Header("4stのプレイヤー")] private GameObject _player4st;
    [SerializeField, Header("4stのステージ")] private GameObject _game4st;
    private bool _isGame4st = true;

    [Header("5stStage")]
    [SerializeField, Header("5stPlayerのタグ"), Tag] private string _tag5st;
    [SerializeField, Header("5stのプレイヤー")] private GameObject _player5st;
    [SerializeField, Header("5stのステージ")] private GameObject _game5st;
    private bool _isGame5st = true;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
    private void GameObjTrueFalse(GameObject[] trueObjects, GameObject[] falseObjects) {
        foreach (GameObject obj in falseObjects) {
            obj.SetActive(false);
        }
        foreach (GameObject obj in trueObjects) {
            obj.SetActive(true);
        }

    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(_tag1st) && _isGame1st) {

            _isGame1st = false;
        }
        if (other.CompareTag(_tag2st) && _isGame2st) {

            _isGame2st = false;
        }
        if (other.CompareTag(_tag3st) && _isGame3st) {

            _isGame3st = false;
        }
        if (other.CompareTag(_tag4st) && _isGame4st) {

            _isGame4st = false;
        }
        if (other.CompareTag(_tag5st) && _isGame5st) {

            _isGame5st = false;
        }

    }
}
