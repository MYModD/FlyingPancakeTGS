using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    [SerializeField] GameObject[] _effects;
    private Transform _pointOfOccurrence = default;
    private int _effectIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetPointOfOccurrence(Transform deathPoint)
    {
        _pointOfOccurrence = deathPoint;
        EffectActivation();
    }

    private void EffectActivation()
    {
        _effects[_effectIndex].transform.position = _pointOfOccurrence.position;
        _effects[_effectIndex].SetActive(true);
        _effectIndex++;
        if (_effectIndex == _effects.Length)
        {
            _effectIndex = 0;
        }
    }
}
