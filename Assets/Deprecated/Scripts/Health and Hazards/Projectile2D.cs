using UnityEngine;
using UnityEngine.Events;

/*
 * CLASS Projectile2D
 * ------------------
 * Projectiles can hit and cancel out other projectiles
 * Projectiles have a persistence rating that determines how many other 
 * projectiles it can hit before disabling. Also, the persistence rating
 * depletes when the projectile deals damage to a damageable object
 * ------------------
 */

public class Projectile2D : Hazard2D
{
    [SerializeField]
    protected int maxPersistence;   
    protected int persistence;  // Once depleted, the bullet is disabled
    [SerializeField]
    protected bool projectileCancelImmune;    // If true, this projectile cancels other projectiles but cannot be cancelled by other projectiles
    [SerializeField]
    protected bool damageableCancelImmune;  // If true, this projectile will not deplete persistence when it deals damage to a damageable object
    private Projectile2D otherProjectile; // Reference to another projectile, if any, that this projectile hit
    private UnityAction projectileCancelledEvent;    // Event called when the projectile is cancelled out

    private void Start()
    {
        persistence = maxPersistence;
    }
    // Persistence of a projectile is always reset when re-enabled
    private void OnEnable()
    {
        persistence = maxPersistence;
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        // When the projectile damages an object, try to cancel it with the corresponding type
        if(recentlyDamaged != null)
        {
            TryCancelProjectile(1, ProjectileCancelType.Damageable);
        }

        // See if we struck a projectile
        otherProjectile = collision.GetComponent<Projectile2D>();
        if(otherProjectile != null)
        {
            // If this projectile is not immune to projectile cancelling, try to cancel the other projectile as an ordinary projectile
            if(!projectileCancelImmune)
            {
                otherProjectile.TryCancelProjectile(_info.strength, ProjectileCancelType.Projectile);
            }
            // If this projectile is immune to projectile cancelling, try to cancel the other projectile as an immune projectile
            else
            {
                otherProjectile.TryCancelProjectile(_info.strength, ProjectileCancelType.ImmuneProjectile);
            }
        }
    }

    // Enables a calling method to cancel this projectile. The calling method must specify
    // the type of object that is trying to cancel this projectile
    public void TryCancelProjectile(int otherStrength, ProjectileCancelType otherType)
    {
        // If this projectile hit a damageable object or another projectile...
        if(otherType == ProjectileCancelType.Projectile && !projectileCancelImmune ||
            otherType == ProjectileCancelType.Damageable && !damageableCancelImmune)
        {
            //...deplete persistence
            DepletePersistence(otherStrength);
        }
        // Otherwise, if this projectile hit a projectile that is immune to cancellation,
        // and this projectile is not also immune to cancellation...
        else if(!projectileCancelImmune)
        {
            //...immediately cancel this projectile
            CancelProjectile();
        }
    }

    // Deplete persistence and check to see if the projectile has now been cancelled
    private void DepletePersistence(int amount)
    {
        persistence -= amount;

        // If persistence is depleted, this projectile has been cancelled
        if(persistence <= 0)
        {
            CancelProjectile();
        }
    }
    // Called once the projectile has been successfully cancelled out
    protected virtual void CancelProjectile()
    {
        gameObject.SetActive(false);

        // Call the event
        if(projectileCancelledEvent != null)
        {
            projectileCancelledEvent();
        }
    }

    public void AddProjectileCancelledEvent(UnityAction method)
    {
        projectileCancelledEvent += method;
    }
    public void RemoveProjectileCancelledEvent(UnityAction method)
    {
        projectileCancelledEvent -= method;
    }
}

public enum ProjectileCancelType
{
    Projectile, // This projectile is trying to be cancelled by another projectile
    ImmuneProjectile,   // This projectile is trying to be cancelled by a projectile that cannot be cancelled
    Damageable  // This projectile is trying to be cancelled by hitting a damageable object
}
