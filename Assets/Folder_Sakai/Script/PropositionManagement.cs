using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropositionManagement : MonoBehaviour
{
    #region 変数
    #endregion
    #region　メソッド
    #endregion
    #region 変数
    /// <summary>
    /// お題を表す列挙型
    /// </summary>
    public enum Proposition {
        
        DefeatTheEnemy1,        //敵を倒せ
        GoThroughTheGate2,     //ゲートをくぐれ
        DodgeTheMonsterTruck3, //モンスタートラックを避けろ
        CollectStars4,         //星を集めろ
        Overtake5,             //追い越せ
        DoNotCollide6          //ぶつかるな
    }

    private Proposition _proposition;

    [SerializeField] private OvertakeManager _overtakeManager;
    [SerializeField] private DefeatTheEnemyManager _defeatTheEnemyManager;
    [SerializeField] private GoThroughTheGateManager _goThroughTheGateManager;
    #endregion
    #region メソッド

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
