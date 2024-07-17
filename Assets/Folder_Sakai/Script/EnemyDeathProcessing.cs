using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathProcessing : MonoBehaviour
{
    [SerializeField] EffectsManager _effectsManager;

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
        if (other.gameObject.CompareTag("Missile"))
        {
            _effectsManager.GetPointOfOccurrence(this.gameObject.transform);
            this.gameObject.SetActive(false);
        }
    }
}
