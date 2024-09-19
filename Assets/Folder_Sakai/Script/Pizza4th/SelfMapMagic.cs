using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfMapMagic : MonoBehaviour
{
    [SerializeField] private GameObject[] _maps;
    [SerializeField] private Transform[] _mapMovementPoint;
    private int _mapIndex = 0;
    private int _mapMovementPointIndex = 0;

    public void MapMove() {

        // 対応するポイントへ移動させる
        _maps[_mapIndex].transform.position = _mapMovementPoint[_mapMovementPointIndex].position;
        print("移動したで");
        // 次のマップとポイントへインデックスを進める
        _mapIndex++;
        _mapMovementPointIndex++;

        // インデックスがリストの終わりに達したらループさせる
        if (_mapIndex >= _maps.Length) {
            _mapIndex = 0;
        }
    }
}
