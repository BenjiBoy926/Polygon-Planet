using UnityEngine;

/*
 * CLASS TimeEmissionConstraint
 * ----------------------------
 * Prevent the constrained emitter from emitting too frequently
 * ----------------------------
 */ 
public class TimeEmissionConstraint : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Reference to the emitter to constrain")]
    private ConstrainedEmitter2D emitter;
    [SerializeField]
    [Tooltip("Minimum time between each emission of the emitter")]
    private float emissionRate;

    public State recentlyEmitted { get; private set; }  // True if the emitter recently emitted

    /*
     * PRIVATE HELPERS
     */

    // Use this for initialization
    void Start()
    {
        recentlyEmitted = State.Construct("EmitterTimeConstraint", gameObject);
        emitter.emissionEvent += ActivateOnEmit;
        emitter.AddConstraint(() => !recentlyEmitted);
    }

    void ActivateOnEmit(Vector2 aimVector)
    {
        recentlyEmitted.Activate(emissionRate);
    }
}
