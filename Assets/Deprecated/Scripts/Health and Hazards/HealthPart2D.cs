using UnityEngine;
using UnityEngine.Events;

/*
 * CLASS HealthPart2D : MonoBehaviour
 * ----------------------------------
 * A single, damageable part in a health complex can have varying
 * levels of weakness, allowing the designer to craft a single 
 * health object with unique weak spots
 * ----------------------------------
 */ 

public class HealthPart2D : MonoBehaviour, IDamageable2D
{
    [SerializeField]
    private HealthComplex2D complex;
    [SerializeField]
    private Collider2D _hitBox;
    [SerializeField]
    private int weakness;   // Multiplied by damage from precision-type damage to get the damage dealt to the complex
    private UnityAction<DamageInfo, DamageType> damageTakenEvent; // Event called when the health part takes damage

    public Collider2D hitBox { get { return _hitBox; } }

    private void Start()
    {
        complex.RegisterDamageable(this);
    }

    // Increases damage using local weakness constant, if damage is precise
    public void TakeDamage (DamageInfo info, DamageType type)
    {
        // If the damage is precise, schedule damage multiplied by weakness
        if (type == DamageType.Precision)
        {
            complex.ScheduleDamage(new DamageInfo(info.strength * weakness, info.hitBox));
        }
        // Otherwise, schedule damage as is
        else
        {
            complex.ScheduleDamage(info);
        }

        // If a damage taken event has been specified, invoke the event
        if(damageTakenEvent != null)
        {
            damageTakenEvent(info, type);
        }
    }

    // Add a reference to a function to the event
    public void AddDamageTakenEvent(UnityAction<DamageInfo, DamageType> method)
    {
        damageTakenEvent += method;
    }
    // Remove a reference to a funciton from the event
    public void RemoveDamageTakenEvent(UnityAction<DamageInfo, DamageType> method)
    {
        damageTakenEvent -= method;
    }
}
