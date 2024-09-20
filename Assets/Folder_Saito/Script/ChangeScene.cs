// ---------------------------------------------------------
// ChangeScene.cs
//
// 作成日:
// 作成者:
// ---------------------------------------------------------
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class ChangeScene : MonoBehaviour
{
    #region 変数
    [SerializeField]
    private CanvasManager _canvas;
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private AudioSource _audio;
    [SerializeField]
    private AudioClip _clip;
    #endregion
    #region プロパティ
    #endregion
    #region メソッド

/// <summary>
/// 更新処理
/// </summary>
void Update ()
{
    }
    public void PlayEvent() {
        _audio.PlayOneShot(_clip);
        _audio.PlayOneShot(_clip);
    }

    public void OnEvent() {
       _canvas.FictionAnimEnd();
        _audioSource.Play();
    }
    #endregion
}