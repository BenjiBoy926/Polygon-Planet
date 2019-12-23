using UnityEngine;
using UnityEditor;
using System.Collections;

public static class GUIContentExt
{
    public static GUIContent GUIContentProperty(SerializedProperty property)
    {
        return new GUIContent(property.name, property.tooltip);
    }
}
