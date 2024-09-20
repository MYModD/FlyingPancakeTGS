using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawConeCircle : MonoBehaviour {

    public Camera _mainCamera;

    [SerializeField]
    private GameObject _canvas = default;

    [SerializeField, Header("CameraManger")]
    private TestLockOnManager _testLockOnManager;

    public bool _debugIsON = false;

    void Start() {

    }

    // Update is called once per frame
    private void Update() {


        Vector3 hoge = RectTransformUtility.WorldToScreenPoint(_mainCamera, _testLockOnManager._circleCenterPostion);



        transform.position = hoge;

        //float scale = _testLockOnManager._coneAngle * 0.1339f;
        //transform.localScale = new Vector3(scale, scale, scale);

    }


#if UNITY_EDITOR
    private void OnDrawGizmos() {

        if (!_debugIsON) {

            Vector3 hoge = RectTransformUtility.WorldToScreenPoint(Camera.main, _testLockOnManager._circleCenterPostion);
            this.transform.position = hoge;
            float scale = _testLockOnManager._coneAngle * 0.1339f;
            transform.localScale = new Vector3(scale, scale, scale);



            //Quaternion hoge = _testLockOnManager._circleRotation;

            //transform.rotation = hoge;
            //transform.position = _testLockOnManager._circleCenterPostion;

            //float scale = _testLockOnManager._circleRadius * 0.01487f;
            //transform.localScale = new Vector3(scale, scale, scale);

        }

    }
#endif
}