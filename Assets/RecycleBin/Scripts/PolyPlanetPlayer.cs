using UnityEngine;
using System.Collections;

/*
 * CLASS PolyPlanetPlayer
 * ----------------------
 * Player object in Polygon Planet. The Player class ties together and integrates
 * the events of multiple classes to create the full functionality of the player in the game
 * It integrates two emitters - one for a rapid-fire gun and one for a charge up gun -
 * and a blowback mover
 * ----------------------
 */ 

public class PolyPlanetPlayer : MonoBehaviour
{
    [SerializeField]
    private MoveByInput2D mover;
    [SerializeField]
    private EmitByMouseInput2D rapidFireGun;
    [SerializeField]
    private EmitByMouseInput2D chargeGun;
    [SerializeField]
    private float chargeGunRecoil;  // Strength of the blowback caused by firing the charged gun
    [SerializeField]
    private float defaultMoveSpeed; // Move speed of the player while not firing any guns
    [SerializeField]
    private float firingMoveSpeed;  // Move speed of the player while firing the rapidfire gun

    private void Start()
    {
        // Add local methods to rapid fire gun's events
        //rapidFireGun.emitted.onStateDeactivated += OnRapidFireGunReady;
        //rapidFireGun.onEmittedEvent += OnRapidFireGunFired;

        // Add local methods to charge gun events
        //chargeGun.emitted.onStateDeactivated += OnChargeGunReady;
        //chargeGun.onEmittedEvent += OnChargeGunFired;

        // Prevent charge gun from being charged at the start
        //chargeGun.emitted.Activate();
        mover.speed = defaultMoveSpeed;
    }

    // Event called when the rapid fire gun is ready to fire
    private void OnRapidFireGunReady()
    {
        mover.speed = defaultMoveSpeed;
    }
    // Event called when the rapid fire gun is fired
    private void OnRapidFireGunFired(Vector2 shotAim)
    {
        //chargeGun.emitted.Activate();
        mover.speed = firingMoveSpeed;
    }
    // Lock the rapid fire gun when the charge gun is at full charge
    private void OnChargeGunReady()
    {
        //rapidFireGun.emitted.Lock(true);
    }
    // When charge shot is fired, blowback away from the aim
    // Activate state on the rapid fire gun to prevent it shooting simultaneously
    private void OnChargeGunFired(Vector2 shotAim)
    {
        //mover.ApplyForce(-shotAim, chargeGunRecoil);
        //rapidFireGun.emitted.Unlock();
        //rapidFireGun.emitted.Activate();
    }
}
