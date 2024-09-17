using System.Collections;
using UnityEngine;
using TMPro;

public class RainbowTextEffect : MonoBehaviour {
    // TextMeshProオブジェクトを指定
    [SerializeField] private TMP_Text _tmpText;
    [SerializeField] private float _rainbowSpeed = 5f;

    private void Start() {
        if (_tmpText == null) {
            _tmpText = GetComponent<TMP_Text>();
        }
        StartCoroutine(AnimateVertexColors());
    }

    // 頂点カラーを虹色でアニメーションさせる
    private IEnumerator AnimateVertexColors() {
        TMP_TextInfo textInfo = _tmpText.textInfo;

        while (true) {
            _tmpText.ForceMeshUpdate(); // メッシュを強制的に更新
            int characterCount = textInfo.characterCount;

            if (characterCount == 0) {
                yield return null;
                continue;
            }

            // 文字ごとに頂点カラーを変更
            for (int i = 0; i < characterCount; i++) {
                if (!textInfo.characterInfo[i].isVisible)
                    continue;

                // 各文字の頂点情報を取得
                int vertexIndex = textInfo.characterInfo[i].vertexIndex;
                int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;
                Color32[] vertexColors = textInfo.meshInfo[materialIndex].colors32;

                // 色を虹色に変化させる
                Color32 rainbowColor = GetRainbowColor(Time.time + i * 0.25f);
                vertexColors[vertexIndex + 0] = rainbowColor;
                vertexColors[vertexIndex + 1] = rainbowColor;
                vertexColors[vertexIndex + 2] = rainbowColor;
                vertexColors[vertexIndex + 3] = rainbowColor;
            }

            // メッシュ情報を更新
            for (int i = 0; i < textInfo.meshInfo.Length; i++) {
                textInfo.meshInfo[i].mesh.colors32 = textInfo.meshInfo[i].colors32;
                _tmpText.UpdateGeometry(textInfo.meshInfo[i].mesh, i);
            }

            yield return null;
        }
    }

    // 虹色を生成する関数
    private Color32 GetRainbowColor(float time) {
        float r = Mathf.Sin(time * _rainbowSpeed) * 0.5f + 0.5f;
        float g = Mathf.Sin(time * _rainbowSpeed + 2f) * 0.5f + 0.5f;
        float b = Mathf.Sin(time * _rainbowSpeed + 4f) * 0.5f + 0.5f;
        return new Color(r, g, b);
    }
}
