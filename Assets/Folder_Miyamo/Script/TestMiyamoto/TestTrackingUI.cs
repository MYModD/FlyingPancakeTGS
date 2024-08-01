using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestTrackingUI : MonoBehaviour
{
    #region �ϐ�+�֗��{�^��

    
    [SerializeField] private GameObject[] _enemyIncameraUI; // ��������̓G��Image
    [SerializeField] private GameObject[] _enemyInCone; // �~�����̓G��Image

    public TestLockOnManager _lockOnManager;

    private Image[] _enemyInCameraImages;               //�L���b�V���p��
                                                        //Image�R���|�[�l���g
    private Image[] _enemyInConeImages;

    [SerializeField, Header("�q�̃X�P�[���ύX�̒l")] private float _childrenScale = 1f;
    [SerializeField, Button,]
    private void ChengeChildrenScale()
    {

        for (int i = 0; i < _enemyIncameraUI.Length; i++)
        {
            _enemyIncameraUI[i].GetComponent<RectTransform>().localScale = new Vector3(_childrenScale, _childrenScale, _childrenScale);
        }
        for (int i = 0; i < _enemyInCone.Length; i++)
        {
            _enemyInCone[i].GetComponent<RectTransform>().localScale = new Vector3(_childrenScale, _childrenScale, _childrenScale);
        }

    }
    #endregion
    void Start()
    {
        _enemyInCameraImages = InitializeUIElements(_enemyIncameraUI);
        _enemyInConeImages = InitializeUIElements(_enemyInCone);
    }

    void Update()
    {
        UpdateUIPositions(_lockOnManager._targetsInCamera, _enemyInCameraImages);
        UpdateUIPositions(_lockOnManager._targetsInCone, _enemyInConeImages);
    }

    /// <summary>
    /// UI����Image�R���|�[�l���g�����������A�L���b�V�����쐬
    /// </summary>
    private Image[] InitializeUIElements(GameObject[] uiElements)
    {
        Image[] images = new Image[uiElements.Length];
        for (int i = 0; i < uiElements.Length; i++)
        {
            images[i] = uiElements[i].GetComponent<Image>();
            images[i].enabled = false;
        }
        return images;
    }


    /// <summary>
    /// update���Ƃ�
    /// </summary>
    private void UpdateUIPositions(List<Transform> targets, Image[] uiElements)
    {
        for (int i = 0; i < targets.Count; i++)
        {
            if (i < uiElements.Length)
            {
                Vector3 enemyScreenPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, targets[i].position);
                uiElements[i].transform.position = enemyScreenPosition;
                uiElements[i].enabled = true;
            }
        }

        // �]����UI�v�f���\���ɂ���
        for (int i = targets.Count; i < uiElements.Length; i++)
        {
            uiElements[i].enabled = false;
        }
    }

    
}