using NaughtyAttributes;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem.iOS;

public class TargetDistanceScaler : MonoBehaviour {
    [SerializeField,Header("追いかけてる敵を取得するのに必要")]
    private EnemyMissile _enemyMissile;


    [MinMaxSlider(0, 10000), Header("minは0にしてね")]
    [SerializeField]
    private Vector2 _minMaxDistanceRange = new Vector2(0, 10000);


    [MinMaxSlider(1, 100), Header("最大最小スケール")]
    [SerializeField]
    private Vector2 _minMaxScaleRange = new Vector2(1, 100);



    [SerializeField, Header("カーブで表現できるニョ")]
    public AnimationCurve _targetScaleCurve;

    [SerializeField, Header("debugをONにしますか？")]
    private bool _isDubugOn = false;

    [Range(0, 10000), Header("debug用、ゲームスタートすると使えないよ")]
    [ShowIf(nameof(_isDubugOn))]
    public float _currentDebugtargetDistance;

    const float MULTIPLAY = 100f; 

    private void Update() {

        Transform enemyTarget = _enemyMissile._enemyTarget;
        float enemyTargetDistance = (enemyTarget.position - transform.position).sqrMagnitude;


        //debug用とは違い、targetを参照してその距離を求める
        float scale = _targetScaleCurve.Evaluate(enemyTargetDistance / _minMaxDistanceRange.y);

        scale = scale * MULTIPLAY;
        scale = Mathf.Clamp(scale, _minMaxScaleRange.x, _minMaxScaleRange.y);

        this.transform.localScale = new Vector3(scale, scale, scale);


    }

#if UNITY_EDITOR
    /// <summary>
    /// debug用
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
