using UnityEngine;
using System.Collections.Generic;

/*
 * CLASS EnergySocket
 * ------------------
 * An object that can absorb energy. Adds an interpretive layer
 * on top of the energy and decides how much energy is actually absorbed.
 * This allows energies to only identify themselves, while sockets
 * decide how they will respond to what types of energies
 * ------------------
 */ 

public class EnergySocket : MonoBehaviour
{
    [SerializeField]
    private List<Tag> hazards;  // Energy sources with this tag decrease energy
    [SerializeField]
    private List<Tag> healers;  // Energy sources with this tag increase energy
    [SerializeField]
    private List<EnergyIntakeInfo> intakeInfo;  // Pairs multiplier constant with energy type

    // Raised when energy is absorbed, passing in amount absorbed
    public event UnityAction<int> energyAbsorbedEvent;

    public void AbsorbEnergy(Energy energy)
    {
        int adjustedPower;  // Power of the energy actually absorbed
        bool hazardous = hazards.Contains(energy.tag);
        bool healing = healers.Contains(energy.tag);

        if((hazardous || healing) && energyAbsorbedEvent != null)
        {
            // Find an intake info whose type matches the source type
            EnergyIntakeInfo matchedInfo = intakeInfo.Find(x => x.type == energy.type);
            // If the socket does not have intake info specified for energy of this type...
            if (matchedInfo == null)
            {
                //...assign unmodified energy power
                adjustedPower = energy.power;
            }
            // If the socket has intake info specified for energy of this type...
            else
            {
                //...multiply energy power by intake info multiplier
                adjustedPower = Mathf.RoundToInt(energy.power * matchedInfo.multiplier);
            }
            // Force power to negative if the energy is classified as hazardous
            adjustedPower = Mathf.Abs(adjustedPower);
            if(hazardous)
            {
                adjustedPower *= -1;
            }
            // Raise the event with the calculated power absorbed
            energyAbsorbedEvent(adjustedPower);
            Debug.Log(transform.root.gameObject.name + " absorbs " + adjustedPower + " energy");
        }
    }
}
