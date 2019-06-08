using UnityEngine.Events;
using System.Collections;

/*
 * INTERFACE IEnergySocketSuperlativeEventHandler
 * ----------------------------------------------
 * Describes an object that can have its energy filled or emptied out
 * Implies that its energy can be increased / decreased
 * ----------------------------------------------
 */

public interface IEnergySocketSuperlativeEventHandler : IEnergySocketComparativeEventHandler
{
    event UnityAction energyFilledEvent;
    event UnityAction energyEmptiedEvent;
}
