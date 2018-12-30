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

public class SimpleHealth2D : MonoBehaviour, IDamageable2D, IDeathHandler
{
    protected int health; // Current health
    [SerializeField]
    protected int maxHealth;
    private UnityAction deathEvent; // Multi-cast delegate calls all specified functions
    [SerializeField]
    private Collider2D _hitBox; // Hit box on this health object

    public Collider2D hitBox { get { return _hitBox; } }

    protected virtual void Start()
    {
        health = maxHealth;
    }
    protected virtual void OnEnable()
    {
        health = maxHealth;
    }

    // Deplete health by the strength of the damage info specified
    public virtual void TakeDamage (DamageInfo info, DamageType type)
    {
        health -= info.strength;
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
