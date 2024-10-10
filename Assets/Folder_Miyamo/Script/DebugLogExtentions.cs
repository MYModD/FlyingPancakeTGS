using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DebugLogExtentions 
{
    /// <summary>
    /// �F�ύX
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
    /// �T�C�Y�ύX
    /// </summary>
    /// <param name="str"></param>
    /// <param name="sizeValue"></param>
    /// <returns></returns>
    public static string SetSize(this string str, int sizeValue)
    {

        return $"<size={sizeValue}>{str}</size>";
    }
    /// <summary>
    /// ����
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>

    public static string SetBold(this string str)
    {

        return $"<b>{str}</b>";
    }
    /// <summary>
    /// �C�^���b�N
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string SetItalic(this string str)
    {
        return $"<i>{str}</i>";
    }

    /// <summary>
    ///  ���s
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string SetLineBreakInEnd(this string str)
    {
        return $"{str}\n";
    }
}
