using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class LabelledComponentKeeper<T> where T : ILabelledComponent
{
    public List<T> components { get; private set; }

    public LabelledComponentKeeper(List<LabelledComponentID> labelledComponentIDs)
    {
        foreach (LabelledComponentID ids in labelledComponentIDs)
        {
            // Use utility to get all labelled components
            T[] array = LabelledComponentUtility.FindComponentsWithLabel<T>(ids.gameObjectTag, ids.labelledComponentTag);

            // Add all found components to the list
            foreach(T component in array)
            {
                components.Add(component);
            }
        }
    }
}
