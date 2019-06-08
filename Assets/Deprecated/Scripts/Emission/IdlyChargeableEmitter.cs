using UnityEngine;
using System.Collections;

/*
 * CLASS IdlyChargeableEmitter
 * ---------------------------
 * Ties two emitters - one to emit "rapid fire" objects and
 * the other to emit "charge" objects.  The charge emitter is
 * only ready to fire if the rapid fire emitter doesn't emit
 * for the time it takes the charge emitter to charge up
 * ---------------------------
 */ 

public class IdlyChargeableEmitter : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Emits the rapid fire shots")]
    private ConstrainedEmitter2D rapidFireEmitter;
    [SerializeField]
    [Tooltip("Emits the charged-up shots")]
    private ConstrainedEmitter2D chargedUpEmitter;
    private void Start()
    {
        rapidFireEmitter.emissionEvent += OnRapidFireEmitted;
        chargedUpEmitter.emissionEvent += OnChargedUpEmitted;
        //chargedUpEmitter.recentlyEmitted.stateDeactivatedEvent += OnChargedUpReady;
    }
    // Activate recently emitted on the charged emitter each time the rapid fire emitter emits
    private void OnRapidFireEmitted(Vector2 aim)
    {
        //chargedUpEmitter.recentlyEmitted.Activate(chargedUpEmitter.emissionRate);
    }
    // Lock the rapid fire emitter so it can't emit while the charged emitter is ready
    private void OnChargedUpReady()
    {
        //rapidFireEmitter.recentlyEmitted.Lock(true);
    }
    // Once the charge gun is fired, unlock the rapid fire emitter
    private void OnChargedUpEmitted(Vector2 aim)
    {
        //rapidFireEmitter.recentlyEmitted.Unlock();
        //rapidFireEmitter.recentlyEmitted.Activate(rapidFireEmitter.emissionRate);
    }
}
