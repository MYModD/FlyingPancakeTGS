using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DebugLogExtentions 
{
    /// <summary>
    /// 色変更
    /// </summary>
    /// <param name="str"></param>
    /// <param name="color"></param>
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
    public static string SetLineBreakInEnd(this string str)
    {
        return $"{str}\n";
    }
}
