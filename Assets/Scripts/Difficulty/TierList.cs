using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TierList
{
    [SerializeField]
    [Tooltip("A list where each element is one level in the tier list")]
    private List<Tier> _tiers;
    public List<Tier> tiers { get { return _tiers; } }

    public List<ComponentTier<T>> ComponentTierList<T>() where T : Component
    {
        // The tier list as transcribed into a tier list of components
        List<ComponentTier<T>> componentTiers = new List<ComponentTier<T>>();
        T currentComponent;

        // Loop through each tier
        foreach(Tier t in _tiers)
        {
            componentTiers.Add(new ComponentTier<T>());

            // Loop through each object in the tier
            foreach(Object o in t.list)
            {
                // If the current object is a game object, try to get the desired component
                // If the desired component exists, add it to the current component tier
                if(o.GetType() == typeof(GameObject))
                {
                    currentComponent = ((GameObject)o).GetComponent<T>();

                    if(currentComponent != null)
                    {
                        componentTiers[componentTiers.Count - 1].list.Add(currentComponent);
                    }
                }
            }
        }

        return componentTiers;
    }
}
