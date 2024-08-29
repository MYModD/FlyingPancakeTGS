using NaughtyAttributes;
using UnityEngine;

public class GameController : MonoBehaviour {

    #region �ϐ�

    
    [Foldout("�~�T�C���W")]
    [SerializeField, Header("�~�T�C�����ˈʒu")]
    private Transform _fireMissilePosition;

    [Foldout("�~�T�C���W")]
    [SerializeField]
    private MissilePoolManager _missilePoolManager;

    [Foldout("�~�T�C���W")]
    [SerializeField, Header("�N�[���^�C��")]
    private float _missileCoolTime�@= 1f;

    [Foldout("���b�N�I���n")]
    [SerializeField]
    private TestLockOnManager _testLockOnManager;

    
    
    

    private float _missileCooldownTimer; // �^�C�}�[�v�Z�p


    #endregion



    #region ���\�b�h
    
    void Update() {


        // �~�T�C�����˂̃N�[���^�C���v�Z
        _missileCooldownTimer -= Time.deltaTime;

        // �X�y�[�X��������GamePad�̉��{�^���������Ă��Ă��N�[���^�C����0�ȉ��̂Ƃ�
        bool canFire = (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Submit"))
             && (_missileCooldownTimer <= 0);


        if (canFire)       
        {
            _missilePoolManager.FireMissiles(_fireMissilePosition);
            _missileCooldownTimer = _missileCoolTime;
            Debug.Log("�{�^��������");
        }

        
    }
    #endregion
}
