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
    private State hazardsImmunized; // If true, the energy socket cannot absorb negative energy
    private State healersImmunized; // If true, the energy socket cannot absorb positive energy

    private void Start()
    {
        hazardsImmunized = State.Construct(theLabel: "HazardsImmunized", obj: gameObject);
        healersImmunized = State.Construct(theLabel: "HealersImmunized", obj: gameObject);
    }

    public void AbsorbEnergy(Energy energy)
    {
        int adjustedPower;  // Power of the energy actually absorbed
        bool hazardous = hazards.Contains(energy.tag);
        bool healing = healers.Contains(energy.tag);
        // If the energy is hazardous and isn't immunized...
        // The energy is healing and isn't immunized...
        // and some event has been specified when the socket absorbs energy
        if(((hazardous && !hazardsImmunized) || 
            (healing && !healersImmunized)) && 
            energyAbsorbedEvent != null)
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
        }
    }
    // Cause the energy socket not to respond to hazards
    public void ImmunizeHazards()
    {
        hazardsImmunized.Lock(true);
    }
    public void ImmunizeHazards(float time)
    {
        hazardsImmunized.Activate(time);
    }
    public void ClearHazardImmunization()
    {
        hazardsImmunized.Unlock();
        hazardsImmunized.Deactivate();
    }
    // Cause the energy socket not to respond to healers
    public void ImmunizeHealers()
    {
        healersImmunized.Lock(true);
    }
    public void ImmunizeHealers(float time)
    {
        healersImmunized.Activate(time);
    }
    public void ClearHealerImmunization()
    {
        healersImmunized.Unlock();
        healersImmunized.Deactivate();
    }
    // Cause the energy socket not to respond to hazards or healers
    public void Immunize()
    {
        ImmunizeHazards();
        ImmunizeHealers();
    }
    public void Immunize(float time)
    {
        ImmunizeHazards(time);
        ImmunizeHealers(time);
    }
    public void ClearImmunization()
    {
        ClearHazardImmunization();
        ClearHealerImmunization();
    }
}
