using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChengeLockOnDistance : MonoBehaviour
{
    // Start is called before the first frame update
    public TestLockOnManager _cameraLockOn;

    [Header("コーンの長さ、半径")]
    public float _sphereDistance;

    [Header("コーンの長さ、半径")]
    public float _coneRange;

    [SerializeField, Range(0f, 180f)]
    [Header("コーンの角度")]
    public float _coneAngle;
    private void OnEnable() {

        _cameraLockOn._searchRadius = _sphereDistance;
        _cameraLockOn._coneAngle = _coneAngle;
        _cameraLockOn._coneRange = _coneRange;


    }
}
