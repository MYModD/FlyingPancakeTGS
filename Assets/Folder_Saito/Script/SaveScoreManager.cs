// ---------------------------------------------------------
// SaveScoreManager.cs
//
// 作成日:
// 作成者:
// ---------------------------------------------------------
using UnityEngine;
using System.Collections;
using System.IO;
using System.Linq;

public class SaveScoreManager : MonoBehaviour {
    // 保存ファイルのパス
    string _filePath;
    // スコアデータを格納するオブジェクト
    SaveScoreData _save;

    // オブジェクトが有効化されたときに呼び出される
    void Awake() {
        // 保存ファイルのパスを設定
        _filePath = Application.persistentDataPath + "/" + ".savedata.json";
        // スコアデータオブジェクトを初期化
        _save = new SaveScoreData();
    }

    // スコアデータを保存する
    public void Save() {
        // スコアデータをJSON文字列にシリアライズ
        string json = JsonUtility.ToJson(_save);
        // JSON文字列をファイルに書き込む
        using (StreamWriter streamWriter = new StreamWriter(_filePath)) {
            streamWriter.Write(json);
        }
    }

    // スコアデータを読み込む
    public void Load() {
        // 保存ファイルが存在するか確認
        if (File.Exists(_filePath)) {
            // ファイルからJSON文字列を読み込む
            using (StreamReader streamReader = new StreamReader(_filePath)) {
                string data = streamReader.ReadToEnd();
                // JSON文字列をオブジェクトにデシリアライズ
                _save = JsonUtility.FromJson<SaveScoreData>(data);
            }
        }
    }
}