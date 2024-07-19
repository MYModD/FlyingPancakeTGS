using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitchingJudgment : MonoBehaviour
{
    [Header("カメラ切り替え管理インデックス"), SerializeField] private int _switchiCameraManagementIndex;
    [SerializeField, Tag] private string _player = default;
    [SerializeField] private CameraManager _cameraManager;
    [SerializeField] private PlayerMove _playerMove;

    private float _stopTime = default;
    private void Start() {

        _stopTime = _cameraManager.TimeToSwitchCamera();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag(_player)) {
 
            _cameraManager.GetCameraSwitchingValue(_switchiCameraManagementIndex);
            _playerMove.StopMoving(_stopTime);
        }
    }
}
