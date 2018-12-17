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
        IDamageable2D target = collision.GetComponent<IDamageable2D>();

        if (target != null)
        {
            target.TakeDamage(_info, _type);
            gameObject.SetActive(piercing);
            Debug.Log(gameObject.name + " deals its damage to " + collision.gameObject.name);
        }
    }
}
