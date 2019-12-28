using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SuppressSimultaneousEnergyAbsorptions))]
[CanEditMultipleObjects]
public class SuppressSimultaneousEnergyAbsorptionsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        SerializedProperty currentProperty = serializedObject.FindProperty("relationship");
        EditorGUILayout.PropertyField(currentProperty);

        if(currentProperty.enumValueIndex == 0)
        {
            ManyToManyEditor();
        }
        else
        {
            ManyToOneEditor();
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void ManyToManyEditor()
    {
        SerializedProperty property = serializedObject.FindProperty("useChildren");
        EditorGUILayout.PropertyField(property);
        
        // If the script is not using the object's children to populate the fields, 
        // then put the list in the editor
        if(!property.boolValue)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("pairs"), true);
        }
    }

    private void ManyToOneEditor()
    {
        SerializedProperty property = serializedObject.FindProperty("useChildrenForStockpile");
        EditorGUILayout.PropertyField(property);

        // If the script is not using the object's children to populate the fields, 
        // then put the list in the editor
        if (!property.boolValue)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("singleStockpile"));
        }

        property = serializedObject.FindProperty("useChildrenForSockets");
        EditorGUILayout.PropertyField(property);

        // If the script is not using the object's children to populate the fields, 
        // then put the list in the editor
        if (!property.boolValue)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("energySockets"), true);
        }
    }
}
