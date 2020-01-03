using UnityEngine;
using System.Collections.Generic;

public class LabelledComponentKeeper : MonoBehaviour
{
    [SerializeField]
    [Tooltip("If true, search the children of the game objects with the given game object tags")]
    private bool includeChildren;

    [SerializeField]
    [Tooltip("The number of labelled component IDs to specify")]
    private LabelIDQuantity idQuantity;

    [SerializeField]
    [Tooltip("The ids of the components to reference")]
    private List<LabelledComponentID> labelledComponentIDs;

    public T LabelledComponent<T>() where T : ILabelledComponent
    {
        return LabelledComponents<T>()[0];
    }
    public List<T> LabelledComponents<T>() where T : ILabelledComponent
    {
        return LabelledComponents<T>(labelledComponentIDs, includeChildren);
    }
    public static List<T> LabelledComponents<T>(List<LabelledComponentID> labelledComponentIDs, bool includeChildren = false)
        where T : ILabelledComponent
    {
        List<T> toReturn = new List<T>();
        foreach (LabelledComponentID id in labelledComponentIDs)
        {
            // Use utility to get all labelled components
            T[] array = LabelledComponentUtility.FindComponentsWithLabel<T>(id.gameObjectTag, id.labelledComponentTag, includeChildren);

            // Add all found components to the list
            foreach(T c in array)
            {
                toReturn.Add(c);
            }
        }
        return toReturn;
    }
}

public enum LabelIDQuantity
{
    Single, Multiple
}