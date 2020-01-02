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
    [Tooltip("Tier level to choose from")]
    public int tier;
    [Tooltip("Number of objects to choose from this tier")]
    public int quantity;
}

[System.Serializable]
public class TierSelectionLevel
{
    [Tooltip("List of tier selections for this level")]
    public List<TierSelection> tierSelections;
    [Min(1)]
    [Tooltip("Number of times that these selections are made")]
    public int duration;
}