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
    private State _invincible = new State();   // Specifies the duration for which the health object is invincible
    public State invincible { get { return _invincible; } }

    // Only take damage if not invincible
    public override void TakeDamage(ProjectileInfo info, DamageType type)
    {
        if(!_invincible)
        {
            base.TakeDamage(info, type);
        }
    }
}
