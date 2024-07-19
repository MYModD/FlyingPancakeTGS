using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFireMissile : MonoBehaviour
{
    [SerializeField, Header("発射位置")]
    private  Transform _firePostion;
    [SerializeField,Header("ミサイルのプレハブ")]
    private  GameObject _missileObject;

    [SerializeField, Header("LockOnManger")]
    private TestLockOnManager _testLockOnManger;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
           
            //var hage = _testLockOnManger.targetsInCone;
            //foreach (var item in hage)
            //{
            //    var hoge = Instantiate(_missileObject, _firePostion);
            //    hoge.transform.parent = null;
            //    hoge.GetComponent<TestMissile>().target = item.transform;
            //    print($"発射+{_firePostion.position}");
            //}
            
        }
    }
}
