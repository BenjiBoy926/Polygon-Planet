using UnityEditor;

[CustomEditor(typeof(OneTriggerPerLifetime))]
[CanEditMultipleObjects]
public class OneTriggerPerLifetimeEditor : Editor
{
    SerializedProperty useChildrenProperty;
    SerializedProperty callingBehavioursProperty;
    SerializedProperty trackedTags;

    private void OnEnable()
    {
        useChildrenProperty = serializedObject.FindProperty("useChildren");
        callingBehavioursProperty = serializedObject.FindProperty("_callingBehaviours");
        trackedTags = serializedObject.FindProperty("trackedTags");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(useChildrenProperty);
        if(!useChildrenProperty.boolValue)
        {
            EditorGUILayout.PropertyField(callingBehavioursProperty, true);
        }
        EditorGUILayout.PropertyField(trackedTags, true);

        serializedObject.ApplyModifiedProperties();
    }
}
