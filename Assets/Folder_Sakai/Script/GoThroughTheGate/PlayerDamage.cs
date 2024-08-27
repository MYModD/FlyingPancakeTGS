using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour {
    // MeshRenderer�̔z��ɕύX
    [SerializeField]
    MeshRenderer[] _meshRenderers;

    // �ꃋ�[�v�̒���(�b��)
    float _flickerInterval = 0.075f;

    // �J�n���̐F
    Color _startColor = new Color(1, 1, 1, 1);

    // �I��(�܂�Ԃ�)���̐F
    Color _endColor = new Color(1, 1, 1, 0);

    // �o�ߎ���
    float _flickerElapsedTime;

    // �_�ł̒���(�b��)
    float _flickerDuration = 2.0f;

    // �_�ŃR���[�`���Ǘ��p
    Coroutine _flicker;

    // ��_���[�W�t���O
    bool _isDamaged = false;

    // Awake���\�b�h��MeshRenderer���擾
    void Awake() {
        // �q�I�u�W�F�N�g�ɂ��邷�ׂĂ�MeshRenderer���擾
        _meshRenderers = GetComponentsInChildren<MeshRenderer>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            print("������");
            Damaged();
        }
    }

    // �_���[�W���󂯂����ɌĂ�
    void Damaged() {
        if (_isDamaged)
            return;
        StartFlicker();
    }

    // �_�ŊJ�n
    void StartFlicker() {
        _flicker = StartCoroutine(Flicker());
    }

    // �_�ŏ���
    IEnumerator Flicker() {
        _isDamaged = true;
        _flickerElapsedTime = 0;

        while (true) {
            _flickerElapsedTime += Time.deltaTime;

            // �eMeshRenderer�̐F��ύX
            foreach (var meshRenderer in _meshRenderers) {
                foreach (var material in meshRenderer.materials) {
                    material.color = Color.Lerp(_startColor, _endColor, Mathf.PingPong(_flickerElapsedTime / _flickerInterval, 1.0f));
                }
            }

            // �_�ŏI��
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

    // �X�e�[�W�؂�ւ������ŁA�����I��Flicker���~������p
    public void ResetFlicker() {
        if (_flicker != null) {
            StopCoroutine(_flicker);
            _flicker = null;
        }
    }
}
