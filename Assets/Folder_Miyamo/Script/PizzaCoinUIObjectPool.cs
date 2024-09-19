using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaCoinUIObjectPool : PoolManager<PizzaCoinUI> {

    [SerializeField,Range(0,50f)]
    private float _widthRandom;

    [SerializeField, Range(0, 50f)]
    private float _scaleYrandom;


    protected override PizzaCoinUI Create() {
        PizzaCoinUI instance = Instantiate(_pooledPrefab, transform.position, Quaternion.identity, transform);
        instance.ObjectPool = _objectPool;

        Vector2 uiScale = new Vector2(100 - Random.Range(_widthRandom, _widthRandom), 100 - Random.Range(_scaleYrandom, _scaleYrandom));
        instance.GetComponent<RectTransform>().sizeDelta = uiScale;
        return instance;
    }


}
