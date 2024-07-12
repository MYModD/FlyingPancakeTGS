// ---------------------------------------------------------
// CanvasManager.cs
//
// 作成日:
// 作成者:
// ---------------------------------------------------------
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CanvasManager : MonoBehaviour
{
    #region 変数
    [Header("タイトルのボタン")]
    [SerializeField, Header("スタートボタン")] private Button _titleStart;
    [SerializeField, Header("ゲーム終了ボタン")] private Button _titleGameEnd;
    [SerializeField, Header("設定ボタン")] private Button _titleSetting;

    [SerializeField] private GameObject[] _titleObj;
    [SerializeField] private GameObject[] _menuObj;
    [SerializeField] private GameObject[] _gamePlayObj;
    [SerializeField] private GameObject[] _resultObj;

    #endregion
    #region プロパティ
    #endregion
    #region メソッド
    /// <summary>
    /// 初期化処理 使わないなら消す
    /// </summary>
    void Awake()
    {
      
    }
    /// <summary>
    /// 更新前処理
    /// </summary>
    void Start()
    {

    }
    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {

    }
    private void OnClickSw()
    {

    }
    private void StartToGamePlay()
    {
        foreach (GameObject obj in _titleObj)
        {
           obj.SetActive(false);
        }
        foreach (GameObject obj in _gamePlayObj)
        {
            obj.SetActive(true);
        }
    }
    private void GameFinish()
    {
        foreach (GameObject obj in _gamePlayObj)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in _resultObj)
        {
            obj.SetActive(true);
        }
    }
    private void PlayToMenu()
    {
        
    }
    private void MenuToPlay()
    {

    }
    private void GameObjTrueFalse()
    {

    }
    private void MenuOrResultToStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    #endregion
}