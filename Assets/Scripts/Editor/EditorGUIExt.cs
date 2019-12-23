using UnityEngine;
using UnityEditor;
using System.Collections;

public static class EditorGUIExt
{
    public static void ListField(Rect position, SerializedProperty list)
    {
        for(int i = 0; i < list.arraySize; i++)
        {
            EditorGUI.PropertyField(position, list.GetArrayElementAtIndex(i));
        }
    }
}
