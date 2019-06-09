using UnityEngine;

/*
 * CLASS AmmoEmissionConstraint
 * ----------------------------
 * Applies a constraint to an emitter such that it cannot emit
 * while the given stockpile is empty. Also updates the stock 
 * on the stockpile each time the emitter emits
 * ----------------------------
 */ 

public class AmmoEmissionConstraint : EmitterConstraint
{
    [SerializeField]
    [Tooltip("Reference to a script with the ammo level")]
    private Stockpile ammo;
    [SerializeField]
    [Tooltip("Amount of ammo used each time the emitter emits")]
    private int consumptionPerEmission;
    [SerializeField]
    [Tooltip("If true, the emitter is allowed to emit while the stock is non-empty, even if ammo consumption exceeds current ammo")]
    private bool allowOverflow;

    /*
     * PRIVATE HELPERS
     */ 

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        emitter.emissionEvent += ConsumeOnEmit;
    }

    void ConsumeOnEmit(Vector2 aimVector)
    {
        ammo.ChangeStock(-consumptionPerEmission);
    }
    protected override bool Constraint()
    {
        if(allowOverflow)
        {
            return !ammo.empty;
        }
        else
        {
            return ammo.currentStock >= consumptionPerEmission;
        }
    }
}
