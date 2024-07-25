using System.Collections;
using UnityEngine;

public class DirectionController : MonoBehaviour {
    private bool _shouldMaintainDirection;
    private bool _shouldRotate;
    private Vector3 _initialDirection;
    private Quaternion _targetRotation;

    void Update() {
        if (_shouldMaintainDirection) {
            MaintainDirection();
        }
    }

    public void SetMaintainDirection(bool shouldMaintain, float rotationAmount) {
        _shouldMaintainDirection = shouldMaintain;
        if (_shouldMaintainDirection) {
            _initialDirection = transform.forward;
            StartCoroutine(RotateAndMaintainDirection(rotationAmount));
        }
    }

    public void SetRotate(bool shouldRotate) {
        _shouldRotate = shouldRotate;
    }

    private void MaintainDirection() {
        transform.rotation = _targetRotation;
    }

    private IEnumerator RotateAndMaintainDirection(float rotationAmount) {
        float elapsedTime = 0f;
        float duration = 1f; // ‰ñ“]‚É‚©‚¯‚éŽžŠÔ (•b)

        Quaternion initialRotation = transform.rotation;
        _targetRotation = Quaternion.Euler(transform.eulerAngles.x + rotationAmount, transform.eulerAngles.y, transform.eulerAngles.z);

        while (elapsedTime < duration) {
            transform.rotation = Quaternion.Slerp(initialRotation, _targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = _targetRotation;

        // _shouldRotate ‚ª false ‚É‚È‚é‚Ü‚Å‘Ò‚Â
        while (_shouldRotate) {
            yield return null;
        }
    }
}
