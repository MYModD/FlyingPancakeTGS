using NaughtyAttributes;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem.iOS;

public class TargetDistanceScaler : MonoBehaviour {
    [SerializeField,Header("�ǂ������Ă�G���擾����̂ɕK�v")]
    private EnemyMissile _enemyMissile;


    [MinMaxSlider(0, 10000), Header("min��0�ɂ��Ă�")]
    [SerializeField]
    private Vector2 _minMaxDistanceRange = new Vector2(0, 10000);


    [MinMaxSlider(1, 100), Header("�ő�ŏ��X�P�[��")]
    [SerializeField]
    private Vector2 _minMaxScaleRange = new Vector2(1, 100);



    [SerializeField, Header("�J�[�u�ŕ\���ł���j��")]
    public AnimationCurve _targetScaleCurve;

    [SerializeField, Header("debug��ON�ɂ��܂����H")]
    private bool _isDubugOn = false;

    [Range(0, 10000), Header("debug�p�A�Q�[���X�^�[�g����Ǝg���Ȃ���")]
    [ShowIf(nameof(_isDubugOn))]
    public float _currentDebugtargetDistance;

    const float MULTIPLAY = 100f; 

    private void Update() {

        Transform enemyTarget = _enemyMissile._enemyTarget;
        float enemyTargetDistance = (enemyTarget.position - transform.position).sqrMagnitude;


        //debug�p�Ƃ͈Ⴂ�Atarget���Q�Ƃ��Ă��̋��������߂�
        float scale = _targetScaleCurve.Evaluate(enemyTargetDistance / _minMaxDistanceRange.y);

        scale = scale * MULTIPLAY;
        scale = Mathf.Clamp(scale, _minMaxScaleRange.x, _minMaxScaleRange.y);

        this.transform.localScale = new Vector3(scale, scale, scale);


    }

#if UNITY_EDITOR
    /// <summary>
    /// debug�p
    /// </summary>
    private void OnValidate() {
        if (_isDubugOn) {

            float scale = _targetScaleCurve.Evaluate(_currentDebugtargetDistance / _minMaxDistanceRange.y);

            scale = scale * MULTIPLAY;
            scale = Mathf.Clamp(scale, _minMaxScaleRange.x, _minMaxScaleRange.y);
            Debug.Log(_minMaxScaleRange.y);

            this.transform.localScale = new Vector3(scale, scale, scale);

            
        }

       
    }
#endif

}
