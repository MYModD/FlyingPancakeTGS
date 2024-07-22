using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotationDetermination : MonoBehaviour
{
    [SerializeField,Tag] private string _eliteEnemy;
    [SerializeField] private bool _directionMaintenance;
    [SerializeField] private DirectionController _directionController;
    [SerializeField] private float _tiltValue;
    [SerializeField] private bool _shouldRotate;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag(_eliteEnemy)) {

            _directionController.SetMaintainDirection(_directionMaintenance,_tiltValue);
            //_directionController.SetRotate(_shouldRotate);
        }
    }
}
