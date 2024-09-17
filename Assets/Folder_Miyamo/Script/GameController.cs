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

    [Foldout("�~�T�C������")]
    [SerializeField, Header("�~�T�C�����ˊԊu")]
    [Range(0,3f)]
    private float _missileFireInterval = 0.5f;

    

    private  float _missileCooldownTimer; // �~�T�C�����˂܂ł̎c�莞��
    private float _bulletCooldownTimer;   // �e�۔��˂܂ł̎c�莞��

    #endregion

    #region ���\�b�h

    void Update() {
        // �~�T�C�����˃N�[���^�C���̍X�V
        _missileCooldownTimer -= Time.deltaTime;

        // �~�T�C�����ˏ�������
        bool canFireMissile = (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Submit"))
                              && _missileCooldownTimer <= 0;

        if (canFireMissile) {
            _missilePoolManager.FireMissiles(_missileLaunchPosition);
            _missileCooldownTimer = _missileFireInterval;
            Debug.Log("�~�T�C������");
        }

    }

    public float CoolTime() {
        return _missileCooldownTimer;
    }
    #endregion
}