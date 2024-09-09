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
    #endregion
    #region プロパティ
    #endregion
    #region メソッド
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject == _player) {
            _camera.IsResultMoveSwitch();
            _playerMove.StopMoving(10000f);
            _miniScore.TextTrue(true);
        }
    }
    #endregion
}