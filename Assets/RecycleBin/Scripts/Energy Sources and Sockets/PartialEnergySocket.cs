using UnityEngine;
using System.Collections.Generic;

/*
 * CLASS PartialEnergySocket
 * -------------------------
 * An energy socket that absorbs energy but does not have a
 * standalone pool of energy to add to / detract from. Partial
 * Energy Sockets are composed by Complex Energy Sockets.
 * When energy is absorbed, the partial socket notifies the
 * complex socket, which does the hard work of resolving
 * multiple conflicting notifications from multiple partial
 * sockets
 * ------------------------
 */ 

public class PartialEnergySocket : AbstractEnergySocket, IEnergySocketComparativeEventHandler
{
    [SerializeField]
    private ComplexEnergySocket owner;
    [SerializeField]
    private List<EnergyIntakeInfo> intakeInfo;    // List of information of how much energy will be absorbed from an energy source depending on its type

    // Events
    public event UnityAction<Energy> energyIncreasedEvent;
    public event UnityAction<Energy> energyDecreasedEvent;

    private void Start()
    {
        owner.RegisterSocket(this);
    }

    // When energy is absorbed, schedule energy delta on the complex energy socket
    public override void AbsorbEnergy(Energy sourceInfo)
    {
        bool hazardous = hazards.Contains(sourceInfo.tag);
        bool healing = healers.Contains(sourceInfo.tag);

        if (hazards.Contains(sourceInfo.tag))
        {
            owner.ScheduleEnergyDecrease(sourceInfo);
            if(energyDecreasedEvent != null)
            {
                energyDecreasedEvent(sourceInfo);
            }
            Debug.Log("Scheduled energy depletion by " + sourceInfo.power + " points on complex " + owner.gameObject.name);
        }
        else if(healers.Contains(sourceInfo.tag))
        {
            owner.ScheduleEnergyIncrease(sourceInfo);
            if (energyIncreasedEvent != null)
            {
                energyIncreasedEvent(sourceInfo);
            }
            Debug.Log("Scheduled energy increase by " + sourceInfo.power + " points on complex " + owner.gameObject.name);
        }
    }
}
