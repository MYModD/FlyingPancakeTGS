using NaughtyAttributes;
using UnityEngine;

public class GameController : MonoBehaviour {
    #region �ϐ�
    [Foldout("���b�N�I���n")]
    [SerializeField]
    private TestLockOnManager _testLockOnManager;

    [Foldout("�~�T�C������")]
    [SerializeField, Header("�~�T�C�����ˈʒu")]
    private Transform _missileLaunchPosition;

    [Foldout("�~�T�C������")]
    [SerializeField]
    private MissilePoolManager _missilePoolManager;


    [Header("4thMap��Memo")]
    [SerializeField]
    private Memo _memo;

    [Header("4th�̃~�T�C���X�s�[�h")]
    [SerializeField]
    private float _4thMissileSpeed;


    [Foldout("�~�T�C������")]
    [SerializeField, Header("�~�T�C�����ˊԊu")]
    [Range(0, 3f)]
    private float _missileFireInterval = 0.5f;

    [Header("�N�[���^�C���̊���"), ReadOnly]
    public float _coolTimeRatio;

    private float _missileCooldownTimer; // �~�T�C�����˂܂ł̎c�莞��
    private float _bulletCooldownTimer;   // �e�۔��˂܂ł̎c�莞��
    #endregion

    #region ���\�b�h
    void Update() {
        // �~�T�C�����˃N�[���^�C���̍X�V
        _missileCooldownTimer -= Time.deltaTime;
        _missileCooldownTimer = Mathf.Max(_missileCooldownTimer, 0f); // 0�����ɂȂ�Ȃ��悤�ɂ���


        // �~�T�C�����ˏ�������
        bool canFireMissile = (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Submit"))
                              && _missileCooldownTimer <= 0;

        if (canFireMissile) {

            if (_memo.gameObject.activeSelf == true) {

                // 4th�}�b�v �s�U���~�G����ON�̏ꍇ

                _missilePoolManager.FireMissilesSpeed4th(_missileLaunchPosition, _4thMissileSpeed);
                _missileCooldownTimer = _missileFireInterval;
                Debug.Log("4thVer�̃~�T�C������");



            } else {

                _missilePoolManager.FireMissiles(_missileLaunchPosition);
                _missileCooldownTimer = _missileFireInterval;
                Debug.Log("�~�T�C������");

            }






            
        }

        // �N�[���^�C���̊������v�Z�i0~1�͈̔͂Ɏ��߂�j
        _coolTimeRatio = 1f - Mathf.Clamp01(_missileCooldownTimer / _missileFireInterval);
    }

    public float CoolTime() {
        return _missileCooldownTimer;
    }
    #endregion
}