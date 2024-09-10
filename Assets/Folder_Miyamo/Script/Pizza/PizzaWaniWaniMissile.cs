using UnityEngine;
using Cysharp.Threading.Tasks; // UniTask���g�p���邽�߂ɕK�v
using System.Threading;
using System;

public class PizzaWaniWaniMissile : MonoBehaviour {
    [SerializeField] private Transform _startTransform;  // �J�n�ʒu
    [SerializeField] private Transform _endTransform;    // �ڕW�ʒu
    [SerializeField, Range(0, 1f)] private float _lerpTForward = 0.5f;  // �s���Ƃ��̑��x
    [SerializeField, Range(0, 1f)] private float _lerpTBackward = 0.3f; // �߂�Ƃ��̑��x
    [SerializeField] private float _epsilon = 0.01f;     // �ڕW�ʒu�Ƃ̌덷�̋��e�͈�
    [SerializeField] private float _waitDuration = 2f;   // ��~����
    [SerializeField] private float _damage = 10f;        // �v���C���[�ɗ^����_���[�W
    [SerializeField] private Camera _mainCamera;         // ���C���J����
    [SerializeField] private GameObject _damageOverlay;  // �J�����𕢂��I�[�o�[���C�I�u�W�F�N�g�iUI�Ȃǁj

    private enum MissileState {
        Idle,
        MovingToTarget,
        StayingAtTarget,
        MovingBack
    }

    private MissileState _currentState = MissileState.Idle;

    private void Start() {
        transform.position = _startTransform.position; // �����ʒu���X�^�[�g�ʒu�ɐݒ�
        if (_damageOverlay != null)
            _damageOverlay.SetActive(false); // ������Ԃł̓I�[�o�[���C���\����
    }

    private void FixedUpdate() {
        switch (_currentState) {
            case MissileState.MovingToTarget:
                MoveTowards(_endTransform.position, _lerpTForward);
                if (HasReached(_endTransform.position)) {
                    _currentState = MissileState.StayingAtTarget;
                    StayAtTargetAsync().Forget();
                }
                break;

            case MissileState.MovingBack:
                MoveTowards(_startTransform.position, _lerpTBackward);
                if (HasReached(_startTransform.position)) {
                    _currentState = MissileState.Idle;
                }
                break;

            case MissileState.Idle:
            case MissileState.StayingAtTarget:
                // �������Ȃ�
                break;
        }
    }

    /// <summary>
    /// �~�T�C���̓�����J�n���郁�\�b�h
    /// </summary>
    public void LaunchPizzaBox() {
        if (_currentState != MissileState.Idle) {
            Debug.LogWarning("�~�T�C���͊��ɓ��쒆�ł��B");
            return;
        }

        _currentState = MissileState.MovingToTarget;
    }

    /// <summary>
    /// �w�肳�ꂽ�ʒu�Ɍ������Ĉړ�����
    /// </summary>
    /// <param name="targetPosition">�ڕW�ʒu</param>
    /// <param name="lerpT">�ړ����x�̕�Ԓl</param>
    private void MoveTowards(Vector3 targetPosition, float lerpT) {
        transform.position = Vector3.Lerp(transform.position, targetPosition, lerpT * Time.fixedDeltaTime);
    }


    /// <summary>
    /// �ڕW�ʒu�ɓ��B�������𔻒肷��
    /// </summary>
    /// <param name="targetPosition">�ڕW�ʒu</param>
    /// <returns>���B���Ă���� true</returns>
    private bool HasReached(Vector3 targetPosition) {
        return Vector3.Distance(transform.position, targetPosition) < _epsilon;
    }

    /// <summary>
    /// �ڕW�ʒu�ɑ؍݂��鏈��
    /// </summary>
    /// <returns></returns>
    private async UniTaskVoid StayAtTargetAsync() {
        // �I�[�o�[���C��\��
        if (_damageOverlay != null)
            _damageOverlay.SetActive(true);

        // �v���C���[�Ƀ_���[�W��^���鏈���i�����ł͒P���Ƀ_���[�W��K�p�j
        DealDamage();

        // �w�肳�ꂽ���ԑҋ@
        await UniTask.Delay(TimeSpan.FromSeconds(_waitDuration));

        // �I�[�o�[���C���\��
        if (_damageOverlay != null)
            _damageOverlay.SetActive(false);

        // �߂鏈�����J�n
        _currentState = MissileState.MovingBack;
    }

    /// <summary>
    /// �v���C���[�Ƀ_���[�W��^���鏈��
    /// </summary>
    private void DealDamage() {
        // �����ł̓v���C���[�̃X�N���v�g���擾���ă_���[�W��K�p���܂��B
        // �v���C���[�̃^�O�� "Player" �Ɖ��肵�Ă��܂��B

        //GameObject player = GameObject.FindGameObjectWithTag("Player");
        //if (player != null) {
        //    // �v���C���[�Ƀ_���[�W��^����X�N���v�g������Ɖ���
        //    PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        //    if (playerHealth != null) {
        //        playerHealth.TakeDamage(_damage);
        //        Debug.Log($"�v���C���[�� {_damage} �̃_���[�W��^���܂����B");
        //    } else {
        //        Debug.LogWarning("PlayerHealth �R���|�[�l���g��������܂���B");
        //    }
        //} else {
        //    Debug.LogWarning("�v���C���[��������܂���B");
        //}
    }


    private void Update() {

        if (Input.GetKeyDown(KeyCode.Y)){
            LaunchPizzaBox();
        }


    }
}
