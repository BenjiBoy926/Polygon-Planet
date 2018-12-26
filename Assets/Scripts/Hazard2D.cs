using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * CLASS Hazard2D : MonoBehaviour
 * ------------------------------
 * Defines a type of object that deals damage to damageable objects entering its trigger
 * ------------------------------
 */ 

public class Hazard2D : MonoBehaviour
{
    [SerializeField]
    protected DamageType _type;
    [SerializeField]
    protected DamageInfo _info;
    protected IDamageable2D recentlyDamaged; // Script recently damaged by the hazard

    public DamageInfo info { get { return _info; } }
    public DamageType type { get { return _type; } }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        recentlyDamaged = collision.GetComponent<IDamageable2D>();

        if (recentlyDamaged != null)
        {
            recentlyDamaged.TakeDamage(_info, _type);
        }
    }
}

public enum DamageType
{
    Precision,
    Explosive
}