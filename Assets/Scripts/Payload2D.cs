using UnityEngine;
using System.Collections;

/*
 * CLASS Payload2D
 * ---------------
 * A type of bullet that releases an explosion as soon as it hits a damageable object
 * ---------------
 */ 
public class Payload2D : Bullet2D
{
    [SerializeField]
    private GameObject explosionPrefab; // Prefab of the object that creates the payload's explosion
    private Explosion2D explosion;  // Reference to the explosion released when the payload hits a damageable object

    // Payloads cannot pierce targets by default
    protected virtual void Start()
    {
        piercing = false;
        explosion = Instantiate(explosionPrefab).GetComponent<Explosion2D>();
        explosion.gameObject.SetActive(false);
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        // Release an explosion if a damageable was damaged
        if (recentlyDamaged != null)
        {
            explosion.ExplodeAtPoint(transform.position);
        }
    }
}
