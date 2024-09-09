using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class TestWayPointsCount : MonoBehaviour {
    [Tag, Header("waypointのタグ")]
    [SerializeField]
    private string _wayPoint;

    [Header("waypointを通った回数")]
    [SerializeField]
    private  int _wayPointsCount;

    [SerializeField, Header("順位クラスマネージャー")]
    private PlayerRankManager _playerRankManager;

    [SerializeField]
    private ExplosionPoolManager _explosion;

    [SerializeField, Tag]
    private string _buildingTag;

    [SerializeField, Tag]
    private string _missileTag;

    [SerializeField, Tag]
    private string _enemyTag;

   

    

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        
        //print(this.gameObject.name + _wayPointsCount);      
    }


    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(_wayPoint)) {

            //print("Trigger" + this.gameObject.name + other.gameObject.name);
            _wayPointsCount++;
            _playerRankManager.UpdatePlayerWaypointCount(this.gameObject, _wayPointsCount);

        }

        if (this.gameObject.tag == null) {

            return;
        
        }
        
        
        if (other.CompareTag(_buildingTag) && this.gameObject.CompareTag(_enemyTag)) {

            _explosion.StartExplosion(this.transform);
            this.gameObject.SetActive(false);

        } else if (other.CompareTag(_missileTag)) {


           
        
        
        }



    }
}
