using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(ManualTierSelector))]
[CanEditMultipleObjects]
public class ManualTierSelectorEditor : Editor
{
    private SerializedPropertyDictionary properties;
    private List<bool> tierSelectionFoldouts = new List<bool>();

    private void OnEnable()
    {
        string[] propertyNames =
        {
            "tierList", "tierSelections", "reference", "_tierSelectionEvent"
        };
        properties = new SerializedPropertyDictionary(serializedObject, propertyNames);
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        UpdateEditorLists();

        EditorGUILayout.PropertyField(properties["reference"]);
        EditorGUILayout.PropertyField(properties["tierList"], true);
        properties["tierSelections"].arraySize = EditorGUILayout.IntField("Total tier selections", properties["tierSelections"].arraySize);

        for (int i = 0; i < properties["tierSelections"].arraySize; i++)
        {
            // Setup a foldout for each level selection
            tierSelectionFoldouts[i] = EditorGUILayout.Foldout(tierSelectionFoldouts[i], "Level " + i + " selections");

            // Do this editor part only if the foldout is folded out
            if(tierSelectionFoldouts[i])
            {
                // Get current tier selection
                SerializedProperty currentTierSelection = properties["tierSelections"].GetArrayElementAtIndex(i);
                SerializedProperty currentTierSelectionQuantities = currentTierSelection.FindPropertyRelative("tierSelectionQuantities");

                CheckTierSelection(currentTierSelection);

                // Produce a selection quantity for each tier
                for (int j = 0; j < currentTierSelectionQuantities.arraySize; j++)
                {
                    EditorGUILayout.PropertyField(currentTierSelectionQuantities.GetArrayElementAtIndex(j));
                }
                EditorGUILayout.PropertyField(currentTierSelection.FindPropertyRelative("duration"));
            }
        }

        EditorGUILayout.PropertyField(properties["_tierSelectionEvent"]);

        serializedObject.ApplyModifiedProperties();
    }

    private void UpdateEditorLists()
    {
        EditorGUIExt.ResizeListToProperty(tierSelectionFoldouts, properties["tierList"].FindPropertyRelative("_tiers"));
    }
    // Ensure that the list of quantities to select from each tier
    // is equal in size to the number to tiers to select from
    private void CheckTierSelection(SerializedProperty tierSelection)
    {
        tierSelection.FindPropertyRelative("tierSelectionQuantities").arraySize = properties["tierList"].FindPropertyRelative("_tiers").arraySize;
    }
}
