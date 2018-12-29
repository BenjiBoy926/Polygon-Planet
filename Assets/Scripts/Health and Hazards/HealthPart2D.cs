using UnityEngine;
using System.Collections;

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
    private int weakness;

    public Collider2D hitBox { get { return _hitBox; } }

    private void Start()
    {
        complex.RegisterDamageable(this);
    }

    // Increases damage using local weakness constant, if damage is precise
    public void TakeDamage (DamageInfo info, DamageType type)
    {
        int newDamage = info.strength;
        if (type == DamageType.Precision)
        {
            newDamage *= weakness;
        }
        complex.ScheduleDamage(new DamageInfo(newDamage, info.hitBox));
    }
}
