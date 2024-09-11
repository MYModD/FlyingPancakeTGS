using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaCoinCount : MonoBehaviour
{
    [SerializeField, Tag]
    private string _pizzaTag;

    [SerializeField]
    private int _pizzaCount = 0;

    public EnemyMissile _missile;
    public TimeLimit _timelimit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) {

        Debug.Log($"‚Ô‚Â‚©‚Á‚½‚â‚Â : {other.gameObject.name}");

        if (other.CompareTag(_pizzaTag)) {

            _pizzaCount++;
            other.gameObject.SetActive(false);


            if (_pizzaCount >= 15) {

                // ‚±‚±‚É”j‰óˆ—‘‚­
                Destroy(GameObject.Find("PizzaMan"));
                _timelimit._limitTime = 0;


            }
        }
    }
}
