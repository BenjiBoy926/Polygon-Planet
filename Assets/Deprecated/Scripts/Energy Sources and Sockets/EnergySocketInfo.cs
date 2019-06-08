using UnityEngine;
using UnityEngine.Events;
using System;

[Serializable]
public class EnergySocketInfo : IEnergySocketSuperlativeEventHandler
{
    [SerializeField]
    private int maxEnergy;  // Max energy of the socket
    [SerializeField]
    private int startingEnergy; // Starting energy of the socket
    private int _currentEnergy;  // Current energy of the socket

    // Events
    public event UnityAction<Energy> energyIncreasedEvent;
    public event UnityAction<Energy> energyDecreasedEvent;
    public event UnityAction energyFilledEvent;
    public event UnityAction energyEmptiedEvent;

    public int currentEnergy
    {
        get
        {
            return _currentEnergy;
        }

        // Always clamp current energy within min/max
        set
        {
            _currentEnergy = value;
            _currentEnergy = (int)Mathf.Clamp(currentEnergy, 0f, maxEnergy);
        }
    }

    public bool full { get { return _currentEnergy >= maxEnergy; } }
    public bool empty { get { return _currentEnergy <= 0f; } }

    public void Initialize()
    {
        currentEnergy = startingEnergy;
    }

    // Increase/decrease the energy by the energy source's energy
    public void DecreaseEnergy(Energy sourceInfo)
    {
        DeltaEnergy(-sourceInfo.power, energyDecreasedEvent, sourceInfo, energyEmptiedEvent, () => empty);
        Debug.Log("Decreaseing energy by " + sourceInfo.power);
    }
    public void IncreaseEnergy(Energy sourceInfo)
    {
        DeltaEnergy(sourceInfo.power, energyIncreasedEvent, sourceInfo, energyFilledEvent, () => full);
        Debug.Log("Increasing energy by " + sourceInfo.power);
    }

    // Change the energy. Give events for what will happen when energy increases, decreases, is full or is empty
    private void DeltaEnergy(int delta, UnityAction<Energy> comparativeEvent, Energy comparativeArg,
        UnityAction superlativeEvent, Func<bool> superlativeCheck)
    {
        currentEnergy += delta;
        // Invoke comparative event if it exists
        if(comparativeEvent != null)
        {
            comparativeEvent(comparativeArg);
        }
        // If the superlative condition is met and the event exists, invoke it
        if (superlativeCheck() && superlativeEvent != null)
        {
            superlativeEvent();
        }
    }
}