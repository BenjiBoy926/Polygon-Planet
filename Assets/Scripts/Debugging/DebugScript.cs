using UnityEngine;

public class DebugScript : MonoBehaviour
{
    public Object functor;
    public string methodName;

    public void Test()
    {
        System.Delegate.CreateDelegate(typeof(GameObject), functor, methodName);
    }
}
