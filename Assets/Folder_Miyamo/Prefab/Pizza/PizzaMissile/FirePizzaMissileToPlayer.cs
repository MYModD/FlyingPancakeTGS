using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePizzaMissileToPlayer : MonoBehaviour {
    [Header("�~�T�C���ݒ�")]
    [SerializeField]
    private PizzaMissile _pizzaMissileToPlayer;

    [Header("�^�[�Q�b�g")]
    [SerializeField]
    private Transform _player;

    [Header("���ˈʒu")]
    [SerializeField]
    private Transform[] _firePostion;
    [SerializeField]
    private Transform _pearentPostion;

    [Header("�G�t�F�N�g")]
    [SerializeField]
    private ExplosionPoolManager _explosionPoolManager;

    [Header("���ˊԊu")]
    [SerializeField]
    private float _fireInterval = 1.0f; // ���ˊԊu (�b)

    private float _timer = 0.0f;

    void Update() {
        _timer += Time.deltaTime;
        if (_timer >= _fireInterval) {
            _timer = 0.0f;

            // ���˂���~�T�C���̐�
            int numToFire = Random.Range(1, _firePostion.Length + 1);

            // �I���ς݂̃C���f�b�N�X���Ǘ����郊�X�g
            List<int> usedIndexes = new List<int>();

            for (int i = 0; i < numToFire; i++) {
                // ���I���̃C���f�b�N�X�������_���ɑI��
                int randomIndex;
                do {
                    randomIndex = Random.Range(0, _firePostion.Length);
                } while (usedIndexes.Contains(randomIndex));

                usedIndexes.Add(randomIndex);

                // �~�T�C���𐶐�
                PizzaMissile missile = Instantiate(_pizzaMissileToPlayer, _pearentPostion);
                missile._player = _player;
                missile._explosionPool = _explosionPoolManager;
                missile.gameObject.transform.SetPositionAndRotation(_firePostion[randomIndex].position, _firePostion[randomIndex].rotation);
                Debug.Log($"���ˈʒu�� : {_firePostion[randomIndex]}");
            }

            // ���̃��[�v�̂��߂Ƀ��X�g���N���A
            usedIndexes.Clear();
        }
    }
}
