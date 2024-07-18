using UnityEngine;

public class BulletPoolManager : PoolManager<Bullet>
{
       
    private int i = 30;
    private void Awake()
    {
        Initialize();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_objectPool == null)
            {
                return;
            }

            var bulletObject = _objectPool.Get();

            if (bulletObject == null)
            {
                return;
            }

            bulletObject.transform.position = transform.position;
            bulletObject.Initialize();
        }
    }
}
