using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaCoinInstance : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform[] _instantiatePostion;

    public GameObject _coin;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F) && Input.GetKeyDown(KeyCode.J)) {

            // 0~3
            int i = Random.Range(0, 4);
            Debug.Log($"îzóÒî‘çÜÇÕ{i}");

            GameObject obj =  Instantiate(_coin);
            obj.transform.position = _instantiatePostion[i].position;
        }
    }
}
