using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChengeLockOnDistance : MonoBehaviour
{
    // Start is called before the first frame update
    public TestLockOnManager _cameraLockOn;

    [Header("�R�[���̒����A���a")]
    public float _sphereDistance;

    [Header("�R�[���̒����A���a")]
    public float _coneRange;

    [SerializeField, Range(0f, 180f)]
    [Header("�R�[���̊p�x")]
    public float _coneAngle;
    private void OnEnable() {

        _cameraLockOn._searchRadius = _sphereDistance;
        _cameraLockOn._coneAngle = _coneAngle;
        _cameraLockOn._coneRange = _coneRange;


    }
}
