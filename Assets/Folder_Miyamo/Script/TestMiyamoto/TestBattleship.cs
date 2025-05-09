using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBattleship : MonoBehaviour {
    [Foldout("大砲")]
    [SerializeField, Header("大砲01")]
    private Transform _largeTurret01;
    [Foldout("大砲")]
    [SerializeField, Header("大砲01の回転範囲")]
    [MinMaxSlider(-360, 360f)] private Vector2 _largeTurret01Range;

    [Foldout("大砲")]
    [SerializeField, Header("大砲02")]
    private Transform _largeTurret02;
    [Foldout("大砲")]
    [SerializeField, Header("大砲02の回転範囲")]
    [MinMaxSlider(-50, 50)] private Vector2 _largeTurret02Range;

    [Foldout("敵")]
    [SerializeField, Header("敵のトランスフォーム")]
    private Transform _enemyTransform;

    // Update is called once per frame
    void FixedUpdate() {
        // _largeTurret01の回転
        RotateTurretY(_largeTurret01, _largeTurret01Range, _enemyTransform);

        // _largeTurret02の回転
        RotateTurretX(_largeTurret01, _largeTurret02, _largeTurret02Range, _enemyTransform);
    }

    private void RotateTurretY(Transform turretTransform, Vector2 rotationRange, Transform target) {
        Vector3 direction = target.position - turretTransform.position;
        direction.y = 0; // Y軸の回転だけを考慮するために、Y成分を0にする

        if (direction != Vector3.zero) {
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // 現在の回転角度をY軸に制限
            float targetYRotation = targetRotation.eulerAngles.y;

            // 回転範囲内に制限
            targetYRotation = Mathf.Clamp(targetYRotation, rotationRange.x, rotationRange.y);

            // 新しい回転角度をY軸に設定
            Quaternion limitedRotation = Quaternion.Euler(0, targetYRotation, 0);

            // 球面線形補間を使って回転を徐々にターゲットに向ける
            turretTransform.rotation = Quaternion.Lerp(turretTransform.rotation, limitedRotation, 0.05f);
        }
    }

    private void RotateTurretX(Transform parentTransform, Transform turretTransform, Vector2 rotationRange, Transform target) {
        Vector3 direction = target.position - parentTransform.position;

        if (direction != Vector3.zero) {
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // 現在のX軸回転角度を制限
            float targetXRotation = targetRotation.eulerAngles.x;
            if (targetXRotation > 180)
                targetXRotation -= 360; // X軸回転角度の正規化
            targetXRotation = Mathf.Clamp(targetXRotation, rotationRange.x, rotationRange.y);

            // 新しい回転角度を設定
            Quaternion limitedRotation = Quaternion.Euler(targetXRotation, parentTransform.eulerAngles.y, 0);

            // 球面線形補間を使って回転を徐々にターゲットに向ける
            turretTransform.rotation = Quaternion.Lerp(turretTransform.rotation, limitedRotation, 0.05f);
        }
    }
}
