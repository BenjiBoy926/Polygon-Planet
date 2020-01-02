using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomPropertyDrawer(typeof(GameObjectRelative))]
public class GameObjectRelativeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, null, property);

        //EditorGUI.PropertyField(position, property.FindPropertyRelative("parentOrChild"));
        //EditorGUI.PropertyField(position, property.FindPropertyRelative("parentLevel"));
        EditorGUIExt.ListField(position, property.FindPropertyRelative("childIndeces"));

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label);
    }
}
