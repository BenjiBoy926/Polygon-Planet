using UnityEngine;
using System.Collections;

/*
 * CLASS Bullet2D : Hazard2D
 * -------------------------
 * Defines a type of hazard that could potentially pierce through many damageable objects
 * -------------------------
 */ 
 [RequireComponent(typeof(Mover2D))]
public class Bullet2D : Hazard2D
{
    [SerializeField]
    private bool piercing;  // True if the bullet can pierce through multiple damageable objects

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        // If an object was recently damaged by the on trigger enter...
        if (recentlyDamaged != null)
        {
            //...set active depending on if bullet pierces multiple damageables
            gameObject.SetActive(piercing);
        }
    }
}
