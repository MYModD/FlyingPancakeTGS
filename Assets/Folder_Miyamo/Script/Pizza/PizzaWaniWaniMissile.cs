using UnityEngine;
using Cysharp.Threading.Tasks;

public class EnemyAttack : MonoBehaviour {

    [SerializeField] private Transform _startTransform;  // �J�n�ʒu
    [SerializeField] private Transform _attackTransform; // �U���ʒu
    [SerializeField, Range(0f, 1f)] private float _moveSpeedToAttack = 0.5f; // �U���ʒu�ɍs���X�s�[�h
    [SerializeField, Range(0f, 1f)] private float _moveSpeedToStart = 0.3f;  // �A��X�s�[�h
    [SerializeField] private float _epsilon = 0.01f; // �ʒu�덷�̋��e�͈�
    [SerializeField] private float _stayDuration = 2f; // �U���ʒu�ő؍݂��鎞��

    private bool _isMoving = false;

    private void Start() {
        MoveToAttackPosition().Forget();
    }

    // ���\�b�h�ňʒu�ړ��A�؍݁A�߂�����s
    public async UniTaskVoid MoveToAttackPosition() {
        // �ړ�����
        await MoveToPosition(_attackTransform.position, _moveSpeedToAttack);

        // �؍݂���
        await UniTask.Delay((int)(_stayDuration * 1000)); // milliseconds

        // �A��
        await MoveToPosition(_startTransform.position, _moveSpeedToStart);
    }

    // ���X�Ɉʒu���ړ����鏈��
    private async UniTask MoveToPosition(Vector3 targetPosition, float speed) {
        _isMoving = true;

        while (Vector3.Distance(transform.position, targetPosition) > _epsilon) {
            transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.fixedDeltaTime);
            await UniTask.Yield(); // ���̃t���[���܂őҋ@
        }

        // �ŏI�I�ɂ҂����荇�킹��
        transform.position = targetPosition;
        _isMoving = false;
    }
}
