using UnityEngine;

public class FogControl : MonoBehaviour {
    [SerializeField]
    private bool _enableFog = false;

    void Start() {
        // �t�H�O�̐ݒ��K�p
        SetFog(_enableFog);
    }

    public void SetFog(bool enable) {
        RenderSettings.fog = enable;
    }
}
