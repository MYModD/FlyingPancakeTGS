using UnityEngine;
using Cysharp.Threading.Tasks;
using NaughtyAttributes;

public class EnemyAttack : MonoBehaviour {
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
    public Vector2 _durationRange = new();

    [Header("�A�j���[�V����")]
    public Animator _animator;

    private bool _isMoving = false;
    private float _timerValue;

    // ���\�b�h�ňʒu�ړ��A�؍݁A�߂�����s
    public async UniTaskVoid MoveToAttackPosition() {
        // ���łɈړ����̏ꍇ�͏������X�L�b�v
        if (_isMoving) {
            Debug.Log("�ړ����ł��B�V�����A�N�V�����͖�������܂��B");
            return;
        }
        _isMoving = true;  // �ړ����J�n����
        // �U���ʒu�Ɉړ�
        Debug.Log("�N�����܂���");
        _animator.SetTrigger("Start");
        await MoveToPosition(_attackTransform.localPosition, _moveSpeedToAttack);
        // �؍݂���
        await UniTask.Delay((int)(_stayDuration * 1000)); // milliseconds
        // ���̈ʒu�ɖ߂�
        await MoveToPosition(_startTransform.localPosition, _moveSpeedToStart);
        _isMoving = false; // �ړ��I��
    }

    // ���X�Ɉʒu���ړ����鏈��
    private async UniTask MoveToPosition(Vector3 targetPosition, float speed) {
        while (Vector3.Distance(transform.localPosition, targetPosition) > _epsilon) {
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, speed * Time.fixedDeltaTime);
            await UniTask.Yield(); // ���̃t���[���܂őҋ@
        }
        // �ŏI�I�ɂ҂����荇�킹��
        //transform.localPosition = targetPosition;
        Debug.Log("��~���܂�");
    }

    private void Update() {
        _timerValue -= Time.deltaTime;
        _timerValue = Mathf.Max(0, _timerValue);
        if (_timerValue <= 0) {
            MoveToAttackPosition().Forget();
            _timerValue = Random.Range(_durationRange.x, _durationRange.y);
            Debug.Log($"�^�C�}�[��{_timerValue}");
        }
    }

    private void OnTriggerEnter(Collider other) {
        //leftArm���s�U�ɂ��������������
        if (other.CompareTag(_pizzaCoinTag)) {
            other.gameObject.SetActive(false);
        }
    }

    private void OnEnable() {
        _timerValue = Random.Range(_durationRange.x, _durationRange.y);
    }
}