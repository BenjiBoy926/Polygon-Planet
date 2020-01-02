using UnityEngine;

[System.Serializable]
public class WeightedObject
{
    [Tooltip("A Unity Object with the assigned weight")]
    public Object obj;
    [Tooltip("The weight associated with this object")]
    public int weight;

    public WeightedObject(Object theObject, int wt)
    {
        obj = theObject;
        weight = wt;
    }
}
