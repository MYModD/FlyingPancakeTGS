using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class DebugLogExtentions 
{
    #region 基本メソッド
/// <summary>
    /// 色変更
    /// </summary>>
    /// <returns></returns>
    public static string SetColor(this string str,Color color)
    {
        string colorHtmlString = ColorUtility.ToHtmlStringRGBA(color);
        
        return $"<color=#{colorHtmlString}>{str}</color>";
    }

    /// <summary>
    /// サイズ変更
    /// </summary>
    /// <param name="str"></param>
    /// <param name="sizeValue"></param>
    /// <returns></returns>
    public static string SetSize(this string str, int sizeValue)
    {
        return $"<size={sizeValue}>{str}</size>";
    }
    /// <summary>
    /// 太字
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string SetBold(this string str)
    {

        return $"<b>{str}</b>";
    }
    /// <summary>
    /// イタリック
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string SetItalic(this string str)
    {
        return $"<i>{str}</i>";
    }

    /// <summary>
    ///  改行
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string SetEnter(this string str)
    {
        return $"{str}\n";
    }
    #endregion

    #region 便利メソッド


   
    /// <summary>
    /// 警告 文字黄色
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string Warning(this string str)
    {
        string warningStr = str.SetColor(Color.yellow).SetBold().SetSize(15);
        return warningStr;
    }
    /// <summary>
    /// エラー 文字赤色
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string Error(this string str)
    {
        string errorStr = str.SetColor(Color.magenta).SetBold().SetSize(15);
        return errorStr;
    }




    /// <summary>
    /// ???
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>

    public static string Gaming(this string str)
    {
        // カラーの配列　見やすい色集めた
        Color[] colors = { Color.red, Color.yellow, Color.blue, Color.green, Color.magenta };

        // 扱いやすいようにリスト化
        List<Color> availableColors = colors.ToList();

        string gamingString = "";

        foreach (char c in str)
        {

            int randomIndex = Random.Range(0, availableColors.Count);
            Color randomColor = availableColors[randomIndex];

            availableColors.RemoveAt(randomIndex);

            //　色が被らないようにランダム化,都度消してなくなったら作り直す
            if (availableColors.Count == 0)
            {
                availableColors = colors.ToList();
            }

            // 文字化 カラー化　太文字化　サイズ変更
            gamingString += c.ToString().SetColor(randomColor).SetBold().SetSize(30);

        }

        return gamingString;
    }



    /// <summary>
    /// ???
    /// </summary>
    /// <returns></returns>
    public static string Rainbow(this string str)
    {
        float lerp = 0f;

        // 赤:0から青:240　までグラデーションするための値を文字数で割る
        // これで文字数が異なる場合でも同じグラデーションができる
        float addLerpTValue = 240f  / 360f / str.Length;


        string rainbowString = "";


        foreach (char c in str)
        {
            //HSVのほうがゲーミングしやすい 
            Color rainColor = Color.HSVToRGB(lerp, 1, 1);
            rainbowString += c.ToString().SetColor(rainColor).SetBold().SetSize(30);

            //加算
            lerp += addLerpTValue;

        }

        return rainbowString;

    }


    #endregion
}
