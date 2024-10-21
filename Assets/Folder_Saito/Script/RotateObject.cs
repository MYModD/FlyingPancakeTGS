using UnityEngine;

public class RotateObject : MonoBehaviour {
    [Header("回転速度 (degrees per second)")]
    [SerializeField] private float _rotationSpeed = 90f;
    private void Update() {
        RotateContinuously();
    }
    /// <summary>
    /// 回す
    /// </summary>
    private void RotateContinuously() {
        transform.Rotate(Vector3.right * _rotationSpeed * Time.deltaTime);
    }
}
