using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaCoinInstance : MonoBehaviour {
    // �z�u�ʒu�̔z��
    public Transform[] _instantiatePosition;

    // �R�C���I�u�W�F�N�g�̔z��
    public GameObject[] _coin;

    // �R�C�������Ԋu
    public float _duration;

    // �^�C�}�[
    private float _calucurate;

    private void OnEnable() {
        _calucurate = _duration;
    }

    // Update is called once per frame
    void Update() {
        _calucurate -= Time.deltaTime; // �o�ߎ��Ԃ����炷
        if (_calucurate <= 0) {
            // �z��̃T�C�Y�ɉ����ă����_���Ȉʒu������
            int i = Random.Range(0, _instantiatePosition.Length);
            Debug.Log($"�z��ԍ��� {i}");

            // �R�C���̎�ނ������_���Ɍ���
            int j = Random.Range(0, _coin.Length);
            GameObject obj = Instantiate(_coin[j]);
            obj.transform.position = _instantiatePosition[i].position;

            // �^�C�}�[�����Z�b�g
            _calucurate = _duration;
        }

        // �C�ӂ̃L�[���͂ŏ������s�������ꍇ
        if (Input.GetKey(KeyCode.F) && Input.GetKeyDown(KeyCode.J)) {
            // �����ɏ�����ǉ�
            Debug.Log("F�L�[��J�L�[�������ɉ�����܂���");
        }
    }
}
