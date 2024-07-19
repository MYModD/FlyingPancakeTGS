using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitchingJudgment : MonoBehaviour
{
    [Header("カメラ切り替え管理インデックス"), SerializeField] private int _switchiCameraManagementIndex;
    [SerializeField, Tag] string _player = default;
    [SerializeField] CameraManager _cameraManager;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag(_player)) {

            _cameraManager.GetCameraSwitchingValue(_switchiCameraManagementIndex);
        }
    }
}
