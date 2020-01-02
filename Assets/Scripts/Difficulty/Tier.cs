using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tier
{
    [Tooltip("The unity objects in this tier")]
    public List<Object> list;
}

public class ComponentTier<T> where T : Component
{
    public List<T> list = new List<T>();
}

[System.Serializable]
public class TierSelection
{
    [Tooltip("Quantity of objects to choose from each tier. " +
        "Element at 0 indicates the number of selections to make from tier 0")]
    public List<int> tierSelectionQuantities;
    [Min(1)]
    [Tooltip("Number of levels for which this tier selection is made")]
    public int duration;
}
