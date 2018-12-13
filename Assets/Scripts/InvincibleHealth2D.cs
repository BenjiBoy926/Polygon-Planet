using UnityEngine;
using System.Collections;

/*
 * CLASS InvincibleShieldedHealth2D : SimpleHealth2D
 * -------------------------------------------------
 * A type of simple health that can become invulnerable for a set amount of time
 * -------------------------------------------------
 */

public class InvincibleHealth2D : SimpleHealth2D
{
    [SerializeField]
    private State invincible;   // Specifies the duration for which the health object is invincible

    // Only take damage if not invincible
    public override void TakeDamage(ProjectileInfo info, DamageType type)
    {
        if(!invincible)
        {
            base.TakeDamage(info, type);
        }
    }

    // Activate invincibility for the preset amount of time
    public void ActivateInvincibility()
    {
        invincible.Activate();
    }
    // Activate invincibility for a custom amount of time
    public void ActivateInvincibility(float customTime)
    {
        invincible.Activate(customTime);
    }
}
