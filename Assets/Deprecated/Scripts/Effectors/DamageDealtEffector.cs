using UnityEngine;
using System.Collections;

/*
 * CLASS DamageDealtEffector : ParticleEffector
 * --------------------------------------------
 * A type of particle effector that produces a particle effect 
 * at the position of the hazard as soon as it deals damage to
 * a damageable object
 * --------------------------------------------
 */ 

public class DamageDealtEffector : EventEffector<Hazard2D>
{
    // Subscribe to the damage dealt event
    protected override void Start()
    {
        base.Start();
        eventHandle.AddDamageDealtEvent(EnableEffectOnDamageDealt);
    }

    // A reference to this function is added to the hazards damage dealt event
    private void EnableEffectOnDamageDealt(DamageInfo info, DamageType type)
    {
        EnableEffect(eventObject.transform.position);
    }
}
