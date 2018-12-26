using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable2D
{
    Collider2D hitBox { get; }
    void TakeDamage(DamageInfo info, DamageType type);
}
