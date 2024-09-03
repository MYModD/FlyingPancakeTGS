using UnityEngine;

public class FogControl : MonoBehaviour {
    [SerializeField]
    private bool _enableFog = false;

    void Start() {
        // フォグの設定を適用
        SetFog(_enableFog);
    }

    public void SetFog(bool enable) {
        RenderSettings.fog = enable;
    }
}
