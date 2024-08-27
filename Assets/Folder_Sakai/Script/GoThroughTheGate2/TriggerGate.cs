using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SakaiScript {

    public class TriggerGate : MonoBehaviour {
        [SerializeField, Tag] private string _gate;
        [SerializeField] private GoThroughTheGateManager _goThroughTheGateManager;

        private void OnTriggerEnter(Collider other) {

            if (other.gameObject.CompareTag(_gate)) {

                _goThroughTheGateManager.ScoreAddition();
            }
        }
    }

}

