using UnityEngine;
using System.Collections;

/*
 * CLASS EnergySourceScalar
 * ------------------------
 * When an energy source comes into contact with this object,
 * the power of the energy source is scaled by the locally
 * defined value
 * ------------------------
 */ 

public class EnergySourceScalar : MonoBehaviour
{
    [SerializeField]
    private float scalar;
    // Event called when this object scales the power level 
    public event UnityAction<float> powerScaledEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnergySource source = collision.GetComponent<EnergySource>();
        if(source != null)
        {
            ScaleEnergySourcePower(source);
        }
    }
    // Scale the energy source's power and invoke the event
    private void ScaleEnergySourcePower(EnergySource source)
    {
        source.ScalePowerLevel(scalar);
        if(powerScaledEvent != null)
        {
            powerScaledEvent(scalar);
        }
    }
}
