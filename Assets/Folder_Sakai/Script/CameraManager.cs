using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {
    [SerializeField] private GameObject[] _cameras;
    private int _mainCameraIndex = 0;
    private int _swichCameraIndex = default;
    private float _cameraSwitchingDuration;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void GetCameraSwitchingValue(int value,float cameraSwitchingDuration) {

        _swichCameraIndex = value;
        _cameraSwitchingDuration = cameraSwitchingDuration;
        CameraSwitchingSub();
    }

    private void CameraSwitchingSub() {

        _cameras[_swichCameraIndex].SetActive(true);
        _cameras[_mainCameraIndex].SetActive(false);  
        StartCoroutine(CameraSwitchingMain());
    }

    private IEnumerator CameraSwitchingMain() {

        yield return new WaitForSeconds(_cameraSwitchingDuration);
        _cameras[_mainCameraIndex].SetActive(true);
        _cameras[_swichCameraIndex].SetActive(false);
    }

}
