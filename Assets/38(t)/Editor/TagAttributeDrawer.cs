#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

/// <summary>
/// タグ名の専用UIを表示させるためのPropertyDrawer
/// </summary>

public class TagAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // 対象のプロパティが文字列かどうか
        if (property.propertyType != SerializedPropertyType.String)
        {
            EditorGUI.PropertyField(position, property, label);
            return;
        }

        // タグフィールドを表示
        var tag = EditorGUI.TagField(position, label, property.stringValue);

        // タグ名を反映
        property.stringValue = tag;
    }
}
#endif
