using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_AK : MonoBehaviour
{
    //[SerializeField] GameObject particlePrefab; // パーティクルのプレハブ
    //[SerializeField] float particleLifetime = 2f; // パーティクルの寿命

    public float _moveSpeed = 5f; // 移動速度

    private void Update()
    {

        // オブジェクトを前方に移動させる
        transform.Translate(Vector3.forward * _moveSpeed * Time.deltaTime);
        // オブジェクトが通り過ぎた後、パーティクルを生成する

        //GenerateParticle(transform.position);

        if (Input.GetKeyDown(KeyCode.A))
        {
            
        }
            
    }

    // オブジェクトが通り過ぎるときに呼ばれるメソッド
    public void ObjectPassed()
    {
        
    }

    private void GenerateParticle(Vector3 position)
    {
        // パーティクルを生成して指定した位置に配置
       // GameObject particle = Instantiate(particlePrefab, position, Quaternion.identity);

        // パーティクルの寿命を設定
       // Destroy(particle, particleLifetime);
    }
}
