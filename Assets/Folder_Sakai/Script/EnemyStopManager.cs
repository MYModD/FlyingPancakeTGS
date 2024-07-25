using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStopManager : MonoBehaviour
{
    bool _isTheStateStopped;

    public void EnemyMovementOrStopped(bool isTheStateStopped) {

        _isTheStateStopped = isTheStateStopped;

    }
}
