using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MTTrigger : MonoBehaviour
{
    [SerializeField] private ExplosionPoolManager _explosionPoolManager;
    [SerializeField] private MTAppearanceManagement _mTAppearanceManagement;
    [SerializeField] private int _spawnPointNumber = default;
    [SerializeField, Tag] private string _obstaclesTag;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag(_obstaclesTag)) {

            _mTAppearanceManagement.MTTriggerObstacles(_spawnPointNumber);
            _explosionPoolManager.StartExplosion(this.gameObject.transform);
            this.gameObject.SetActive(false);
        }
    }
}
