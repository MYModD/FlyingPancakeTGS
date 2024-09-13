using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class GameController3rd : MonoBehaviour
{
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
    [Range(0, 3f)]
    private float _missileFireInterval = 0.5f;

    [SerializeField, Header("�~�T�C���c�e��")]
    private int _missileRoundsRemaining = 2;


    private float _missileCooldownTimer; // �~�T�C�����˂܂ł̎c�莞��
    private float _bulletCooldownTimer;   // �e�۔��˂܂ł̎c�莞��

    #endregion

    #region ���\�b�h

    void Update() {
        print("�c�e��" + _missileRoundsRemaining);

        // �~�T�C�����˃N�[���^�C���̍X�V
        _missileCooldownTimer -= Time.deltaTime;

        // �~�T�C�����ˏ�������
        bool canFireMissile = (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Submit"))
                              && _missileCooldownTimer <= 0 && _missileRoundsRemaining >= 1;

        if (canFireMissile) {
            _missileRoundsRemaining--;
            print(_missileRoundsRemaining);
            _missilePoolManager.FireMissiles(_missileLaunchPosition);
            _missileCooldownTimer = _missileFireInterval;
            Debug.Log("�~�T�C������");
        }

    }

    public void RoundsRemainingIncrease(int increaseValue) {

        _missileRoundsRemaining += increaseValue;

        print("�c�e��" + _missileRoundsRemaining);
    }
    #endregion
}
