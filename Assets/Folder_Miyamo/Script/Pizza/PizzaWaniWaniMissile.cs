using UnityEngine;
using Cysharp.Threading.Tasks;
using NaughtyAttributes;

public class PizzaWaniWaniMissile : MonoBehaviour {
    [Header("�ړ��ݒ�")]
    [SerializeField] private Transform _startTransform;  // �J�n�ʒu
    [SerializeField] private Transform _attackTransform; // �U���ʒu
    [SerializeField, Range(0f, 10f)] private float _moveSpeedToAttack = 2f; // �U���ʒu�ɍs���X�s�[�h�i�P��: units/second�j
    [SerializeField, Range(0f, 10f)] private float _moveSpeedToStart = 1f;  // �A��X�s�[�h�i�P��: units/second�j
    [SerializeField] private float _epsilon = 0.01f; // �ʒu�덷�̋��e�͈�

    [Header("�U���ݒ�")]
    [SerializeField] private float _stayDuration = 2f; // �U���ʒu�ő؍݂��鎞��
    [SerializeField, Tag]
    private string _pizzaCoinTag;

    [Header("�^�C�~���O�ݒ�")]
    [MinMaxSlider(0, 5f)]
    public Vector2 _durationRange = new Vector2(1f, 3f);

    [Header("�A�j���[�V����")]
    public Animator _animator;

    private bool _isMoving = false;
    private float _timerValue;

    public async UniTaskVoid MoveToAttackPosition() {
        if (_isMoving) {
            Debug.Log("�ړ����ł��B�V�����A�N�V�����͖�������܂��B");
            return;
        }

        _isMoving = true;
        Debug.Log("�N�����܂���");
        
        //_animator.SetTrigger("Start");

        await MoveToPosition(_attackTransform.localPosition, _moveSpeedToAttack);
        await UniTask.Delay((int)(_stayDuration * 1000));
        await MoveToPosition(_startTransform.localPosition, _moveSpeedToStart);

        _isMoving = false;
    }

    private async UniTask MoveToPosition(Vector3 targetPosition, float speed) {
        Vector3 startPosition = transform.localPosition;
        float distance = Vector3.Distance(startPosition, targetPosition);
        float duration = distance / speed;
        float elapsedTime = 0f;

        while (elapsedTime < duration) {
            float t = elapsedTime / duration;
            transform.localPosition = Vector3.Lerp(startPosition, targetPosition, t);
            elapsedTime += Time.deltaTime;
            await UniTask.Yield();
        }

        transform.localPosition = targetPosition;
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
        if (other.CompareTag(_pizzaCoinTag)) {
            other.gameObject.SetActive(false);
        }
    }

    private void OnEnable() {
        _timerValue = Random.Range(_durationRange.x, _durationRange.y);
    }
}