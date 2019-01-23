using UnityEngine;
using System.Collections;

/*
 * CLASS DamageTakenEffector : ParticleEffector
 * --------------------------------------------
 * A simple type of particle effector that activates its effect
 * at the position of a damageable object when it takes damage
 * --------------------------------------------
 */ 

public class DamageTakenEffector : EventEffector<IDamageable2D>
{
    // Get a damageable script on the object and subscribe to it's damage taken event
    protected override void Start()
    {
        base.Start();
        eventHandle.AddDamageTakenEvent(EnableEffectOnDamageTaken);
    }

    // Enable effect at damageable object's position
    private void EnableEffectOnDamageTaken(DamageInfo info, DamageType type)
    {
        EnableEffect(eventObject.position);
    }
}
