using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfMapMagic : MonoBehaviour
{
    [SerializeField] private GameObject[] _maps;
    [SerializeField] private Transform[] _mapMovementPoint;
    private int _mapIndex = 0;
    private int _mapMovementPointIndex = 0;

    public void MapMove() {

        // �Ή�����|�C���g�ֈړ�������
        _maps[_mapIndex].transform.position = _mapMovementPoint[_mapMovementPointIndex].position;
        print("�ړ�������");
        // ���̃}�b�v�ƃ|�C���g�փC���f�b�N�X��i�߂�
        _mapIndex++;
        _mapMovementPointIndex++;

        // �C���f�b�N�X�����X�g�̏I���ɒB�����烋�[�v������
        if (_mapIndex >= _maps.Length) {
            _mapIndex = 0;
        }
    }
}
