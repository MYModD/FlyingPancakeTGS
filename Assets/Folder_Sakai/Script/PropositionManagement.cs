using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropositionManagement : MonoBehaviour
{
    #region �ϐ�
    #endregion
    #region�@���\�b�h
    #endregion
    #region �ϐ�
    /// <summary>
    /// �����\���񋓌^
    /// </summary>
    public enum Proposition {
        
        DefeatTheEnemy1,        //�G��|��
        GoThroughTheGate2,     //�Q�[�g��������
        DodgeTheMonsterTruck3, //�����X�^�[�g���b�N�������
        CollectStars4,         //�����W�߂�
        Overtake5,             //�ǂ��z��
        DoNotCollide6          //�Ԃ����
    }

    private Proposition _proposition;

    [SerializeField] private OvertakeManager _overtakeManager;
    [SerializeField] private DefeatTheEnemyManager _defeatTheEnemyManager;
    [SerializeField] private GoThroughTheGateManager _goThroughTheGateManager;
    #endregion
    #region ���\�b�h

    public void ChangeProposition(Proposition proposition) {

        _proposition = proposition;

        switch (_proposition) {

            case Proposition.DefeatTheEnemy1:
                _defeatTheEnemyManager.FirstGroupStartMoving();
                break;
            case Proposition.GoThroughTheGate2:
                _goThroughTheGateManager.GateActivation();
                break;
            case Proposition.DodgeTheMonsterTruck3:
                break;
            case Proposition.CollectStars4:
                break;
            case Proposition.Overtake5:
                _overtakeManager.StartMoving();
                break;
            case Proposition.DoNotCollide6:
                break;

        }
    }
    #endregion
}
