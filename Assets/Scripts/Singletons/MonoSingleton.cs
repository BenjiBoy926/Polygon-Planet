using UnityEngine;
using System.Collections;

/*
 * CLASS MonoSingleton
 * -------------------
 * A type of singleton class that inherits from monobehaviour
 * Because of the MonoBehavior inheritance, the class is limited
 */ 

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    public static T instance { get { return _instance; } }

    public static void CreateInstance()
    {
        GameObject parentObj = new GameObject("MonoSingleton");
        CreateInstance(parentObj);
    }

    public static void CreateInstance(GameObject parentObj)
    {
        if (_instance == null)
        {
            _instance = parentObj.AddComponent<T>();
            Debug.Log("Created mono singleton on " + parentObj.name);
        }
    }
}
