using UnityEngine;
using System.Collections;

/*
 * CLASS DeathEffector
 * -------------------
 * A type of event effector where the event is the death of the object given
 * Produces effect at the object's position when it dies
 * -------------------
 */ 

public class DeathEffector : EventEffector<IDeathHandler>
{
    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        eventHandle.AddDeathEvent(EnableEffectOnDeath);
    }

    // Enable effect at the position of the death object
    private void EnableEffectOnDeath()
    {
        EnableEffect(eventObject.position);
    }
}
