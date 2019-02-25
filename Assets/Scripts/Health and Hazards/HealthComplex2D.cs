using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System;

/*
 * CLASS HealthComplex2D : MonoBehaviour
 * -------------------------------------
 * Defines a health object capable of recieving damage inputs through an
 * aggregation of multiple damageable objects.  The script strictly
 * enforces a "one hazard, one hit" rule that prevents a single hazard
 * from dealing its damage to multiple damageable parts.
 * -------------------------------------
 */ 

public class HealthComplex2D : MonoBehaviour, IDeathHandler, IHealable2D
{
    protected int health;   // Current health
    [SerializeField]
    private int maxHealth;
    private UnityAction deathEvent; // Multi-cast delegate to functions that call when health is depleted
    private List<IDamageable2D> damageables = new List<IDamageable2D>();    
    private List<DamageInfo> scheduledDamage = new List<DamageInfo>();   // List of damage info scheduled to be taken by the health complex when the next frame is resolved
    private List<Collider2D> offenders = new List<Collider2D>();    // List of colliders belonging to each object that has hurt this complex in its lifetime

    public bool isDead { get { return health <= 0; } }

    private void Start()
    {
        health = maxHealth;
    }
    private void Update()
    {
        // Take the damage scheduled
        if(scheduledDamage.Count == 1)
        {
            TakeDamage(scheduledDamage[0]);
            scheduledDamage.RemoveAt(0);
        }
        // If there are many damages scheduled, 
        // we need the more performance-intensive function to sort out the damage
        else if(scheduledDamage.Count > 1)
        {
            ResolveDamage();
        }

        if(offenders.Count == 1)
        {
            // If the offender is no longer active, re-enable collisions with it
            if(!offenders[0].gameObject.activeInHierarchy)
            {
                MuteCollider(offenders[0], false);
            }
        }
        else if(offenders.Count > 1)
        {
            int index = 0;

            // Loop through the list of offenders
            while(index < offenders.Count)
            {
                // If the current offender is no longer active, re-enable collisions with it
                if(!offenders[index].gameObject.activeInHierarchy)
                {
                    MuteCollider(offenders[index], false);
                }
                else
                {
                    index++;
                }
            }
        }
    }

    // Deplete health. Invoke death event if health is depleted
    protected void TakeDamage(DamageInfo info)
    {
        health -= info.strength;
        MuteCollider(info.hitBox, true);

        if (health <= 0)
        {
            Die();
        }
    }

    // Disable the object and call the death event
    public void Die()
    {
        gameObject.SetActive(false);

        if(deathEvent != null)
        {
            deathEvent();
        }
    }

    // Sets health back to max and reactivates the object
    public void FullRestore()
    {
        health = maxHealth;
        gameObject.SetActive(true);
    }

    // Add given damage info to the schedule
    public void ScheduleDamage(DamageInfo info)
    {
        scheduledDamage.Add(info);
    }
    // Cleans the schedule of repeated offenses from the same hazard,
    // then takes the damage specified
    private void ResolveDamage ()
    {
        int index = 0;

        // Clean the schedule by making sure only one instance of any given collider
        // exists only once in the list
        while (index < scheduledDamage.Count)
        {
            CleanSchedule(scheduledDamage[index].hitBox);
            ++index;
        }

        // Once cleaned, take all the damage scheduled
        foreach (DamageInfo damage in scheduledDamage)
        {
            TakeDamage(damage);
        }

        // Clear all damage scheduled
        scheduledDamage.Clear();
    }
    // Given a collider, the method searches the list of scheduled damage and removes
    // all but the most damaging info belonging to that collider
    private void CleanSchedule (Collider2D search)
    {
        // Predicate returns true if local collider matches the one on the current damage info
        Predicate<DamageInfo> MatchColliders = x => x.hitBox == search;
        // Find all scheduled damage instances that have a reference to the same collider passed in as a parameter
        List<DamageInfo> matches = scheduledDamage.FindAll(MatchColliders);

        // Enter selection if multiple damages have the same collider
        // (i.e. if multiple damages were scheduled by the same hazardous object on multiple damageable parts)
        if (matches.Count > 1)
        {
            // Sort the list from greatest to least damage, and remove the greatest damage
            matches.Sort();
            matches.RemoveAt(0);    // WARNING: can't remember if we're supposed to remove this one or the one at the END of the list...

            // Remove all but the highest damage info from scheduled damage
            foreach(DamageInfo info in matches)
            {
                scheduledDamage.Remove(info);
            }
        }
    }

    // Prevent the given collider from colliding with any damageable object associated with this script
    private void MuteCollider (Collider2D col, bool muted)
    {
        foreach (IDamageable2D part in damageables)
        {
            Physics2D.IgnoreCollision(part.hitBox, col, muted);
        }

        // If the collider is being ignored...
        if(muted)
        {
            //...add it to the list of offenders to check later
            offenders.Add(col);
        }
        // If this collider's collisions are being re-enabled...
        else
        {
            //...remove it from the list
            offenders.Remove(col);
        }
    }

    // Add given method to multi-cast delegate
    public void AddDeathEvent (UnityAction action)
    {
        deathEvent += action;
    }
    public void RemoveDeathEvent(UnityAction action)
    {
        deathEvent -= action;
    }
    // Add given damageable to list
    public void RegisterDamageable (IDamageable2D register)
    {
        damageables.Add(register);
    }
}
