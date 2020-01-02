using System.Collections.Generic;
using UnityEngine;

public class ManualTierSelector : MonoBehaviour, ITierSelector
{
    [SerializeField]
    [Tooltip("The tier list to make selections from for every level")]
    private TierList tierList;
    [SerializeField]
    [Tooltip("Selections made from the given tier list for each level")]
    private List<TierSelection> tierSelections;
    [SerializeField]
    [Tooltip("Reference to a GameObject with a component of type IntComponent")]
    private GameObjectReference reference;
    [SerializeField]
    private ObjectListEvent _tierSelectionEvent;
    public ObjectListEvent tierSelectionEvent { get { return _tierSelectionEvent; } }
    
    public void TierSelection()
    {
        // Obtain the supplier script from the game object referenced
        IntComponent level = reference.gameObjectReferenced.GetComponent<IntComponent>();

        if(level >= 0 && level < tierSelections.Count)
        {
            // Get current level being used to select from the tier list
            List<Object> selected = new List<Object>();
            TierSelection currentSelection = GetCurrentSelection(level);

            for(int i = 0; i < currentSelection.tierSelectionQuantities.Count; i++)
            {
                // Add objects from the current tier to the list in the quantity specified
                for (int j = 0; j < currentSelection.tierSelectionQuantities[i]; j++)
                {
                    selected.Add(tierList.tiers[i].list.GetRandomItem());
                }
            }

            _tierSelectionEvent.Invoke(selected);
        }
    }

    private TierSelection GetCurrentSelection(int level)
    {
        return tierSelections[level];
    }
}
