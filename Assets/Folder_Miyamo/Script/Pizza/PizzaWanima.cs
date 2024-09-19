using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using System;
using System.Threading;
using UnityEngine;

public class PizzaWanima : MonoBehaviour {
    [Header("�ړ��ݒ�")]
    [SerializeField] private Transform _startTransform;  // �J�n�ʒu
    [SerializeField] private Transform _attackTransform; // �U���ʒu
    [SerializeField, Range(0f, 1f)] private float _moveSpeedToAttack = 0.5f; // �U���ʒu�ɍs���X�s�[�h
    [SerializeField, Range(0f, 1f)] private float _moveSpeedToStart = 0.3f;  // �A��X�s�[�h
    [SerializeField] private float _epsilon = 0.01f; // �ʒu�덷�̋��e�͈�

    [Header("�U���ݒ�")]
    [SerializeField] private float _stayDuration = 2f; // �U���ʒu�ő؍݂��鎞�� 

    [SerializeField, Tag]
    private string _pizzaCoinTag;

    [Header("�^�C�~���O�ݒ�")]
    [MinMaxSlider(0, 5f)]
    public Vector2 _durationRange = new Vector2();

    [Header("�A�j���[�V����")]
    public Animator _animator;

    private bool _isMoving = false;
    private float _timerValue;
    private CancellationTokenSource _cts;

    private void OnEnable() {
        _timerValue = UnityEngine.Random.Range(_durationRange.x, _durationRange.y);
        _cts = new CancellationTokenSource();
    }

    private void OnDisable() {
        _cts?.Cancel();
        _cts?.Dispose();
    }

    private void Update() {
        _timerValue -= Time.deltaTime;
        _timerValue = Mathf.Max(0, _timerValue);
        if (_timerValue <= 0) {
            MoveToAttackPosition().Forget();
            _timerValue = UnityEngine.Random.Range(_durationRange.x, _durationRange.y);
            Debug.Log($"�^�C�}�[��{_timerValue}");
        }
    }

    public async UniTaskVoid MoveToAttackPosition() {
        if (_isMoving) {
            Debug.Log("�ړ����ł��B�V�����A�N�V�����͖�������܂��B");
            return;
        }

        _isMoving = true;

        try {
            Debug.Log("�N�����܂���");
            _animator.SetTrigger("Start");

            await MoveToPosition(_attackTransform.localPosition, _moveSpeedToAttack);
            await UniTask.Delay(TimeSpan.FromSeconds(_stayDuration), cancellationToken: _cts.Token);
            await MoveToPosition(_startTransform.localPosition, _moveSpeedToStart);
        } catch (OperationCanceledException) {
            Debug.Log("�ړ����L�����Z������܂���");
        } catch (Exception ex) {
            Debug.LogError($"�ړ����ɃG���[���������܂���: {ex.Message}");
        } finally {
            _isMoving = false;
        }
    }

    private async UniTask MoveToPosition(Vector3 targetPosition, float speed) {
        while (Vector3.Distance(transform.localPosition, targetPosition) > _epsilon) {
            _cts.Token.ThrowIfCancellationRequested();
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, speed * Time.fixedDeltaTime);
            await UniTask.Yield(_cts.Token);
        }

        Debug.Log("��~���܂�");
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(_pizzaCoinTag)) {
            other.gameObject.SetActive(false);
        }
    }
}