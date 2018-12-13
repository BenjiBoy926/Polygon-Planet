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
    protected ProjectileInfo _info;

    public ProjectileInfo info { get { return _info; } }
    public DamageType type { get { return _type; } }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable2D target = collision.GetComponent<IDamageable2D>();

        if (target != null)
        {
            target.TakeDamage(_info, _type);
        }
    }
}

public enum DamageType
{
    Precision,
    Explosive
}