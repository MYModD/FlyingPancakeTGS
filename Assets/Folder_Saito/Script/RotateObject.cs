using UnityEngine;

public class RotateObject : MonoBehaviour {
    [Header("回転速度 (degrees per second)")]
    [SerializeField] private float _rotationSpeed = 90f;

    private void Start() {
        InitializeRotation();
    }

    private void Update() {
        RotateContinuously();
    }

    private void InitializeRotation() {
        // 初期設定があればここで
    }

    private void RotateContinuously() {
        transform.Rotate(Vector3.right * _rotationSpeed * Time.deltaTime);
    }
}
