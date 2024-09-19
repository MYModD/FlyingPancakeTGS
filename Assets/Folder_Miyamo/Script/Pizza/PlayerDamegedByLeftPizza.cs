using UnityEngine;

public class PlayerDamegedByLeftPizza : MonoBehaviour {
    [SerializeField]
    [Header("Player(MovingObj)")]
    public GameObject _playerMovingObj;

    [SerializeField]
    private PizzaWanima _pizzaLeftArm;

    [SerializeField]
    private PlayerMove _playerMove;

    public PizzaCoinCount _pizzaCount;

    [Range(0, 1000000)]
    [SerializeField]
    [Header("許されない距離")]
    private float _unacceptableDistance = 106448.5f;

    [SerializeField, Range(0, 3f)]
    [Header("減少する間隔")]
    public float _decreaseInterval = 1f; // 減少する間隔（秒）


    private float _lastDecreaseTime;


    void Start() {

    }

    // Update is called once per frame
    void Update() {
        float distance = Vector3.SqrMagnitude(_playerMovingObj.transform.position - _pizzaLeftArm.transform.position);
        //Debug.Log($"プレイヤーと左腕の距離{distance}");


        if (distance < _unacceptableDistance) {

            if (Time.time - _lastDecreaseTime >= _decreaseInterval) {


                bool playerisRight = _playerMove.transform.localPosition.x < 0f;
                //Debug.LogError(playerisRight);

                if (playerisRight) {

                    Debug.Log($"プレイヤーのローカルX   : {_playerMove.transform.localPosition.x}");
                    _pizzaCount.DegreePizzaCoinLeftArm();
                    _lastDecreaseTime = Time.time;


                }

            }

        }
    }
}

