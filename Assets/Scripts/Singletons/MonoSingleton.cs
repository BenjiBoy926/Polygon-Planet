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

    public static T BaseCreateInstance()
    {
        return BaseCreateInstance("MonoSingleton");
    }

    public static T BaseCreateInstance(string objName)
    {
        if(instance == null)
        {
            GameObject parentObj = new GameObject(objName);
            T component = parentObj.AddComponent<T>();
            DontDestroyOnLoad(parentObj);
            return component;
        }
        else
        {
            return instance;
        }
    }
}
