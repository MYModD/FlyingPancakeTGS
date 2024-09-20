using UnityEngine;

public class AircraftAnimation : MonoBehaviour {
    // ��]�̎� (X, Y, Z �̂����ꂩ���w��)
    [SerializeField] private Vector3 _rotationAxis = Vector3.up;

    // ��]���������ڕW�p�x
    [SerializeField] private float _targetAngle1 = 90f;

    // ���̊p�x
    [SerializeField] private float _targetAngle2 = 0f;

    // ��]���x
    [SerializeField] private float _rotationSpeed = 2f;

    // ���݂̊p�x
    private float _currentAngle = 0f;

    // ��]�����邩�ǂ���
    [SerializeField] private bool _shouldRotate = true;

    private bool _finishRotate = false;   // ��]�������������ǂ���
    private bool _isRotating = false;     // ��]�����ǂ���
    private bool _isTargetAngle1 = false;  // ���݂̖ڕW�p�x���^�[�Q�b�g1���ǂ���
    private float _rotationThreshold = 0.1f; // �ڕW�p�x�Ƃ̋��e��

    private float _startAngle;  // ��]�̊J�n�p�x
    private float _rotationProgress = 0f;  // ��]�̐i�s�x

    void Start() {
        _currentAngle = _targetAngle2; // �����p�x��ݒ�
        Variable();
    }

    void Update() {

        // ��]����
        if (_isRotating && !_finishRotate) {
            // ��]�i�s�x�𑝉�������iLerp�̐i�s�x�j
            _rotationProgress += Time.deltaTime * _rotationSpeed;

            // ���݂̖ڕW�p�x������
            float targetAngle = _isTargetAngle1 ? _targetAngle1 : _targetAngle2;

            // ���݂̊p�x�����炩�ɖڕW�p�x�Ɍ������čX�V (LerpAngle�ŃX���[�Y�ɉ�])
            _currentAngle = Mathf.LerpAngle(_startAngle, targetAngle, _rotationProgress);

            // �w�肳�ꂽ���ɉ����ăI�u�W�F�N�g�����[�J�����W�n�ŉ�]
            transform.localRotation = Quaternion.AngleAxis(_currentAngle, _rotationAxis);

            // ��]���ڕW�p�x�ɋ߂Â�����A��]�I���Ƃ���
            if (Mathf.Abs(_currentAngle - targetAngle) < _rotationThreshold) {
                _isRotating = false;
                _finishRotate = true; // ��]�I�����L�^
                _currentAngle = targetAngle; // �Ō�ɖڕW�p�x�ɐ��m�ɍ��킹��
            }
        }
    }

    public void Variable() {

        print("���΂���00");
        if (!_isRotating) {
            _isRotating = true;
            _finishRotate = false; // ��]���n�߂�O�Ƀ��Z�b�g

            // ��]���n�߂�p�x���L�^
            _startAngle = _currentAngle;

            // �ڕW�p�x��؂�ւ�
            _isTargetAngle1 = !_isTargetAngle1;

            print(_isTargetAngle1);

            // ��]�i�s�x�����Z�b�g
            _rotationProgress = 0f;
        }
    }
}
