using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathProcessing : MonoBehaviour
{
    [SerializeField] EffectsManager _effectsManager;
    [SerializeField] CountTheNumberOfDefeats _countTheNumberOfDefeats;

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
        
    }

    private void OnDisable() {

        _countTheNumberOfDefeats.AdditionOfNumberOfDefeats();
    }
}
