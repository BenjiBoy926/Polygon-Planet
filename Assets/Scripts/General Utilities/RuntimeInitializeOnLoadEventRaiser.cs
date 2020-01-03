using UnityEngine;
using UnityEngine.Events;

public class RuntimeInitializeOnLoadEventRaiser
{
    public static UnityEvent onLoadEvent = new UnityEvent();

    [RuntimeInitializeOnLoadMethod]
    private static void InitializeOnLoadEvent()
    {
        onLoadEvent.Invoke();
    }
}
