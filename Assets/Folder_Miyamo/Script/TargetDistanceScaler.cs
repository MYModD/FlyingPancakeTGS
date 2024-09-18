using UnityEngine;

using NaughtyAttributes;


public class TargetDistanceScaler : MonoBehaviour {
    [SerializeField] private Transform _target; // スケールを調整する対象

    [SerializeField] private float _minDistance = 300f;
    [SerializeField] private float _maxDistance = 27000f;
    [SerializeField] private float _minScale = 100f;
    [SerializeField] private float _maxScale = 300f;

    [SerializeField, Range(300f, 27000f)]
    private float _debugDistance = 300f;

    [SerializeField, ReadOnly]
    private float _currentScale;

    [SerializeField]
    private bool _useDebugDistance = true;

    

    private void Update() {
        UpdateScale();
    }

    private void UpdateScale() {
        if (_target == null) {
            _target = GetComponent<PizzaMissile>()._player;
        }

        float distance =  Vector3.SqrMagnitude(_target.position -  transform.position);
        float t = Mathf.InverseLerp(_minDistance, _maxDistance, distance);
        Debug.LogError(distance);
        float scale =  Mathf.Lerp(_minScale, _maxScale, t);

        this.transform.localScale = new Vector3(scale, scale, scale);


    }


}