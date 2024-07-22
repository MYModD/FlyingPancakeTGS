using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteEnemyIndex : MonoBehaviour
{
    [SerializeField] private int _thisGameObjectIndex;

    public int SetIndex() {

        return _thisGameObjectIndex;
    }
}
