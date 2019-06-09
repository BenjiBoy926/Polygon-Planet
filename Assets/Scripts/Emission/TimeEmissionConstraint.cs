using UnityEngine;

/*
 * CLASS TimeEmissionConstraint
 * ----------------------------
 * Prevent the constrained emitter from emitting too frequently
 * ----------------------------
 */ 
public class TimeEmissionConstraint : EmitterConstraint
{
    [SerializeField]
    [Tooltip("Minimum time between each emission of the emitter")]
    private float emissionRate;
    [SerializeField]
    [Tooltip("Script helps manage timing for when the emitter recently emitted")]
    private State _recentlyEmitted;
    public State recentlyEmitted { get { return _recentlyEmitted; } }  // True if the emitter recently emitted

    /*
     * PRIVATE HELPERS
     */

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        emitter.emissionEvent.unityEvent.AddListener(ActivateOnEmit);
    }

    void ActivateOnEmit()
    {
        recentlyEmitted.Activate(emissionRate);
    }
    protected override bool Constraint()
    {
        return !recentlyEmitted;
    }
}
