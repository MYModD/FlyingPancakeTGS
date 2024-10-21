// ---------------------------------------------------------
// StarFalse.cs
//
// 作成日:
// 作成者:
// ---------------------------------------------------------
using UnityEngine;
using System.Collections;
public class StarFalse : MonoBehaviour
{
    //アニメーションでfalseさせたい
    public void FinishAnimete() {
        this.gameObject.SetActive(false);
    }
}