using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMovePoint : MonoBehaviour
{
    [SerializeField] private SelfMapMagic _selfMapMagic;
    [SerializeField, Tag] private string _playerTag;

    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag(_playerTag)) {
            print("MapMagicTrigger");
            _selfMapMagic.MapMove();
        }
    }
}
