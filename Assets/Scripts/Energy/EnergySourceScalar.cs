using UnityEngine;
using UnityEngine.Events;
using System.Collections;

/*
 * CLASS EnergySourceScalar
 * ------------------------
 * When an energy source comes into contact with this object,
 * the power of the energy source is scaled by the locally
 * defined value
 * ------------------------
 */ 

public class EnergySourceScalar : CollisionComponentProcessor<EnergySource>
{
    [SerializeField]
    private float scalar;
    // Event called when this object scales the power level 
    public event UnityAction<float> powerScaledEvent;

    // Scale the energy source's power and invoke the event
    protected override void ProcessComponent(EnergySource source)
    {
        source.ScalePowerLevel(scalar);
        if(powerScaledEvent != null)
        {
            powerScaledEvent(scalar);
        }
    }
}
