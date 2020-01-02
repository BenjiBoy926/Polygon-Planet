using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpawnerSpecifier2D))]
[CanEditMultipleObjects]
public class SpawnerSpecifier2DEditor : Editor
{
    private SerializedPropertyDictionary properties;

    private void OnEnable()
    {
        string[] propertyNames = { "original", "boundaryTag", "margin", "xPositionType", "xPosition", "yPositionType", "yPosition" };
        properties = new SerializedPropertyDictionary(serializedObject, propertyNames);
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(properties["original"]);

        if(NeedsBoundary())
        {
            EditorGUILayout.PropertyField(properties["boundaryTag"]);
            EditorGUILayout.PropertyField(properties["margin"]);
        }

        PositionPropertyField("xPositionType", "xPosition");
        PositionPropertyField("yPositionType", "yPosition");

        serializedObject.ApplyModifiedProperties();
    }

    private void PositionPropertyField(string positionType, string position)
    {
        EditorGUILayout.PropertyField(properties[positionType]);
        if(properties[positionType].enumValueIndex == 0)
        {
            EditorGUILayout.PropertyField(properties[position]);
        }
    }

    private bool NeedsBoundary()
    {
        return properties["xPositionType"].enumValueIndex != 0 || properties["yPositionType"].enumValueIndex != 0;
    }
}
