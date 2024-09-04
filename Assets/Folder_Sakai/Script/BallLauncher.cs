using UnityEngine;

public class BallLauncher : MonoBehaviour {
    [SerializeField] private GameObject _ballPrefab; // 発射する球のプレハブ
    [SerializeField] private float _launchForce = 10f; // 球に与える力

    // 球を発射するメソッド
    public void LaunchBall() {
        // 球のプレハブが設定されていない場合は何もしない
        if (_ballPrefab == null) {
            Debug.LogError("Ball prefab is not assigned.");
            return;
        }

        // 球を生成し、Rigidbodyを取得する
        GameObject ball = Instantiate(_ballPrefab, transform.position, transform.rotation);
        Rigidbody rb = ball.GetComponent<Rigidbody>();

        // Rigidbodyが設定されていない場合は何もしない
        if (rb == null) {
            Debug.LogError("Rigidbody component is missing on the ball prefab.");
            return;
        }

        // 球に力を加えて発射する
        rb.AddForce(transform.forward * _launchForce, ForceMode.Impulse);
    }
}
