using UnityEngine;
using System.Collections;

/*
 * CLASS SimpleShieldedHealth2D : SimpleHealth2D
 * ---------------------------------------------
 * Defines a type of simple health where the health is protected
 * from being depleted by an outer shield that regenerates over time
 * ---------------------------------------------
 */
 
public class ShieldedHealth2D : InvincibleHealth2D
{
    private int shield; // Current shield amount
    [SerializeField]
    private int maxShield;  // Max size of the shield
    [SerializeField]
    private bool fullHeal;  // If true, this script regenerates health and shields over time. If not, only regenerates the shield
    [SerializeField]
    private float regenerationRate; // Time between each time a health point is regenerated
    private State regenerated;  // Gives duration between regenerating the shield, and returns true within #duration seconds of the shield being regenerated
    [SerializeField]
    private float regenerationLag;  // Time after being damaged it takes to start regenerating health
    private State damaged;  // Gives duration for which object is considered to be "recently" damaged

    protected override void Start()
    {
        base.Start();
        shield = maxShield;
        //regenerated = State.Construct("Regenerated", gameObject);
        //damaged = State.Construct("Damaged", gameObject);
    }

    // If shields are up, deplete them. Otherwise, take direct damage to health
    new public /*override*/ void TakeDamage(DamageInfo info, DamageType type)
    {
        if (shield > 0)
        {
            shield -= info.strength;

            // Make sure shield doesn't go below zero
            shield = Mathf.Clamp(shield, 0, maxShield);
        }
        else
        {
            base.TakeDamage(info, type);
        }
        damaged.Activate(regenerationLag);
    }

    private void Update()
    {
        // If we haven't been damaged recently, haven't regenerated recently, and there is damage to heal, regenerate
        if(shield < maxShield && !regenerated && !damaged)
        {
            // If script is permitted to heal health and it isn't maxed out, increase it
            if(fullHeal && health < maxHealth)
            {
                health++;
            }
            // Otherwise, if shields aren't yet full, increase them
            else if (shield < maxShield)
            {
                shield++;
            }

            // Set recently regenerated to active
            regenerated.Activate(regenerationRate);
        }
    }
}
