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
    private State _invincible;   // Specifies the duration for which the health object is invincible
    public State invincible { get { return _invincible; } }

    protected override void Start()
    {
        base.Start();
        _invincible = State.Construct(obj : gameObject);
    }

    // Only take damage if not invincible
    public /*override*/ void TakeDamage(DamageInfo info, DamageType type)
    {
        if(!_invincible)
        {
            //base.TakeDamage(info, type);
        }
    }
}
