using UnityEngine;
using Cysharp.Threading.Tasks;
using NaughtyAttributes;

public class EnemyAttack : MonoBehaviour {

    [SerializeField] private Transform _startTransform;  // �J�n�ʒu
    [SerializeField] private Transform[] _attackTransform; // �U���ʒu
    [SerializeField, Range(0f, 1f)] private float _moveSpeedToAttack = 0.5f; // �U���ʒu�ɍs���X�s�[�h
    [SerializeField, Range(0f, 1f)] private float _moveSpeedToStart = 0.3f;  // �A��X�s�[�h
    [SerializeField] private float _epsilon = 0.01f; // �ʒu�덷�̋��e�͈�
    [SerializeField] private float _stayDuration = 2f; // �U���ʒu�ő؍݂��鎞��

    [MinMaxSlider(0,5f)]
    public Vector2 _durationRange = new ();

    private bool _isMoving = false;
    private float _timerValue;


    [SerializeField, Header("���̍U���ڕW")]
    private int _attackPos;

    // ���\�b�h�ňʒu�ړ��A�؍݁A�߂�����s
    public async UniTaskVoid MoveToAttackPosition() {
        // ���łɈړ����̏ꍇ�͏������X�L�b�v
        if (_isMoving) {
            Debug.Log("�ړ����ł��B�V�����A�N�V�����͖�������܂��B");
            return;
        }

        _isMoving = true;  // �ړ����J�n����

        // �U���ʒu�Ɉړ�

        
        await MoveToPosition(_attackTransform[_attackPos].localPosition, _moveSpeedToAttack);




        // �؍݂���
        await UniTask.Delay((int)(_stayDuration * 1000)); // milliseconds

        // ���̈ʒu�ɖ߂�
        await MoveToPosition(_startTransform.localPosition, _moveSpeedToStart);

        _isMoving = false; // �ړ��I��
        _attackPos = Random.Range(0, 2);

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
            Debug.Log($"�^�C�}�[��{_timerValue}�ɐݒ肳��܂���");

        }



    }
    private void OnEnable() {


        _timerValue = Random.Range(_durationRange.x, _durationRange.y);
        _attackPos = Random.Range(0, 2);


    }
}
