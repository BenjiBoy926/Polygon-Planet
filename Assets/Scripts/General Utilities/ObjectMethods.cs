using UnityEngine;

public class ObjectMethods : MonoBehaviour
{
    new public void Destroy(Object obj)
    {
        Object.Destroy(obj);
    }
    new public void DestroyImmediate(Object obj)
    {
        Object.DestroyImmediate(obj);
    }
    new public void DontDestroyOnLoad(Object obj)
    {
        Object.DontDestroyOnLoad(obj);
    }
}
