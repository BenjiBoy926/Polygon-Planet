using UnityEngine;
using System.Collections;

/*
 * CLASS MonoSingleton
 * -------------------
 * A type of singleton class that inherits from monobehaviour
 * Classes inheriting from this class should have a function
 * call this function with the [RuntimeInitializeOnLoadMethod]
 * attribute applied to it
 * -------------------
 */

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T instance { get; private set; }

    public static void BaseCreateInstance()
    {
        BaseCreateInstance("MonoSingleton");
    }

    public static void BaseCreateInstance(string objName)
    {
        if (instance == null)
        {
            GameObject parentObj = new GameObject(objName);
            instance = parentObj.AddComponent<T>();
            DontDestroyOnLoad(parentObj);
        }
    }
}
