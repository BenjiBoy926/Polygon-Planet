using UnityEngine;
using System.Collections;

public class ChangeEnergySourceTagSimple : CollisionComponentProcessor<EnergySource>
{
    [SerializeField]
    [Tooltip("Any energy source that collides with this object is changed to this tag")]
    private Tag destinationTag;

    protected override void ProcessComponent(EnergySource component)
    {
        component.SetTag(destinationTag);
    }
}
