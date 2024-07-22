// ---------------------------------------------------------
// TankLookOnTarget.cs
//
// 作成日:
// 作成者:
// ---------------------------------------------------------
using UnityEngine;
using System.Collections;
public class TankLookOnTarget : MonoBehaviour
{
    #region 変数
    [SerializeField]
    private GameObject _target;

    [SerializeField] private GameObject _muzzle;
    [SerializeField] private GameObject _turret;
    #endregion
    #region プロパティ
    #endregion
    #region メソッド
    /// <summary>
    /// 初期化処理 使わないなら消す
    /// </summary>
    void Awake()
{
}
/// <summary>
/// 更新前処理
/// </summary>
void Start ()
{
}
/// <summary>
/// 更新処理
/// </summary>
void Update ()
{
        _muzzle.transform.LookAt(_target.transform);
        Vector3 targetEnemy = new Vector3(_target.transform.position.x,
            this.transform.position.y+1.5f, _target.transform.position.z);
        _turret.transform.LookAt(targetEnemy);
    }
#endregion
}