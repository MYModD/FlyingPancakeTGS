using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestTrackingUI : MonoBehaviour {
    #region 変数+便利ボタン


    [SerializeField] private GameObject[] _enemyIncameraUI; // 視錐台内の敵のImage
    [SerializeField] private GameObject[] _enemyInCone; // 円錐内の敵のImage

    [SerializeField] private TestLockOnManager _lockOnManager;

    private Image[] _enemyInCameraImages;               //キャッシュ用の
                                                        //Imageコンポーネント
    private Image[] _enemyInConeImages;

    [SerializeField, Header("子のスケール変更の値")] private float _childrenCameraScale = 1f;


    [SerializeField, Button,] 

    /// <summary>
    /// uiのスケール変更
    /// </summary>
    private void ChengeChildrenIncameraScale() {

        for (int i = 0; i < _enemyIncameraUI.Length; i++) {
            _enemyIncameraUI[i].GetComponent<RectTransform>().localScale = new Vector3(_childrenCameraScale, _childrenCameraScale, _childrenCameraScale);
        }
        
    }


    [SerializeField, Header("子のスケール変更の値")] private float _childrenConeScale = 1f;
    [SerializeField, Button,]

    private void ChengeChildrenInConeScale() {

       
        for (int i = 0; i < _enemyInCone.Length; i++) {
            _enemyInCone[i].GetComponent<RectTransform>().localScale = new Vector3(_childrenConeScale, _childrenConeScale, _childrenConeScale);
        }

    }
    #endregion



    void Start() {
        _enemyInCameraImages = InitializeUIElements(_enemyIncameraUI);
        _enemyInConeImages = InitializeUIElements(_enemyInCone);
    }

    
    private void Update() {

        

        UpdateUIOutSidePositions(_lockOnManager._targetsInCamera, _enemyInCameraImages);
        UpdateUIOutSidePositions(_lockOnManager._targetsInCone, _enemyInConeImages);
    }

    /// <summary>
    /// UI内のImageコンポーネントを初期化し、キャッシュを作成
    /// </summary>
    private Image[] InitializeUIElements(GameObject[] uiElements) {
        Image[] images = new Image[uiElements.Length];
        for (int i = 0; i < uiElements.Length; i++) {
            images[i] = uiElements[i].GetComponent<Image>();
            images[i].enabled = false;
        }
        return images;
    }


    /// <summary>
    /// updateごとに
    /// </summary>
    private void UpdateUIOutSidePositions(List<Transform> targets, Image[] uiElements) {
        for (int i = 0; i < targets.Count; i++) {

            if (i < uiElements.Length) {
                Vector3 enemyScreenPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, targets[i].position);
                uiElements[i].transform.position = enemyScreenPosition;
                uiElements[i].enabled = true;
            }
        }

        // 余ったUI要素を非表示にする
        for (int i = targets.Count; i < uiElements.Length; i++) {
            uiElements[i].enabled = false;
        }


    }

    /*private void UpdateUIInsidePositions(MissileStuck[] missileStucks, Image[] uiElements) {

        for (int i = 0; i < missileStucks.Length; i++) {
            if (missileStucks[i]._enemyTarget != null) {

                Vector3 enemyScreenPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, missileStucks[i]._enemyTarget.transform.position);
                uiElements[i].transform.position = enemyScreenPosition;
                uiElements[i].enabled = true;


            } else {

                uiElements[i].enabled = false;

            }

        }

    }*/


}