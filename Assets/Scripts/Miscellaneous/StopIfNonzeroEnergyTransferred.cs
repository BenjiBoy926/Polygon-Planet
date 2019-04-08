using UnityEngine;
using System.Collections;

// A type of script that only stops the kinematic mover if the given
// energy source actually transferred nonzero energy
public class StopIfNonzeroEnergyTransferred : StopOnEnergyTransferred
{
    protected override void Start()
    {
        source.energyTransferredEvent += Stop;
    }

    // Simulate the energy absorption on the energy source in the energy transfer event,
    // only call the stop method if the amount absorbed is non-zero
    protected override void Stop(EnergyTransferredEventData eventData)
    {
        int energyTransferred = eventData.socket.ProcessEnergy(eventData.source.energy);
        if(energyTransferred != 0)
        {
            base.Stop(eventData);
        }
    }
}
