using UnityEngine;
using System.Collections;

/*
 * CLASS ProjectileCancelledEffector
 * ---------------------------------
 * Simple effector produces its effect at the position of the event object
 * when the projectile associated with the object is cancelled by another projectile
 * ---------------------------------
 */

public class ProjectileCancelledEffector : EventEffector<Projectile2D>
{
    // Subscribe to the projectile cancelled event
    protected override void Start()
    {
        base.Start();
        eventHandle.AddProjectileCancelledEvent(EnableEffectOnCancel);
    }

    private void EnableEffectOnCancel()
    {
        EnableEffect(eventObject.position);
    }
}
