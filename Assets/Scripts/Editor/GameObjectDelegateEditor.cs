using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameObjectDelegate))]
[CanEditMultipleObjects]
public class GameObjectDelegateEditor : Editor
{
    SerializedProperty parentOrChild;
    SerializedProperty parentLevel;
    SerializedProperty childIndeces;

    private void OnEnable()
    {
        parentOrChild = serializedObject.FindProperty("parentOrChild");
        parentLevel = serializedObject.FindProperty("parentLevel");
        childIndeces = serializedObject.FindProperty("childIndeces");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(parentOrChild);
        // Display parent level if parentOrChild is set to parent
        if(parentOrChild.enumValueIndex == 0)
        {
            EditorGUILayout.PropertyField(parentLevel);
        }
        // Display child indeces if parentOrChild is set to child
        else
        {
            EditorGUILayout.PropertyField(childIndeces, true);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
