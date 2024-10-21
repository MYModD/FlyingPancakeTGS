using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathProcessing : MonoBehaviour {
    //�|�����G�v�Z�X�N���v�g
    [SerializeField] private CountTheNumberOfDefeats _countTheNumberOfDefeats;
    //�~�T�C���̃^�O
    [SerializeField, Tag] private string _missileTag;
    //�_���[�W�A�j���[�V����������X�N���v�g
    [SerializeField] private Planeee _planeee;
    //�d��
    [SerializeField] private Rigidbody _rigidbody;
    //�Ԃ�Ԃ�
    private ControllerBuruBuru _controller;
    //����ł邩
    private bool _isDeath = true;


    private void OnTriggerEnter(Collider other) {
        if (_controller == null) {
            _controller = ControllerBuruBuru.Instance;
        }
        if (other.gameObject.CompareTag(_missileTag) && _isDeath) {
            //����ł����Ԃ�
            _planeee.hp = -100;
            //���񂾂��Ƃ��
            _countTheNumberOfDefeats.AdditionOfNumberOfDefeats();
            //�Ԃ�Ԃ�
            _controller.StartVibration();
            //�d�͋N��
            _rigidbody.useGravity = true;
            //�e�q�̉���؂�
            this.gameObject.transform.parent = null;
        }
    }
}