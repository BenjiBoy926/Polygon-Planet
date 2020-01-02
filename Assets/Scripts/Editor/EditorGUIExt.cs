using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public static class EditorGUIExt
{
    public static void ListField(Rect position, SerializedProperty list)
    {
        for(int i = 0; i < list.arraySize; i++)
        {
            EditorGUI.PropertyField(position, list.GetArrayElementAtIndex(i));
        }
    }

    public static void ResizeListToProperty<T>(List<T> original, SerializedProperty list)
    {
        if(list.isArray)
        {
            // If the list is too small, add default values onto the end of the list
            if(original.Count < list.arraySize)
            {
                int initialCount = original.Count;
                for(int i = 0; i < list.arraySize - initialCount; i++)
                {
                    original.Add(default(T));
                }
            }
            // If the list is too big, remove values off the end
            else if(original.Count > list.arraySize)
            {
                original.RemoveToEnd(list.arraySize - 1);
            }
        }
    }
}
