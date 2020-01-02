using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameObjectReference))]
[CanEditMultipleObjects]
public class GameObjectReferenceEditor : Editor
{
    private SerializedPropertyDictionary properties;

    private void OnEnable()
    {
        string[] propertyNames =
        {
            "referenceType", "gObject", "gObjectTag", "parentOrChild", "parentLevel", "childIndeces"
        };
        properties = new SerializedPropertyDictionary(serializedObject, propertyNames);
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(properties["referenceType"]);

        switch(properties["referenceType"].enumValueIndex)
        {
            case 0:
                break;
            case 1:
                EditorGUILayout.PropertyField(properties["gObject"]);
                break;
            case 2:
                EditorGUILayout.PropertyField(properties["gObjectTag"]);
                break;
            default:
                EditorGUILayout.PropertyField(properties["parentOrChild"]);

                if(properties["parentOrChild"].enumValueIndex == 0)
                {
                    EditorGUILayout.PropertyField(properties["parentLevel"]);
                }
                else
                {
                    EditorGUILayout.PropertyField(properties["childIndeces"]);
                }

                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
