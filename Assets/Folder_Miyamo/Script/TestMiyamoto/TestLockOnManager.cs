using System.Collections.Generic;
using UnityEngine;
using Utils;

public class TestLockOnManager : MonoBehaviour
{


    [Header("カメラの視界に入っているターゲットのリスト")]
    //[HideInInspector]重くなる要因なのでコメントなくす
    public List<Transform> targetsInCamera = new List<Transform>();

    [Header("錐体内に入っているターゲットのリスト")]
    //[HideInInspector]重くなる要因なのでコメントなくす
    public List<Transform> targetsInCone = new List<Transform>();

    [Header("上のリストで一番距離が短いターゲット")]

    [SerializeField, Header("カメラ指定")]
    private Camera _camera;

    [SerializeField, Header("spherecastの半径")]
    private float _searchRadius = 95f;

    [SerializeField, Range(0f, 180f)]
    [Header("コーンの角度")]
    private float _coneAngle = 45f;

    [SerializeField]
    [Header("コーンの長さ、半径")]
    private float _coneRange;






    readonly private Vector3 DrawOrigin = new Vector3(90, 0, 0);    //コーンの円周を向けるためのやつ offset


    private Plane[] cameraPlanes;  //カメラの六面体座標

    private float updateInterval = 0.1f;  // 0.1秒ごとに更新
    private float lastUpdate = 0f;


    void Update()
    {



        if (Time.time - lastUpdate > updateInterval)
        {
            // ターゲットリストを更新
            UpdateTargets();

            //コーンがカメラから外れたらリストから削除する
            RemoveTargetInCone();


            DebugMatarialChange();
            lastUpdate = Time.time;

        }


    }

    // ターゲットリストを更新するメソッド
    private void UpdateTargets()
    {
        // カメラの視錐台平面を取得
        cameraPlanes = GeometryUtility.CalculateFrustumPlanes(_camera);

        targetsInCamera.Clear();
        targetsInCone.Clear();


        Collider[] hits = GetSphereOverlapHits();    //colliderが返り値

        foreach (Collider hit in hits)
        {
            // ヒットしたオブジェクトを処理
            ProcessHit(hit, cameraPlanes);

        }
    }

    /// <summary>
    /// 球状の範囲内のヒットしたコライダーを取得するメソッド
    /// </summary>
    private Collider[] GetSphereOverlapHits()
    {
        return Physics.OverlapSphere(
            _camera.transform.position,
            _searchRadius,
            LayerMask.GetMask("Enemy")                        //レイヤーマスクがenemyかつtagがenemyのとき
        );
    }



    /// <summary>
    /// ヒットしたオブジェクトを処理するメソッド  
    /// </summary>
    /// <param name="hit">コライダー型 オブジェクトを識別する</param>
    /// <param name="planes">カメラの図形をPlane型で表したもの</param>

    private void ProcessHit(Collider hit, Plane[] planes)
    {
        if (hit.CompareTag("Enemy"))
        {
            Transform target = hit.transform;
            Renderer renderer = target.GetComponent<Renderer>();

            if (renderer != null && IsInFrustum(renderer, planes) && hit.gameObject.activeSelf == true)         //&& renderer.GetComponent<hogehoge>.isdead == false
                                                                                                                //死んでなかったら追加
            {
                targetsInCamera.Add(target);              //カメラ範囲内のリストにいれる

                if (IsInCone(target) && hit.gameObject.activeSelf == true)
                {
                    if (!targetsInCone.Contains(target) )

                        targetsInCone.Add(target);            //コーン内のリストにいれる
                }
            }
        }
    }


    /// <summary>
    /// オブジェクトが視錐台内にあるかどうかを確認するメソッド
    /// </summary>
    private bool IsInFrustum(Renderer renderer, Plane[] planes)
    {
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);    //testPlanesAABBでカメラの形とcoliderのboundsで見えているかを判断する
    }




    /// <summary>
    /// オブジェクトが円錐内にあるかどうかを確認するメソッド
    /// </summary>
    /// <param name="target">確認するオブジェクトのTransform</param>
    /// <returns>オブジェクトが円錐内にある場合はtrue、それ以外の場合はfalse</returns>
    private bool IsInCone(Transform target)
    {

        Vector3 cameraPosition = _camera.transform.position;  // カメラの位置を取得
        Vector3 cameraForward = _camera.transform.forward;   // カメラの前方向ベクトルを取得

        //Debug.Log($"{cameraPosition}+{cameraForward}");      // デバッグ用にカメラの位置と方向をログに出力

        Vector3 toObject = target.position - cameraPosition; // カメラ位置からターゲット位置へのベクトルを計算

        // ターゲットまでの距離を計算
        float distanceToObject = toObject.magnitude;                     // ベクトルの長さ（距離）




        if (distanceToObject <= _coneRange)                           // ターゲットが検索半径内にあるかどうかを確認
        {
            Vector3 toObjectNormalized = toObject.normalized;                // ターゲットへのベクトルを正規化（方向のみを取得）

            float angle = Vector3.Angle(cameraForward, toObjectNormalized); // カメラの前方向とターゲットへの方向との角度を計算

            //Debug.Log(angle);
            return angle <= _coneAngle / 2;                                // 角度がコーンの半分の角度以下であればtrueを返す
        }

        // ターゲットが検索半径外にある場合はfalseを返す
        return false;
    }





    /// <summary>
    /// inconeにあるターゲットは発射しても消えないのでもう一回AABBで判定を取る
    /// </summary>
    private void RemoveTargetInCone()
    {
        //リストから削除するためのリスト  繰り返す中で配列エラーが起きる可能性がある
        List<Transform> targetsToRemove = new List<Transform>();


        foreach (Transform target in targetsInCone)
        {
            if (!GeometryUtility.TestPlanesAABB(cameraPlanes, target.GetComponent<Collider>().bounds))
            {
                targetsToRemove.Add(target);


            }
        }


        foreach (Transform target in targetsToRemove)
        {
            targetsInCone.Remove(target);
        }
    }


    /// <summary>
    /// デバッグ用のギズモを描画するメソッド unity側のメソッド
    /// </summary>
    void OnDrawGizmos()
    {
        if (_camera != null)
        {
            // 球状の範囲を描画
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(_camera.transform.position, _searchRadius);

            // コーン上の円周を描画
            Gizmos.color = Color.yellow;
            float coneAngleRad = Mathf.Deg2Rad * _coneAngle / 2;

            Vector3 coneBaseCenter = _camera.transform.position + _camera.transform.forward * _coneRange;

            var hoge = DrawOrigin + transform.rotation.eulerAngles;
            hoge.z = 0;

            GizmosExtensions.DrawWireCircle(coneBaseCenter, _coneRange * Mathf.Tan(coneAngleRad), 20, Quaternion.Euler(hoge));

            //Debug.LogError(_coneRange * Mathf.Sin(coneAngleRad));

            // コーンの範囲を描画
            Gizmos.color = Color.red;
            Vector3 forward = _camera.transform.forward * _coneRange;
            Vector3 rightBoundary = Quaternion.Euler(0, _coneAngle / 2, 0) * forward;
            Vector3 leftBoundary = Quaternion.Euler(0, -_coneAngle / 2, 0) * forward;

            Gizmos.DrawLine(_camera.transform.position, _camera.transform.position + forward);
            Gizmos.DrawLine(_camera.transform.position, _camera.transform.position + rightBoundary);
            Gizmos.DrawLine(_camera.transform.position, _camera.transform.position + leftBoundary);
        }
    }


    /// <summary>
    /// debug用にマテリアル変えるだけ いずれ消す
    /// </summary>
    private void DebugMatarialChange()
    {
        /*for (int i = 0; i < targetsInCamera.Count; i++)
        {
            targetsInCamera[i].GetComponent<MeshRenderer>().material.color = Color.blue;
        }
        for (int i = 0; i < targetsInCone.Count; i++)
        {
            targetsInCone[i].GetComponent<MeshRenderer>().material.color = Color.red;
        }

        */
    }


    /// <summary>
    /// coneRangeをspherecast以下にする制御スクリプト
    /// </summary>
    private void OnValidate()
    {
        if (_coneRange > _searchRadius)
        {
            _coneRange = _searchRadius;
        }
    }
}