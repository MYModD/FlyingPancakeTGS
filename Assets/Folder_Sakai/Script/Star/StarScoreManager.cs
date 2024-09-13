using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StarScoreManager : MonoBehaviour {
    #region 変数定義
    // シングルトンインスタンスを保持するための静的フィールド
    private static StarScoreManager _instance;

    // インスタンスへのアクセスを提供するプロパティ
    public static StarScoreManager Instance {
        get {
            // インスタンスがnullの場合、シーン内から探す
            if (_instance == null) {
                _instance = FindObjectOfType<StarScoreManager>();
                if (_instance == null) {
                    Debug.LogError("StarScoreManagerのインスタンスがシーンに存在しません");
                }
            }
            return _instance;
        }
    }

    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private TextMeshProUGUI _textTitle;
    [SerializeField] private TextMeshProUGUI _textScore;
    [SerializeField] private TextMeshProUGUI _timeText1;
    [SerializeField] private TextMeshProUGUI _timeText2;
    [SerializeField] private AudienceGaugeManager _gauge;
    [SerializeField, Header("星のUI入れてね")] private GameObject[] _starUI;
    [SerializeField] private AudioClip _clipBIG;
    [SerializeField] private AudioClip _clipLITTLE;

    [SerializeField]private float _weitSecond = 0.1f;
    private int _score;
    private int _maxStarUSA = 30;

    [SerializeField] private AudioSource _se;

    #endregion

    #region メソッド
    private void Awake() {
        // インスタンスが既に存在している場合はこのオブジェクトを破棄
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        }
    }
    private void Update() {
        //_textTitle.text = "Star Count";
        //_textScore.text = _score.ToString();
        //_scoreManager.InputGetStarScore(_score, _maxStarUSA);
        //_gauge.SetScoreValue(_score, _maxStarUSA, "Star Count");
    }

    public void ScoreAddition(int score) {
        if (score == 10000) {
            score = Random.Range(-5, 6);
        }
        _score += score;
        StartCoroutine(StarUP(score));  // コルーチンを開始
        _scoreManager.InputGetStarScore(_score, _maxStarUSA);
       if (score > 0) {
            _se.PlayOneShot(_clipBIG);
        }else if(score<0){
            _se.PlayOneShot(_clipLITTLE);
        }
           
        _textScore.color = Color.white;
        _textTitle.color = Color.white;
        _timeText1.color = Color.white;
        _timeText2.color = Color.white;
    }

    private IEnumerator StarUP(int starCount) {
        for (int i = 0; i < starCount; i++) {
            int ramdam = Random.Range(0, _starUI.Length);
            if (_starUI[ramdam].activeSelf) {
                i--;
            } else {
                yield return new WaitForSeconds(_weitSecond);  // 0.1秒待つ
                _starUI[ramdam].SetActive(true);
            }
        }
    }
    #endregion
}
