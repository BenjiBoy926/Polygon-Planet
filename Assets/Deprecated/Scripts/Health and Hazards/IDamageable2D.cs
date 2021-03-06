﻿using UnityEngine;
using UnityEngine.Events;

public interface IDamageable2D
{
    Collider2D hitBox { get; }
    void TakeDamage(DamageInfo info, DamageType type);
    void AddDamageTakenEvent(UnityAction<DamageInfo, DamageType> method);
    void RemoveDamageTakenEvent(UnityAction<DamageInfo, DamageType> method);
}
