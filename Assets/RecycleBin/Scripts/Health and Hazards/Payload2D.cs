using UnityEngine;
using System.Collections;

/*
 * CLASS Payload2D
 * ---------------
 * A type of bullet that releases an explosion as soon as it is cancelled out,
 * either by hitting another stronger projectile or hitting a damageable object
 * ---------------
 */ 
public class Payload2D : Projectile2D
{
    [SerializeField]
    private GameObject explosionPrefab; // Prefab of the object that creates the payload's explosion
    private Explosion2D explosion;  // Reference to the explosion released when the payload hits a damageable object

    protected virtual void Start()
    {
        explosion = Instantiate(explosionPrefab).GetComponent<Explosion2D>();
        explosion.gameObject.SetActive(false);
    }

    // Override of the cancelled method of projectile releases explosion when cancelled
    protected override void CancelProjectile()
    {
        base.CancelProjectile();
        explosion.ExplodeAtPoint(transform.position);
    }
}
