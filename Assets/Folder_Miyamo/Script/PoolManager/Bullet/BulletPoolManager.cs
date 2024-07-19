using UnityEngine;

public class BulletPoolManager : PoolManager<Bullet>
{
       
    
    private void Awake()
    {
        Initialize();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            

            var bulletObject = _objectPool.Get();
            bulletObject.GetComponent<Rigidbody>().AddForce(Vector3.forward, ForceMode.Acceleration);

            
        }
    }
}
