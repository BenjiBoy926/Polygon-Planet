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

public class OrderedUnityEventArg0
{
    [SerializeField]
    [Tooltip("A list of UnityEvents. The events are invoked in the order they appear in the list")]
    private List<UnityEvent> events;

    public void AddListener(UnityAction listener)
    {
        events[events.Count - 1].AddListener(listener);
    }
    public void AddListener(UnityAction listener, int order)
    {
        events[order].AddListener(listener);
    }

    public void Invoke()
    {
        foreach (UnityEvent e in events)
        {
            e.Invoke();
        }
    }
}

public class OrderedUnityEventArg1
{
    [System.Serializable]
    public class EventType : UnityEvent<object> { };
    [SerializeField]
    [Tooltip("A list of UnityEvents. The events are invoked in the order they appear in the list")]
    private List<EventType> events;

    public void AddListener(UnityAction<object> listener)
    {
        events[events.Count - 1].AddListener(listener);
    }
    public void AddListener(UnityAction<object> listener, int order)
    {
        events[order].AddListener(listener);
    }

    public void Invoke(object arg)
    {
        foreach (UnityEvent<object> e in events)
        {
            e.Invoke(arg);
        }
    }
}

public class OrderedUnityEventArg2
{
    [System.Serializable]
    public class EventType : UnityEvent<object, object> { };
    [SerializeField]
    [Tooltip("A list of UnityEvents. The events are invoked in the order they appear in the list")]
    private List<EventType> events;

    public void AddListener(UnityAction<object, object> listener)
    {
        events[events.Count - 1].AddListener(listener);
    }
    public void AddListener(UnityAction<object, object> listener, int order)
    {
        events[order].AddListener(listener);
    }

    public void Invoke(object arg1, object arg2)
    {
        foreach (UnityEvent<object, object> e in events)
        {
            e.Invoke(arg1, arg2);
        }
    }
}

