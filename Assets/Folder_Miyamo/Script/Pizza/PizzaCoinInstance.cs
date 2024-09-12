using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaCoinInstance : MonoBehaviour {
    // �z�u�ʒu�̔z��
    public Transform _instantiateLeftPosition;
    public Transform _instantiateRightPosition;

    // �R�C���I�u�W�F�N�g�̔z��
    [Header("�Z����ƒ��������")]
    public GameObject[] _leftCoin;

    [Header("�Z��������Ȃ���")]
    public GameObject _rightCoin;

    // �R�C�������Ԋu
    public float _durationTime;

    [Header("���~�G�����猩�č����o��m��  ��������70")]
    public float _leftProbability = 70;

    // �^�C�}�[
    private float _calucurate;

    private void OnEnable() {
        _calucurate = _durationTime;
    }

    // Update is called once per frame
    void Update() {
        _calucurate -= Time.deltaTime; // �o�ߎ��Ԃ����炷
        if (_calucurate <= 0) {


            int i = Random.Range(0, 101);
            Debug.Log($"�����_���l : {i}");
            // 7���̊m���ō��i���~�G�����猩��)�R�C�������������
            if (i < _leftProbability) {

                //���̏ꍇ
                bool random = Random.Range(0, 2) == 0;
                if (random) {
                    GameObject obj = Instantiate(_leftCoin[0]);
                    obj.transform.position = _instantiateLeftPosition.position;
                } else {
                    GameObject obj = Instantiate(_leftCoin[1]);
                    obj.transform.position = _instantiateLeftPosition.position;
                }




            } else {


                //�E�̏ꍇ
                GameObject obj = Instantiate(_rightCoin);
                obj.transform.position = _instantiateRightPosition.position;
            }


            // �^�C�}�[�����Z�b�g
            _calucurate = _durationTime;
        }

        // �C�ӂ̃L�[���͂ŏ������s�������ꍇ
        if (Input.GetKey(KeyCode.F) && Input.GetKeyDown(KeyCode.J)) {
            // �����ɏ�����ǉ�
            Debug.Log("F�L�[��J�L�[�������ɉ�����܂���");
        }
    }
}
