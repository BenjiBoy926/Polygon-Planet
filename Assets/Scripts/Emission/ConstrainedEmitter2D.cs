using UnityEngine;
using System.Collections;

/*
 * CLASS ConstrainedEmitter2D
 * --------------------------
 * A type of emitter that holds an internal state indicator
 * of whether or not the emitter is ready to emit or not
 * --------------------------
 */ 

public class ConstrainedEmitter2D : Emitter2D
{
    [SerializeField]
    [Tooltip("Minimum time between each emission")]
    private float _emissionRate;
    [SerializeField]
    [Tooltip("If true, the emitter is ready to emit as soon as the scene loads")]
    private bool startReady;
    private State _recentlyEmitted;  // True if the emitter emitted within #emissionRate seconds

    public State recentlyEmitted { get { return _recentlyEmitted; } }
    public float emissionRate { get { return _emissionRate; } }

    // Construct the state object
    protected override void Start()
    {
        base.Start();
        _recentlyEmitted = State.Construct("RecentlyEmitted", gameObject);
        // If the emitter is not ready initially, activate recently emitted state
        if (!startReady)
        {
            _recentlyEmitted.Activate(_emissionRate);
        }
    }
    // Override prevents emissions if the emitter recently emitted 
    public override void Emit(Vector2 aimVector)
    {
        if(!_recentlyEmitted)
        {
            ForceEmit(aimVector);
        }
    }
    // Optionally force the emitter to emit regardless of
    // whether or not the emitter recently emitted
    public void ForceEmit(Vector2 aimVector)
    {
        base.Emit(aimVector);
        _recentlyEmitted.Activate(_emissionRate);
    }
}
