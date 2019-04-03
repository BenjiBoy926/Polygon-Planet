using System;
using UnityEngine;

/*
 * CLASS PriotiryWrapper
 * ---------------------
 * Simple class pairs an object of any type 
 * with a priority number.  The higher the number,
 * the higher priority the item
 * ---------------------
 */ 
 [Serializable]
public class PriorityWrapper<Type> : IComparable<PriorityWrapper<Type>>, IEquatable<PriorityWrapper<Type>>
{
    [SerializeField]
    private Type _data;
    [SerializeField]
    private int _priority;

    public Type data { get { return _data; } }
    public int priority { get { return _priority; } }

    public PriorityWrapper(Type theData, int thePriority)
    {
        _data = theData;
        _priority = thePriority;
    }
    // Compare by priority so that higher priorities precede lower ones
    public int CompareTo(PriorityWrapper<Type> other)
    {
        if(other == this)
        {
            return 0;
        }
        return _priority - other._priority;
    }
    public bool Equals(PriorityWrapper<Type> other)
    {
        return _priority == other._priority;
    }
}
