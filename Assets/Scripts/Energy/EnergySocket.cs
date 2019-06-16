using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

/*
 * CLASS EnergySocket
 * ------------------
 * An object that can absorb energy. Adds an interpretive layer
 * on top of the energy and decides how much energy is actually absorbed.
 * This allows energies to only identify themselves, while sockets
 * decide how they will respond to different types of energies
 * ------------------
 */ 

public class EnergySocket : MonoBehaviour
{
    /*
     * PUBLIC TYPEDEFS
     */
    [System.Serializable] public class EnergyAbsorbedEvent : UnityEvent<EnergyAbsorbedEventData> { };

    /*
     * PUBLIC DATA
     */ 

    [SerializeField]
    [Tooltip("Energy sources with any of these tags cause the socket to absorb negative energy")]
    private List<Tag> hazards;
    [SerializeField]
    [Tooltip("Energy sources with any of these tags cause the socket to absorb positive energy")]
    private List<Tag> healers;
    [SerializeField]
    [Tooltip("Applies a multiplier to the energy absorbed by the given energy type")]
    private List<EnergyIntakeInfo> intakeInfo;
    [SerializeField]
    [Tooltip("Set of events invoked when the socket absorbs energy")]
    private EnergyAbsorbedEvent _energyAbsorbedEvent;
    public EnergyAbsorbedEvent energyAbsorbedEvent { get { return _energyAbsorbedEvent; } }

    [SerializeField]
    [Tooltip("State determines if the socket can absorb negative energy")]
    private State hazardsImmunized; // If true, the energy socket cannot absorb negative energy
    [SerializeField]
    [Tooltip("State determines if the socket can absorb positive energy")]
    private State healersImmunized; // If true, the energy socket cannot absorb positive energy

    // Process the energy and raise the event
    public int AbsorbEnergy(Energy energy)
    {
        int energyAbsorbed = ProcessEnergy(energy);
        _energyAbsorbedEvent.Invoke(new EnergyAbsorbedEventData(this, energy, energyAbsorbed));
        return energyAbsorbed;
    }
    // Determine how much energy is absorbed by the socket
    // based on its local fields
    private int ProcessEnergy(Energy energy)
    {
        int processedPower = 0; // Power of the energy when processed by the socket's fields

        // Check to see if the energy is hazardous or healthy
        bool hazardous = hazards.Contains(energy.tag);
        bool healing = healers.Contains(energy.tag);

        if ((hazardous && !hazardsImmunized) || (healing && !healersImmunized))
        {
            // Find an intake info whose type matches the source type
            EnergyIntakeInfo matchedInfo = intakeInfo.Find(x => x.type == energy.type);
            // If the socket does not have intake info specified for energy of this type...
            if (matchedInfo == null)
            {
                //...assign unmodified energy power
                processedPower = energy.power;
            }
            // If the socket has intake info specified for energy of this type...
            else
            {
                //...multiply energy power by intake info multiplier
                processedPower = Mathf.RoundToInt(energy.power * matchedInfo.multiplier);
            }
            // Force power to negative if the energy is classified as hazardous
            processedPower = Mathf.Abs(processedPower);
            if (hazardous)
            {
                processedPower *= -1;
            }
        }

        return processedPower;
    }

    // Set the multiplier for the given energy type
    // If no intake info exists with the given type, add one to the list
    public void SetIntakeMultiplier(EnergyType energyType, float multiplier)
    {
        EnergyIntakeInfo info = intakeInfo.Find(x => x.type == energyType);
        if (info != null)
        {
            info.multiplier = multiplier;
        }
        else
        {
            intakeInfo.Add(new EnergyIntakeInfo(energyType, multiplier));
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
