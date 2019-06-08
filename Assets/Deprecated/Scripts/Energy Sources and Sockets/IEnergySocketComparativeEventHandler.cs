using UnityEngine;
using System.Collections;

/*
 * INTERFACE IEnergySocketComparativeEventHandler
 * ----------------------------------------------
 * Implementing objects have definitions for having energy
 * "increased" or "decreased"
 * ----------------------------------------------
 */

public interface IEnergySocketComparativeEventHandler
{
    event UnityAction<Energy> energyDecreasedEvent;
    event UnityAction<Energy> energyIncreasedEvent;
}
