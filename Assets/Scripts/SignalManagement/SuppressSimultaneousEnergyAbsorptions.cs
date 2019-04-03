using UnityEngine;
using System;
using System.Collections.Generic;

/*
 * CLASS SuppressSimultaneousEnergyAbsorptions
 * -------------------------------------------
 * An advanced way to connect energy sockets to stockpiles. In this class,
 * certain socket-stockpile connections are given priority over others.  
 * If two or more sockets signal an energy absoption on the same frame,
 * the one with higher priority is taken and the others are discarded
 * 
 * EXAMPLE:
 * If an object has a socket on their shield and their body, the socket
 * on the shield should be given priority over the body, since things that
 * hit the shield should not be allowed to transmit energy to the body
 * under any circumstances
 * ------------------------------------------
 */

public class SuppressSimultaneousEnergyAbsorptions : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Energy sockets change stock on paired stockpiles. " +
        "In case of simultanous signals from two or more energy sockets, " +
        "the stockpile with the highest priority has its stock changed " +
        "and all other signals are discarded")]
    private List<PrioritizedSocketStockpilePair> prioritizedPairs;
    // Helper class greatly eases the task of letting only one energy
    // absorption be registered each frame
    private SimultaneousSignalSuppressor<EnergyAbsorbedEventData> energyAbsorbedSuppressor;

    private void Start()
    {
        // Initialize the suppressor
        energyAbsorbedSuppressor = new SimultaneousSignalSuppressor<EnergyAbsorbedEventData>(GetLocalPriority, ChangeAllStock);
        // Have each energy socket add the event data to the signal suppressor when it absorbs energy
        foreach (PrioritizedSocketStockpilePair pair in prioritizedPairs)
        {
            pair.socket.energyAbsorbedEvent += energyAbsorbedSuppressor.AddSignal;
        }
    }

    // Transmit only one signal per frame
    private void Update()
    {
        energyAbsorbedSuppressor.TransmitAndSuppress();
    }

    // When the signal suppressor transmits the signal, this method is called
    // Find all local pairs whose energy sockets match the one that sent the signal,
    // then change stock on each stockpile in each pair
    private void ChangeAllStock(EnergyAbsorbedEventData eventData)
    {
        List<SocketStockpilePair> pairs = ConnectionsInEvent(eventData);
        foreach(SocketStockpilePair pair in pairs)
        {
            pair.stockpile.ChangeStock(eventData.amountAbsorbed);
        }
    }

    // Find the priority wrapper whose energy socket matches the one in the event data and return its priority
    private int GetLocalPriority(EnergyAbsorbedEventData eventData)
    {
        PrioritizedSocketStockpilePair matchedPair = prioritizedPairs.Find(x => x.socket == eventData.socket);
        return matchedPair.priority;
    }

    // Given the data in an event, find all local pairs with the same energy socket and return the pairs
    private List<SocketStockpilePair> ConnectionsInEvent(EnergyAbsorbedEventData eventData)
    {
        List<SocketStockpilePair> pairs = prioritizedPairs.ConvertAll(x => x.pair);
        pairs.RemoveAll(x => x.socket != eventData.socket);
        return pairs;
    }
}
