using UnityEngine;

public class BallLauncher : MonoBehaviour {
    [SerializeField] private GameObject _ballPrefab; // ���˂��鋅�̃v���n�u
    [SerializeField] private float _launchForce = 10f; // ���ɗ^�����

    // ���𔭎˂��郁�\�b�h
    public void LaunchBall() {
        // ���̃v���n�u���ݒ肳��Ă��Ȃ��ꍇ�͉������Ȃ�
        if (_ballPrefab == null) {
            Debug.LogError("Ball prefab is not assigned.");
            return;
        }

        // ���𐶐����ARigidbody���擾����
        GameObject ball = Instantiate(_ballPrefab, transform.position, transform.rotation);
        Rigidbody rb = ball.GetComponent<Rigidbody>();

        // Rigidbody���ݒ肳��Ă��Ȃ��ꍇ�͉������Ȃ�
        if (rb == null) {
            Debug.LogError("Rigidbody component is missing on the ball prefab.");
            return;
        }

        // ���ɗ͂������Ĕ��˂���
        rb.AddForce(transform.forward * _launchForce, ForceMode.Impulse);
    }
}
