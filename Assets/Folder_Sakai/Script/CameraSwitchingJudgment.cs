using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitchingJudgment : MonoBehaviour
{
    [Header("カメラ切り替え管理インデックス"), SerializeField] private int _switchiCameraManagementIndex;
    [SerializeField, Tag] private string _player = default;
    [SerializeField] private CameraManager _cameraManager;
    [SerializeField] private PlayerMove _playerMove;
    [SerializeField] private float _stopTime = 4.0f;


    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag(_player)) {
 
            _cameraManager.GetCameraSwitchingValue(_switchiCameraManagementIndex,_stopTime);
            _playerMove.StopMoving(_stopTime);
         }
    }
}
