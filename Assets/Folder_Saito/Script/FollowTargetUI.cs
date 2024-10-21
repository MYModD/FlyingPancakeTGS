// ---------------------------------------------------------
// FollowTargetUI.cs
//
// 作成日:
// 作成者:
// ---------------------------------------------------------
using UnityEngine;
using System.Collections;
public class FollowTargetUI : MonoBehaviour
{
    [SerializeField] private RectTransform _uiElement;
    [SerializeField] private Transform _target;

    private Camera _mainCamera;

    private void Start() {
        _mainCamera = Camera.main;
    }

    private void Update() {
        UpdateUIPosition();
    }

    private void UpdateUIPosition() {
        //追尾させる
        Vector3 screenPos = _mainCamera.WorldToScreenPoint(_target.position);
        _uiElement.position = screenPos+(Vector3.down*75);
    }
}