using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LabelledComponentKeeper))]
[CanEditMultipleObjects]
public class LabelledComponentKeeperEditor : Editor
{
    private SerializedPropertyDictionary properties;
    private bool labelledComponentIDFoldout;

    private void OnEnable()
    {
        string[] propertyNames =
        {
            "includeChildren", "idQuantity", "labelledComponentIDs"
        };
        properties = new SerializedPropertyDictionary(serializedObject, propertyNames);
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(properties["includeChildren"]);
        EditorGUILayout.PropertyField(properties["idQuantity"]);

        if(properties["idQuantity"].enumValueIndex == 0)
        {
            properties["labelledComponentIDs"].arraySize = 1;
            LayoutLabelledComponentID(properties["labelledComponentIDs"].GetArrayElementAtIndex(0));            
        }
        else
        {
            // Put a foldout for the group
            labelledComponentIDFoldout = EditorGUILayout.Foldout(labelledComponentIDFoldout, "Labelled component IDs");

            if(labelledComponentIDFoldout)
            {
                // layout property for the array size
                properties["labelledComponentIDs"].arraySize = EditorGUILayout.IntField("Total labelled component ids", properties["labelledComponentIDs"].arraySize);
                
                // Layout each labelled id
                for(int i = 0; i < properties["labelledComponentIDs"].arraySize; i++)
                {
                    EditorGUILayout.Space();
                    LayoutLabelledComponentID(properties["labelledComponentIDs"].GetArrayElementAtIndex(i));
                }
            }
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void LayoutLabelledComponentID(SerializedProperty id)
    {
        EditorGUILayout.PropertyField(id.FindPropertyRelative("_gameObjectTag"));
        EditorGUILayout.PropertyField(id.FindPropertyRelative("_labelledComponentTag"));
    }
}
