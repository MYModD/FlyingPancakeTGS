using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitchingJudgment : MonoBehaviour
{
    [Header("�J�����؂�ւ��Ǘ��C���f�b�N�X"), SerializeField] private int _switchiCameraManagementIndex;
    [SerializeField, Tag] string _player = default;
    [SerializeField] CameraManager _cameraManager;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag(_player)) {

            _cameraManager.GetCameraSwitchingValue(_switchiCameraManagementIndex);
        }
    }
}
