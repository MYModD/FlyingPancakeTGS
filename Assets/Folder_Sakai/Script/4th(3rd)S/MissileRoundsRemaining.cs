using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileRoundsRemaining : MonoBehaviour
{
    [SerializeField] private int _missileRoundsRemaining = 2;

    public void RoundsRemainingIncrease(int increaseValue) {

        _missileRoundsRemaining += increaseValue;
    }
}
