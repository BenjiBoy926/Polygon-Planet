using UnityEngine;
using System.Collections.Generic;

public class ChangeEnergySourceTag : CollisionComponentProcessor<EnergySource>
{
    /*
     * PUBLIC TYPEDEFS
     */ 
    [System.Serializable]
    public class TagPair
    {
        [Tooltip("Tag to convert from")]
        public Tag fromTag;
        [Tooltip("Tag to convert to")]
        public Tag toTag;
    }

    [SerializeField]
    [Tooltip("Energy sources with the tags given first " +
        "are changed to the tags given second")]
    private List<TagPair> tagPairs;
    private Dictionary<Tag, Tag> tagMap = new Dictionary<Tag, Tag>();

    private void Start()
    {
        foreach (TagPair pair in tagPairs)
        {
            tagMap.Add(pair.fromTag, pair.toTag);
        }
    }

    protected override void ProcessComponent(EnergySource component)
    {
        if (tagMap.ContainsKey(component.energy.tag))
        {
            component.SetTag(tagMap[component.energy.tag]);
        }
    }
}
