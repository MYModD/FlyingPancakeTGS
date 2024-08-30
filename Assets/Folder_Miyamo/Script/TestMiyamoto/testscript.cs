using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class testscript : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _textMeshProUGUI;
    
    
    
    // Start is called before the first frame update
    void Start()
    {


        Camera[] allCameras = Camera.allCameras;
        foreach (Camera cam in allCameras) {
            if (cam.tag == "MainCamera") {
                Debug.Log($"Main Camera found: {cam.name} in {cam.gameObject.scene.name}");
                _textMeshProUGUI.text += $"Main Camera found: {cam.name} in {cam.gameObject.scene.name}";
            }
        }

        foreach (Camera camera in allCameras) {


            _textMeshProUGUI.text += $"{camera.name}aaaaaaaaaaaaaa";

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
