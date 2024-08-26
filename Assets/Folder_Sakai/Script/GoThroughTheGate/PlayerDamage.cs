using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour {
    // MeshRendererの配列に変更
    [SerializeField]
    MeshRenderer[] _meshRenderers;

    // 一ループの長さ(秒数)
    float _flickerInterval = 0.075f;

    // 開始時の色
    Color _startColor = new Color(1, 1, 1, 1);

    // 終了(折り返し)時の色
    Color _endColor = new Color(1, 1, 1, 0);

    // 経過時間
    float _flickerElapsedTime;

    // 点滅の長さ(秒数)
    float _flickerDuration = 2.0f;

    // 点滅コルーチン管理用
    Coroutine _flicker;

    // 被ダメージフラグ
    bool _isDamaged = false;

    // AwakeメソッドでMeshRendererを取得
    void Awake() {
        // 子オブジェクトにあるすべてのMeshRendererを取得
        _meshRenderers = GetComponentsInChildren<MeshRenderer>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            print("おせた");
            Damaged();
        }
    }

    // ダメージを受けた時に呼ぶ
    void Damaged() {
        if (_isDamaged)
            return;
        StartFlicker();
    }

    // 点滅開始
    void StartFlicker() {
        _flicker = StartCoroutine(Flicker());
    }

    // 点滅処理
    IEnumerator Flicker() {
        _isDamaged = true;
        _flickerElapsedTime = 0;

        while (true) {
            _flickerElapsedTime += Time.deltaTime;

            // 各MeshRendererの色を変更
            foreach (var meshRenderer in _meshRenderers) {
                foreach (var material in meshRenderer.materials) {
                    material.color = Color.Lerp(_startColor, _endColor, Mathf.PingPong(_flickerElapsedTime / _flickerInterval, 1.0f));
                }
            }

            // 点滅終了
            if (_flickerDuration <= _flickerElapsedTime) {
                _isDamaged = false;
                foreach (var meshRenderer in _meshRenderers) {
                    foreach (var material in meshRenderer.materials) {
                        material.color = _startColor;
                    }
                }
                _flicker = null;
                yield break;
            }

            yield return null;
        }
    }

    // ステージ切り替え時等で、強制的にFlickerを停止させる用
    public void ResetFlicker() {
        if (_flicker != null) {
            StopCoroutine(_flicker);
            _flicker = null;
        }
    }
}
