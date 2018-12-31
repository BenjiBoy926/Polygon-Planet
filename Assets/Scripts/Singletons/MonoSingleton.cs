using UnityEngine;
using System.Collections;

/*
 * CLASS MonoSingleton
 * -------------------
 * A type of singleton class that inherits from monobehaviour
 * Because of the MonoBehavior inheritance, the class is limited
 */ 

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    public static T instance { get { return _instance; } }

    protected static void BaseCreateInstance()
    {
        if (_instance == null)
        {
            GameObject parentObj = new GameObject("MonoSingleton");
            _instance = parentObj.AddComponent<T>();
            DontDestroyOnLoad(parentObj);
        }
    }
}
