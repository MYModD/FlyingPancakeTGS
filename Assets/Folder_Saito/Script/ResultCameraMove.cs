// ---------------------------------------------------------
// ResultCameraMove.cs
//
// 作成日:
// 作成者:
// ---------------------------------------------------------
using UnityEngine;
using System.Collections;
public class ResultCameraMove : MonoBehaviour
{
    #region 変数
    [SerializeField]
    private CameraRotate _camera;
    [SerializeField]
    private PlayerMove _playerMove;
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private AudienceGaugeManager _miniScore;
    [SerializeField]
    private GameObject _currsul;
    #endregion
    #region プロパティ
    #endregion
    #region メソッド
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject == _player) {
            //ミニリザルト表示の時のオブジェクトの動き
            //カメラをリザルトのポジションへ
            _camera.IsResultMoveSwitch();
            //指令があるときに動くようにしたいから１００００渡す
            _playerMove.StopMoving(10000f);
            //テキスト表示
            _miniScore.TextTrue(true);
            //ロックオンカーソンを消す
            _currsul.SetActive(false);
        }
    }
    #endregion
}