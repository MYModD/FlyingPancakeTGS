// ---------------------------------------------------------
// CountDownUI.cs
//
// 作成日:
// 作成者:
// ---------------------------------------------------------
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class CountDownUI : MonoBehaviour {
    #region 変数
    [Header("CanvasManager")]
    [SerializeField] private CanvasManager _canvasManager = default;
    private bool _isActive = true;
    [Header("ゲームスタート画面から何秒カウントダウンするか")]
    [SerializeField] private int _startTime;

    [Header("カメラの設定")]
    [SerializeField] private Camera[] _cameras;
    [Header("SE鳴らすオブジェクト")]
    [SerializeField] private AudioSource _audioSource = default;
    [Header("サウンド")]
    [SerializeField] private AudioClip[] _audioClip = default;
    [Header("0が下、１が上")]
    [SerializeField] private Image[] _images;
    [SerializeField] private GameObject[] _gameObject;
    private int _count = 0;
    #endregion

    #region プロパティ
    #endregion

    #region メソッド

    public void PublicStart() {
        StartCoroutine(CountDownCoroutine());
    }

    void StartCountDown() {
        if (_isActive) {
            //ゲームプレイに
            _canvasManager.OPToGamePlay();
            foreach (Camera camera in _cameras) {
                //カウントダウン用のカメラワーク
                camera.gameObject.SetActive(false);
                _audioSource.PlayOneShot(_audioClip[_count]);
                _audioSource.PlayOneShot(_audioClip[_count]);
                _audioSource.PlayOneShot(_audioClip[_count]);
            }
            foreach (GameObject obj in _gameObject) {
                //カウントダウンに使う配列の中身無効化
                obj.gameObject.SetActive(false);
            }
            _images[2].enabled = false;
            _images[3].enabled = false;
            //２度はさせない
            _isActive = false;
            //加算
            _count++;
        }
    }

    IEnumerator CountDownCoroutine() {
        for (int i = 0; i < _cameras.Length; i++) {
            yield return new WaitForSeconds(_startTime / _startTime); // 1秒待機
            _cameras[i].gameObject.SetActive(true); // カメラを有効にする
            SetupCameraViewport(_cameras[i], i); // カメラのビューポートを設定
            _audioSource.PlayOneShot(_audioClip[_count]);
            _count++;
        }
        yield return new WaitForSeconds(_startTime / _startTime); // 1秒待機
        StartCountDown();
    }

    void SetupCameraViewport(Camera camera, int index) {
        //カメラのそれぞれの設定
        switch (index) {
            case 0:
                _images[1].enabled = false;
                camera.rect = new Rect(0, 0.5f, 1, 0.5f); // 画面の上半分
                break;
            case 1:
                _images[0].enabled = false;
                camera.rect = new Rect(0, 0, 1, 0.5f); // 画面の下半分
                break;
            case 2:
                _images[2].enabled = true;
                _images[3].enabled = true;
                camera.rect = new Rect(0f, 0.25f, 1f, 0.5f); // 画面の中央
                break;
        }
    }
    #endregion
}