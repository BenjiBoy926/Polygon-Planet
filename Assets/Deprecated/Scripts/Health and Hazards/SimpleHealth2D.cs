using UnityEngine;
using UnityEngine.Events;
using System.Collections;

/*
 * CLASS SimpleHealth2D : MonoBehaviour
 * ------------------------------------
 * A type of health manager that keeps track of the health on an object
 * with only one hitbox and no unique weak spots
 * ------------------------------------
 */ 

public class SimpleHealth2D : MonoBehaviour, IDamageable2D, IDeathHandler, IHealable2D
{
    protected int health; // Current health
    [SerializeField]
    protected int maxHealth;
    [SerializeField]
    private Collider2D _hitBox; // Hit box on this health object

    private UnityAction deathEvent; // Multi-cast delegate calls all specified functions
    private UnityAction<DamageInfo, DamageType> damageTakenEvent;   // Called when the health object takes damage

    public Collider2D hitBox { get { return _hitBox; } }
    public bool isDead { get { return health <= 0; } }

    protected virtual void Start()
    {
        health = maxHealth;
    }

    // Deplete health by the strength of the damage info specified
    public virtual void TakeDamage (DamageInfo info, DamageType type)
    {
        health -= info.strength;

        // If there are methods in the event, invoke the event
        if(damageTakenEvent != null)
        {
            damageTakenEvent(info, type);
        }

        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        gameObject.SetActive(false);

        if(deathEvent != null)
        {
            deathEvent();
        }
    }

    public virtual void FullRestore()
    {
        health = maxHealth;
        gameObject.SetActive(true);
    }

    // Add/remove the specified method from the event
    public void AddDamageTakenEvent(UnityAction<DamageInfo, DamageType> method)
    {
        damageTakenEvent += method;
    }
    public void RemoveDamageTakenEvent(UnityAction<DamageInfo, DamageType> method)
    {
        damageTakenEvent -= method;
    }

    // Add the specified function to the death event
    public void AddDeathEvent (UnityAction action)
    {
        deathEvent += action;
    }
    public void RemoveDeathEvent(UnityAction action)
    {
        deathEvent -= action;
    }
}
