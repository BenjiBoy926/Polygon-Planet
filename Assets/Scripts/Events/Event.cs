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

[Serializable]
public class Event<T1, T2>
{
    public event Action<T1, T2> action;
    public UnityEvent unityEvent;

    public void Invoke(T1 arg1, T2 arg2)
    {
        if (action != null)
        {
            action.Invoke(arg1, arg2);
        }

        if (unityEvent != null)
        {
            unityEvent.Invoke();
        }
    }
}
