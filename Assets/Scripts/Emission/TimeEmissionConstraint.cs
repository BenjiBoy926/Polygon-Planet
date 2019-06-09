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

    public State recentlyEmitted { get; private set; }  // True if the emitter recently emitted

    /*
     * PRIVATE HELPERS
     */

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        recentlyEmitted = State.Construct("EmitterTimeConstraint", gameObject);
        emitter.emissionEvent += ActivateOnEmit;
    }

    void ActivateOnEmit(Vector2 aimVector)
    {
        recentlyEmitted.Activate(emissionRate);
    }
    protected override bool Constraint()
    {
        return !recentlyEmitted;
    }
}
