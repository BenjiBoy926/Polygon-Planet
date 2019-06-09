using System;
using UnityEngine.Events;

[Serializable]
public class Event
{
    public event Action action;
    public UnityEvent unityEvent;

    public void Invoke()
    {
        if (action != null)
        {
            action.Invoke();
        }
        
        if (unityEvent != null)
        {
            unityEvent.Invoke();
        }
    }
}

[Serializable]
public class Event<T>
{
    public event Action<T> action;
    public UnityEvent unityEvent;

    public void Invoke(T arg)
    {
        if (action != null)
        {
            action.Invoke(arg);
        }
        
        if (unityEvent != null)
        {
            unityEvent.Invoke();
        }
    }
}
