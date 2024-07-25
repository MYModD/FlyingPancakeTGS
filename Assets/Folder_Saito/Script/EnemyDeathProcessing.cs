using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathProcessing : MonoBehaviour
{
    [SerializeField] EffectsManager _effectsManager;
    [SerializeField]
    private GaugeManager _gaugeManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        _gaugeManager.SetGaugeValue(1f);
    }
}
