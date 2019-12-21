using UnityEngine;
using UnityEngine.Events;

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
    private UnityAction<DamageInfo, DamageType> damageDealtEvent;   // Event called when the hazard deals its damage to a damageable object  

    public DamageInfo info { get { return _info; } }
    public DamageType type { get { return _type; } }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        recentlyDamaged = collision.GetComponent<IDamageable2D>();

        // If an object is collided with that is damageable, deal damage to it
        if (recentlyDamaged != null)
        {
            recentlyDamaged.TakeDamage(_info, _type);

            // If methods exist in the damage dealt event, call the event
            if(damageDealtEvent != null)
            {
                damageDealtEvent(_info, _type);
            }
        }
    }

    // Add a function reference to the damage dealt event
    public void AddDamageDealtEvent(UnityAction<DamageInfo, DamageType> method)
    {
        damageDealtEvent += method;
    }
    // Remove a function reference from the damage dealt event
    public void RemoveDamageDealthEvent(UnityAction<DamageInfo, DamageType> method)
    {
        damageDealtEvent -= method;
    }
}

public enum DamageType
{
    Precision,
    Explosive
}