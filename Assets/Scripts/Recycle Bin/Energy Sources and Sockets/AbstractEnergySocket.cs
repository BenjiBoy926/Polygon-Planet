using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

/*
 * CLASS AbstractEnergySocket
 * --------------------------
 * Implementing classes define rules for absorbing energy from energy sources,
 * and have events for when the energy is increased, decreased, totally filled
 * and totally depleted
 * --------------------------
 */ 

public abstract class AbstractEnergySocket : MonoBehaviour
{
    // Hazard-healer classifications may be useful to the implemening class,
    // but might not be used
    [SerializeField]
    protected List<Tag> hazards;
    [SerializeField]
    protected List<Tag> healers;

    // Absorb the energy from the source
    public abstract void AbsorbEnergy(Energy sourceInfo);
}
