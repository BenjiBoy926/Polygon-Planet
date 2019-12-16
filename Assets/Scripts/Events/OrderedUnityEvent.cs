using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
 * Used so that the invokation order of listeners on a UnityEvent 
 * is well-defined. Has a list of UnityEvents such that events
 * earlier in the list are guaranteed to be invoked before the events
 * later in the list. Listeners in the same position in the list
 * have no well-defined invokation order
 */ 

public class OrderedUnityEvent
{
    [SerializeField]
    private List<UnityEvent> events;

    public void Invoke()
    {
        foreach(UnityEvent e in events)
        {
            e.Invoke();
        }
    }
}

public class OrderedUnityEvent<T>
{
    [SerializeField]
    private List<UnityEvent<T>> events;

    public void Invoke(T arg)
    {
        foreach (UnityEvent<T> e in events)
        {
            e.Invoke(arg);
        }
    }
}

public class OrderedUnityEvent<T1, T2>
{
    [SerializeField]
    private List<UnityEvent<T1, T2>> events;

    public void Invoke(T1 arg1, T2 arg2)
    {
        foreach (UnityEvent<T1, T2> e in events)
        {
            e.Invoke(arg1, arg2);
        }
    }
}

