using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MTTrigger : MonoBehaviour
{
    [SerializeField] private ExplosionPoolManager _explosionPoolManager;
    [SerializeField] private MTAppearanceManagement _mTAppearanceManagement;
    [SerializeField] private int _spawnPointNumber = default;
    private ControllerBuruBuru _controller;
    [SerializeField, Tag] private string _obstaclesTag;

    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag(_obstaclesTag)) {

            _mTAppearanceManagement.MTTriggerObstacles(_spawnPointNumber);
            _explosionPoolManager.StartExplosion(this.gameObject.transform);

            if (_controller == null) {
                _controller = ControllerBuruBuru.Instance;
            }
            _controller.StartVibration();
            this.gameObject.SetActive(false);
        }
    }
}
