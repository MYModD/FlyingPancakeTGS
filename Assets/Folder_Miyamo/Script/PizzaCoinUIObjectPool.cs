using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaCoinUIObjectPool : PoolManager<PizzaCoinUI> {


    protected override PizzaCoinUI Create() {
        PizzaCoinUI instance = Instantiate(_pooledPrefab, transform.position, Quaternion.identity, transform);
        instance.ObjectPool = _objectPool;
        return instance;
    }


}
