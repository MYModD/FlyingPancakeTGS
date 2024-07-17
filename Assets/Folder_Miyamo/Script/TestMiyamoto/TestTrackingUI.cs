using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestTrackingUI : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemyIncameraUI; // ��������̓G��Image
    [SerializeField] private GameObject[] _enemyInCone; // �~�����̓G��Image

    public TestLockOnManager lockOnManager;

    private Image[] _enemyInCameraImages;               //�L���b�V���p��
                                                        //Image�R���|�[�l���g
    private Image[] _enemyInConeImages;

    void Start()
    {
        _enemyInCameraImages = InitializeUIElements(_enemyIncameraUI);
        _enemyInConeImages = InitializeUIElements(_enemyInCone);
    }

    void Update()
    {
        UpdateUIPositions(lockOnManager.targetsInCamera, _enemyInCameraImages);
        UpdateUIPositions(lockOnManager.targetsInCone, _enemyInConeImages);
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