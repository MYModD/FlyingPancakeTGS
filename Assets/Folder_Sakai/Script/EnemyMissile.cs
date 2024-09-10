using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyMissile : MonoBehaviour, IPooledObject<EnemyMissile> {
    #region �ϐ� + �v���p�e�B  

    [SerializeField, Header("�~�T�C���̃^�[�Q�b�g (�v���C���[)")]
    public Transform _playerTarget;

    [SerializeField, Header("�~�T�C���̑��x")]
    private float _missileSpeed = 100f;

    [SerializeField, Header("�~�T�C���̍ő�ǔ����x �p�x")]
    [Range(0,180f)]
    private float _maxTurnSpeed = 5f;

    [SerializeField, Header("�v���C���[�̑��x")]
    public Rigidbody _playerRigidbody;

    [SerializeField, Header("�������߂Â����猸������͈�")]
    private float _proximityThreshold = 50f;

    [SerializeField, Header("�M���M���������o���邽�߂̌����W��")]
    [Range(0,1f)]
    private float _nearMissSlowdown = 0.5f;

    [SerializeField, Header("������₷�� 0.1�f�t�H")]
    [Range(0f, 1f)]
    private float _lerpT = 0.1f;

    [SerializeField, Header("��s����")]
    private float _timer = 10f;

    [SerializeField, Header("Gforce�̍ő�l")]
    private float _maxAcceleration = 10f;

    [SerializeField, Header("G�l������𒴂���΂܂������ɂ����i�܂Ȃ��Ȃ�")]
    private float _maxHighAcceleration = 3500f;

    [SerializeField, Header("���������True�ɂȂ�")]
    private bool _isOverGforce = false;

    [Header("�v���C���[�̓��͏�Ԃ��L�^����t���O")]
    [SerializeField, NaughtyAttributes.ReadOnly]
    private bool _isPlayerInputActive = false;

    public List<string> _debug;

    // ��莞�ԓ��͂��Ȃ���false�ɂȂ郏�[���h�ϐ� (�ÓI�ϐ�)
    public static bool IsPlayerActive { get; private set; } = true;

    [SerializeField]
    private float _inputCheckDuration = 1f; // ���͂��Ȃ��Ɣ��肷�鎞��

    [SerializeField, NaughtyAttributes.ReadOnly]
    private float _nowGforce;

    public ExplosionPoolManager _explosionPoolManager {
        set; private get;
    }

    private Rigidbody _rigidbody;
    private float _offtimeValue; //�~�T�C���̎��Ԍv�Z�p
    private Vector3 _previousVelocity; //�O�̉����x

    private const float ONEG = 9.81f;  //1G�̉����x
    private const float MINIMUMALLOWEDVALUE = 0.05f;

    private float _inputCheckTimer;   // �^�C�}�[�v�Z�p

    public IObjectPool<EnemyMissile> ObjectPool {
        get; set;
    }

    #endregion

    #region ���\�b�h
    //-------------------------------objectpool�C���^�[�t�F�C�X�̏���--------------------------------
    /// <summary>
    /// ������
    /// </summary>
    public void Initialize() {
        _offtimeValue = _timer;
        _isOverGforce = false;
    }

    /// <summary>
    /// �v�[���ɖ߂�����
    /// </summary>
    public void ReturnToPool() {
        ObjectPool.Release(this);
    }

    //-------------------------------�~�T�C���̏���--------------------------------

    void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        if (_playerTarget == null) {
            Debug.LogError("�v���C���[�^�[�Q�b�g���A�^�b�`����Ă��܂���");
            return;
        }

        // �^�[�Q�b�g�̃A�N�e�B�u��false�̂Ƃ��Ԃ�
        if (_playerTarget.gameObject.activeSelf == false) {
            ReturnToPool();
        }

        // �^�C�}�[��0�ɂȂ�����v�[���ɕԂ�
        _offtimeValue = Mathf.Max(0, _offtimeValue - Time.fixedDeltaTime);
        if (_offtimeValue == 0) {
            ReturnToPool();
        }

        //PlayerIsMove();
        CalculationFlying();
    }

    private void CalculationFlying() {
        if (_isOverGforce) {
            //return;
        }

        // �v���C���[�ƃ~�T�C���̑��Α��x���l��
        Vector3 playerVelocity = _playerRigidbody.velocity;
        Vector3 missileToPlayer = _playerTarget.position - transform.position;
        float distanceToPlayer = missileToPlayer.magnitude;
        Debug.Log($"�~�T�C���ƃv���C���[�̋��� : {distanceToPlayer}");

        // �v���C���[�Ƃ̋����ɉ����đ��x�𒲐�
        float adjustedMissileSpeed = _missileSpeed;

        if (distanceToPlayer < _proximityThreshold) {
            adjustedMissileSpeed *= _nearMissSlowdown; // �߂Â����猸��
            Debug.Log($"�~�T�C�����x������: {adjustedMissileSpeed}");
        }

        // �~�T�C���̑O�i
        _rigidbody.velocity = transform.forward * adjustedMissileSpeed;

        // �~�T�C���̉�]���x����
        Vector3 directionToTarget = _playerTarget.position + (playerVelocity * Time.fixedDeltaTime) - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _maxTurnSpeed * Time.fixedDeltaTime);

        // G-force�v�Z
        Vector3 currentVelocity = _rigidbody.velocity;
        Vector3 acceleration = (currentVelocity - _previousVelocity) / Time.fixedDeltaTime;
        _previousVelocity = currentVelocity;
        float gForce = acceleration.magnitude / ONEG;
        _nowGforce = gForce;

        Debug.Log($"����G�̒l�� {gForce}");

        // G-force�������𒴂����ꍇ
        if (gForce > _maxHighAcceleration) {
            Debug.Log("��G�l�ɂ��ǔ��I��");
            _isOverGforce = true;
            return;
        }
        if (gForce > _maxAcceleration) {
            Debug.LogError($"�ő�G�l�𒴂��܂����B���݂�G�l�� {gForce}");
            return;
        }
    }

    private void PlayerIsMove() {
        float inputHorizontal = Input.GetAxis("Horizontal");
        float inputVertical = Input.GetAxis("Vertical");
        Debug.Log($"{inputHorizontal}  :{inputVertical}");

        // ���͂���莞�ԂȂ�������bool��ς���X�N���v�g
        if (Mathf.Abs(inputHorizontal) > MINIMUMALLOWEDVALUE || Mathf.Abs(inputVertical) > MINIMUMALLOWEDVALUE) {
            _isPlayerInputActive = true;
            _inputCheckTimer = 0f;
            IsPlayerActive = true;
        } else {
            _inputCheckTimer += Time.deltaTime;

            if (_inputCheckTimer >= _inputCheckDuration) {
                _isPlayerInputActive = false;
                IsPlayerActive = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        _debug.Add($"{other.gameObject.name}   :    {other.tag}  ");

        // �Փ˔��ʃ��W�b�N (�K�v�ɉ����Ēǉ�)
        // if (other.gameObject.CompareTag("Player")) {
        //     print("�v���C���[�ɏՓ�");
        //     gameObject.SetActive(false);
        // }
    }

    #endregion
}
