using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class SerializedPropertyDictionary
{
    private Dictionary<string, SerializedProperty> dictionary = new Dictionary<string, SerializedProperty>();

    public SerializedProperty this[string relativeName]
    {
        get { return dictionary[relativeName]; }
    }

    public SerializedPropertyDictionary(SerializedObject serializedObject, string[] relativeNames)
    {
        foreach(string name in relativeNames)
        {
            Add(serializedObject, name);
        }
    }

    public void Add(SerializedObject serializedObject, string relativeName)
    {
        dictionary.Add(relativeName, serializedObject.FindProperty(relativeName));
    }
}
