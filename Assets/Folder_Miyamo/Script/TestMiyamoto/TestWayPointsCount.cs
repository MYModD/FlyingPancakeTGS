using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class TestWayPointsCount : MonoBehaviour {
    [Tag, Header("waypoint�̃^�O")]
    [SerializeField]
    private string _wayPoint;

    [Header("waypoint��ʂ�����"),ReadOnly]
    [SerializeField]
    private  int _wayPointsCount;

    [SerializeField, Header("���ʃN���X�}�l�[�W���[")]
    private PlayerRankManager _playerRankManager;

    [SerializeField]
    private ExplosionPoolManager _explosion;

    [SerializeField, Tag]
    private string _buildingTag;

    [SerializeField, Tag]
    private string _missileTag;

    [SerializeField, Tag]
    private string _enemyTag;

    [SerializeField]
    private GameObject _vacumeObject;

    

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }


    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(_wayPoint)) {

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


            GameObject game = Instantiate(_vacumeObject);
            game.transform.position = this.transform.position;
            game.GetComponent<VacumeToPlayer>()._player = GameObject.Find("Player(MovingObj)");//�������O�ς����玀��
            Debug.Log("������player");

        
        
        }



    }
}
